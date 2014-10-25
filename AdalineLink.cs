using System;

namespace NeuralNetLib
{
    public class AdalineLink : NeuralLink
    {
        public AdalineLink()
        {
            this[WeightIndex] = Utility.RandomRange(-1.0, 1.0);
        }

        public AdalineLink(NeuralNodeBase inNode, NeuralNodeBase outNode) : base(inNode, outNode)
        {
            this[WeightIndex] = Utility.RandomRange(-1.0, 1.0);
        }

        public override void Epoch(int reserved)
        {
            throw new NotImplementedException();
        }
    }
}