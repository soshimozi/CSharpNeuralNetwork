using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetLib
{
    public class BPOutputNode : BPNode
    {
        public BPOutputNode(double lr, double mt)
            : base(3, 1)
        {
            // default of 3 value set members (NODE_VALUE, LEARNING_RATE, MOMENTUM)
            // default of 1 error set member (NODE_ERROR)
            LearningRate = lr;
            Momentum = mt;
        }

        public double LearningRate
        {
            get { return NodeValues[(int)NodeValueIndex.IdxLearningRate]; }
            set { NodeValues[(int)NodeValueIndex.IdxLearningRate] = value; }
        }

        public double Momentum
        {
            get { return NodeValues[(int)NodeValueIndex.IdxMomentum]; }
            set { NodeValues[(int)NodeValueIndex.IdxMomentum] = value; }
        }

        public double NodeValue
        {
            get { return NodeValues[(int)NodeValueIndex.IdxNodeValue]; }
            set { NodeValues[(int)NodeValueIndex.IdxNodeValue] = value; }
        }

        public double Error
        {
            get { return NodeErrors[(int)NodeValueIndex.IdxNodeError]; }
            set { NodeErrors[(int)NodeValueIndex.IdxNodeError] = value; }
        }

        protected virtual double ComputeError()
        {
            return NodeValue * (1.0 - NodeValue) *  // Compute output node error
                (Error - NodeValue);
        }

        public override void Learn()
        {
            Error = ComputeError();

            for (var i = 0; i < InLinks.Count; i++)   // For each input link
            {
                var delta = LearningRate * Error * InLinks[i].GetInValue(0);
                InLinks[i].UpdateWeight(delta);
            }
        }

        public override void Epoch()
        {
            throw new NotImplementedException();
        }
    }

}
