namespace NeuralNetLib
{
    public class BPMiddleNode : BPOutputNode
    {
        public BPMiddleNode(double lr, double mt) : base(lr, mt) { }

        protected override double ComputeError()
        {
            double total = 0;
            for (var i = 0; i < OutLinks.Count; i++)    // For each of the node's output links
            {
                total += OutLinks[i].GetWeightedError(0, LinkDirection.Output);
            }
            return NodeValue * (1.0 - NodeValue) * total;
        }
    }
}
