namespace NeuralNetLib
{
    class BAMInputNode : BAMNode    // BAM Input Node
    {
        public override void Run()
        {
            NodeValues[LastNodeValueIndex] = NodeValues[DefaultIndex];  // Store last value

            var total = 0.0;
            for (var i = 0; i < OutLinks.Count; i++)        // For each of the node's output links
            {
                var link = OutLinks[i];
                total += link.GetWeightedValue(DefaultIndex, LinkDirection.Output);
            }

            NodeValues[DefaultIndex] = TransferFunction(total);
        }

        public override void Learn()
        {
            // disable so it cannot corrupt the weights accidentally
        }

        public override string ToString()
        {
            return "BAMInputNode";
        }
    }
}
