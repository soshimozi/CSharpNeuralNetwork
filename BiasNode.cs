namespace NeuralNetLib
{
    /// <summary>
    // The Bias Node Class is a node that
    // always produces the same output.
    // The Bias Node's default output is 1.0
    /// </summary>
    public class BiasNode : InputNode
    {
        public BiasNode(double bias)
            : base(1)        // Constructor
        {
            NodeValues[0] = bias;
        }

        public BiasNode()
            : base(1)
        {
            NodeValues[0] = 1.0;
        }

        public override void SetNodeValue(int index, double newVal) 
        {
            // no setting of values
        }

        public override double GetNodeValue(int index)
        {
            return NodeValues[0];
        }
    }
}
