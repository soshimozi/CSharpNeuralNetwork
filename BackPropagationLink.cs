using System;

namespace NeuralNetLib
{
    public class BackPropagationLink : NeuralLink
    {
        public BackPropagationLink(NeuralNodeBase inNode, NeuralNodeBase outNode)
            : base(inNode, outNode, 2)
        {
            Init();
        }

        public BackPropagationLink()
            : base(2)
        {
            Init();
        }

        public override void UpdateWeight(double newValue)
        {
            var momentum = OutNode.GetNodeValue(MomentumIndex);

            // and percent of last change
            Values[WeightIndex] += (newValue + (momentum * Values[DeltaIndex]));   // Update weight with current change

            // Store current change for next time
            Values[DeltaIndex] = newValue;
        }

        private void Init()
        {
            // Weight random value between -1.0 and 1.0
            Values[WeightIndex] = Utility.RandomRange(-1.0, 1.0);

            // Initialize previous change to 0.0
            Values[DeltaIndex] = 0.0;
        }

        public override void Epoch(int reserved)
        {
            throw new NotImplementedException();
        }
    }

}
