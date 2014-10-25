using System.Collections.Generic;

namespace NeuralNetLib
{


	public class BackpropNetwork : AdalineNetwork
	{
		//protected int NumLayers=0;
		protected int FirstMiddleNode;
		protected int FirstOutputNode;
		protected int [] NodeCounts;
		protected double MomentumTerm;

		public override void CreateNetwork()
		{
			Nodes = new NeuralNodeBase[NodeCount];
			Links = new NeuralLink[LinkCount];

			var curr=0;
			for (var i=0; i<NodeCounts[0]; i++)          // Input layer nodes
				Nodes[curr++]=new InputNode(); 

			FirstMiddleNode=curr;						// Middle layer nodes
			for (var i=1; i<NumLayers-1; i++)
				for (var j=0; j<NodeCounts[i]; j++)
					Nodes[curr++]=new BPMiddleNode(LearningRate,MomentumTerm);

			FirstOutputNode=curr;                    // Output layer nodes
			for (var i=0; i<NodeCounts[NumLayers-1]; i++)
				Nodes[curr++]=new BPOutputNode(LearningRate,MomentumTerm);

			for (var i=0; i<LinkCount; i++)                // Create Links
			{
				Links[i] = new BackPropagationLink();
			}

			curr=0;
			int layer1=0,layer2=FirstMiddleNode;
			for (var i=0; i<NumLayers-1; i++)             // Connect Layers
			{
				for (var j=0; j<NodeCounts[i+1]; j++)
					for (var k=0; k<NodeCounts[i]; k++)
					{
						Links[curr].InNode = Nodes[layer1+k];
						Links[curr].OutNode = Nodes[layer2+j];

                        var link = Links[curr++];
                        var inNode = Nodes[layer1 + k];
                        var outNode = Nodes[layer2 + j];

                        inNode.OutLinks.Add(link);
                        outNode.InLinks.Add(link);

                        link.InNode = inNode;
                        link.OutNode = outNode;

					}

				layer1=layer2;
				layer2+=NodeCounts[i+1];
			}

		}


		public BackpropNetwork( double lr, double mt, int layers, IList<int> counts ) 
		{
			NodeCount=0;
			LinkCount=0;

			NumLayers=layers;
			NodeCounts = new int[layers];
			for (var i=0; i<layers; i++)
			{
				NodeCounts[i]=counts[i];
				NodeCount+=NodeCounts[i];
				if (i>0)
					LinkCount+=NodeCounts[i-1]*NodeCounts[i]; // links between layers
			}

			LearningRate=lr;
			MomentumTerm=mt;

			//CreateNetwork();
		}

		public BackpropNetwork() {}

		public override double GetNodeError(int index)
		{
			return Nodes[index+FirstOutputNode].GetNodeError();  // in output layer only
		}

		public override void SetNodeError(int index, double val)
		{
			Nodes[index+FirstOutputNode].SetNodeError(val);
		}

		public void SetNodeError( Pattern output )
		{
			for (var i=0; i<NodeCounts[NumLayers-1]; i++)  // Set error values with output pattern
				Nodes[i+FirstOutputNode].SetNodeError(output.getOut(i));
		}

		public double GetNodeValue() { return GetNodeValue(0); }
		public override double GetNodeValue(int index)
		{
			return Nodes[index+FirstOutputNode].GetNodeValue((int)NodeValueIndex.IdxNodeValue);  // Get value of output layer nodes
		}

		public override void Save(System.IO.Stream outfile)
		{
			var bw = new System.IO.BinaryWriter(outfile);

			//bw.Write(nodeName);
			bw.Write(NumLayers);
			for( var i=0; i<NumLayers; i++ )
				bw.Write(NodeCounts[i]);

			SaveLinksAndNodes(outfile);

		}

		public override void Load(System.IO.Stream infile)
		{
			var br = new System.IO.BinaryReader(infile);
			NumLayers = br.ReadInt32();
			NodeCounts = new int[NumLayers];
			for( var i=0; i<NumLayers; i++ )
			{
				NodeCounts[i] = br.ReadInt32();
			}

			LoadLinksAndNodes(infile);

		}

		public override void Run()
		{
			LoadInputs();
			for (var i=FirstMiddleNode; i<NodeCount; i++)  // Run only nodes in 
				Nodes[i].Run();       		
		}

		public override void Learn()
		{
			var cnt=OutLinks.Count;
			if (cnt>0)	                // Get error from network node's output links
			{                     // if they exist
				for (var i=0; i<cnt; i++)
				{
					Nodes[i+FirstOutputNode].SetNodeError(OutLinks[i].GetWeightedError(0, LinkDirection.Output));
				}
			}

			for (var i=NodeCount-1; i>=FirstMiddleNode; i--)  // Learn from output layer nodes
				Nodes[i].Learn();                         // back through middle layers
		}

        public int NumLayers { get; private set; }

		public int LayerCount( int id ) { return NodeCounts[id]; }
	}
}
