namespace NeuralNetLib
{
    public class SONNetwork : AdalineNetwork
    {
        public const int CompositeIndex=0;
        public const int RowIndex=1;
        public const int ColIndex=2;
		
        protected int XSize, YSize;								// size of Kohonen layer
        protected double InitLearningRate, FinalLearningRate;	// Learning Rate range
        protected int InitNeighSize, NeighDecrementInterval;	// Neigh. Size info
        protected int Neighborhood;								// Current neigh. size
        protected int NumIterations;							// number of training iterations
        protected int CurIteration;							// current iteration     
        protected int CurNeighSize;								// current neighborhood size
        protected int MinX, MinY;								// current winning node position
        protected SONNode [][] KohonenLayer;				// Kohonen layer

        public override void CreateNetwork()
        {
		
            Nodes = new NeuralNodeBase[NodeCount];
            LinkCount=NodeCount*XSize*YSize;

            Links = new NeuralLink[LinkCount];

            //int curr=nodeCount;
            for( var i=0; i<NodeCount; i++ )
            {
                Nodes[i] = new InputNode();
            }

            for( var i=0; i<LinkCount; i++ )
            {
                Links[i] = new SONLink();
            }

            // now build kohonen network
            KohonenLayer = new SONNode[XSize][];
            for( int x=0, curr=0; x<XSize; x++ )
            {
                KohonenLayer[x] = new SONNode[YSize];
                for( var y=0; y<YSize; y++ )
                {
                    KohonenLayer[x][y] = new SONNode(LearningRate);
                    for( var i=0; i<NodeCount; i++ )
                    {
                        var inNode = Nodes[i];
                        var outNode = KohonenLayer[x][y];
                        var link = Links[curr++];

                        outNode.InLinks.Add( link );
                        inNode.OutLinks.Add( link );

                        link.InNode = inNode;
                        link.OutNode = outNode;
                    }
                }
            }
        }


        public SONNetwork( int numberInputs, int xCount, int yCount, double initialLearningRate,
                           double finalLearningRate, int neighborhoodSize, int neighborhoodDecrementSize, int numberIterations )
        {
            InitLearningRate = initialLearningRate;
            FinalLearningRate = finalLearningRate;
            LearningRate = initialLearningRate;
            InitNeighSize = neighborhoodSize;
            NeighDecrementInterval = neighborhoodDecrementSize;
            NumIterations = numberIterations;
            CurIteration = 0;
            NodeCount = numberInputs;
            Neighborhood = neighborhoodSize;
            XSize = xCount;
            YSize = yCount;

            //CreateNetwork();
        }

        public override void Epoch()
        {
            CurIteration++;

            LearningRate = InitLearningRate-
                                ((CurIteration/(double)NumIterations)*
                                 (InitLearningRate-FinalLearningRate));

            if((((CurIteration+1)%NeighDecrementInterval) == 0) && (Neighborhood>0))
            {
                Neighborhood--;
            }
        }

        public override double GetNodeError(int index)
        {
            return 0.0;
        }

        public override void SetNodeError(int index, double val) {}

        public override double GetNodeValue(int index)
        {
            switch( index )
            {
                case SONNetwork.RowIndex:
                    return MinX;

                case SONNetwork.ColIndex:
                    return MinY;

                case SONNetwork.CompositeIndex:
                    return MinX*YSize+MinY;

                default:
                    return MinX*YSize+MinY;
            }
						  
        }

        public override void Save(System.IO.Stream outfile)
        {
            var bw = new System.IO.BinaryWriter(outfile);

            //bw.Write(nodeName);
            bw.Write(InitLearningRate);
            bw.Write(FinalLearningRate);
            bw.Write(InitNeighSize);
            bw.Write(NeighDecrementInterval);
            bw.Write(NumIterations);
            bw.Write(XSize);
            bw.Write(YSize);

            SaveLinksAndNodes(outfile);

            // now save kohonenLayer
            for( var x=0; x<XSize; x++ )
            {
                for( var y=0; y<YSize; y++ )
                {
                    KohonenLayer[x][y].Save(outfile);
                }
            }
        }

        public override void Load(System.IO.Stream infile)
        {
            var br = new System.IO.BinaryReader(infile);

            //bw.Write(nodeName);
            InitLearningRate = br.ReadDouble();
            FinalLearningRate = br.ReadDouble();
            InitNeighSize = br.ReadInt32();
            NeighDecrementInterval = br.ReadInt32();
            NumIterations = br.ReadInt32();
            XSize = br.ReadInt32();
            YSize = br.ReadInt32();

            LoadLinksAndNodes(infile);

            // now load kohonenLayer
            for( var x=0; x<XSize; x++ )
            {
                for( var y=0; y<YSize; y++ )
                {
                    KohonenLayer[x][y].Load(infile);
                }
            }

        }

        public override void Run()
        {
            int x;
            var minValue = 999999.0;

            LoadInputs();
            for( x=0; x<XSize; x++ )
            {
                int y;
                for( y=0; y<YSize; y++ )
                {
                    KohonenLayer[x][y].Run();
                    var nodeValue = KohonenLayer[x][y].GetNodeValue((int)NodeValueIndex.IdxNodeValue);
                    if (nodeValue >= minValue) continue;
                    minValue=nodeValue;
                    MinX = x;
                    MinY = y;
                }
            }
        }

        public override void Learn()
        {
            var xStart = MinX-Neighborhood;
            var xStop = MinX+Neighborhood;
            var yStart = MinY-Neighborhood;
            var yStop = MinY+Neighborhood;

            if(xStart<0) xStart = 0;
            if(xStop>=XSize) xStop = XSize - 1;
            if(yStart<0) yStart = 0;
            if(yStop>=YSize) yStop = YSize - 1;

            for(var x=xStart; x<=xStop; x++)
            {
                for( var y=yStart; y<=yStop; y++ )
                {
                    KohonenLayer[x][y].SetNodeValue((int)NodeValueIndex.IdxLearningRate, LearningRate);
                    KohonenLayer[x][y].Learn();
                }
            }
        }

        public virtual int GetNeighborhoodSize()
        {
            return Neighborhood;
        }

        public virtual double GetLearningRate()
        {
            return LearningRate;
        }

        public virtual int GetInputCount()
        {
            return NodeCount;
        }

        public virtual int GetRows()
        {
            return YSize;
        }

        public virtual int GetCols()
        {
            return XSize;
        }

    }
}