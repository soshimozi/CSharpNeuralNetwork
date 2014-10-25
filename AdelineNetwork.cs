namespace NeuralNetLib
{
    public abstract class AdalineNetwork : NeuralNet
    {
        protected double LearningRate;

        protected AdalineNetwork()
        { }

        protected AdalineNetwork(int size)
        {
            // Constructor
            NodeCount = size + 2;		// num nodes equals input layer 
            // size + bias node + ADALINE Node
            LinkCount = size + 1;		// num links equal one for each input
            // layer node and one for bias node

            LearningRate = 0;
            //CreateNetwork();
        }

        protected AdalineNetwork(int size, double lr) // Constructor
        {
            NodeCount = size + 2;
            LinkCount = size + 1;
            LearningRate = lr;
           // CreateNetwork();
        }

        protected virtual NeuralNodeBase GetAdalineNode()
        {
            // last node is adaline node
            return Nodes[NodeCount - 1];
        }

        protected virtual NeuralNodeBase GetBiasNode()
        {
            // second to last node is bias node
            return Nodes[NodeCount - 2];
        }

        public override void CreateNetwork()
        {
            Nodes = new NeuralNodeBase[NodeCount];
            Links = new NeuralLink[LinkCount];

            for (var i = 0; i < NodeCount - 2; i++)     // Create Input Nodes
                Nodes[i] = new InputNode();

            Nodes[NodeCount - 2] = new BiasNode();      // Create Bias Node
            Nodes[NodeCount - 1] = new AdalineNode(LearningRate);

            for (var i = 0; i < LinkCount; i++)
            {
                var l = new AdalineLink(Nodes[i], Nodes[NodeCount - 1]); // Create links
                Links[i] = l;
            }

            for (var i = 0; i < LinkCount; i++)           // Connect inputs to ADALINE
            {

                Nodes[i].OutLinks.Add(Links[i]);
                Nodes[NodeCount - 1].InLinks.Add(Links[i]);
                Links[i].InNode = Nodes[i];
                Links[i].OutNode = Nodes[NodeCount - 1];

                var inNode = Nodes[i];
                var outNode = Nodes[NodeCount - 1];
                var link = Links[i];

                outNode.InLinks.Add(link);
                inNode.OutLinks.Add(link);

                link.InNode = inNode;
                link.OutNode = outNode;
            }
        }

        public override void LoadInputs()
        {
            // If network node has any input links,
            if (InLinks.Count <= 0) return;
            for (var i = 0; i < InLinks.Count; i++)
            {
                var link = InLinks[i];
                SetNodeValue(i, link.GetInValue(DefaultIndex));
            }
        }

        public override double GetNodeValue(int id)
        {
            return GetAdalineNode().GetNodeValue(id);   // Return only ADALINE node value
        }

        public override void SetNodeValue(int id, double newVal)
        {
            Nodes[id].SetNodeValue(0, newVal);
        }

        public virtual void SetNodeValue(Pattern inputPattern)
        {
            for (int i = 0; i < inputPattern.getInSize(); i++)			// Load pattern's input
                Nodes[i].SetNodeValue(0, inputPattern.getIn(i));	// value into input layer
        }

        public override void Run()
        {
            LoadInputs();
            GetAdalineNode().Run(); // Run only ADALINE node
        }


        public override void Learn()
        {
            GetAdalineNode().Learn(); // only ADALINE node needs to execute its Learn
        }
    }

}
