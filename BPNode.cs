using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetLib
{
    /// <summary>
    /// Summary description for BackPropagation.
    /// </summary>
    /// 
    public abstract class BPNode : FeedbackNode
    {
        protected BPNode(int vSize, int eSize) : base(vSize, eSize) { }
        protected BPNode() : base(1, 0) { }
        protected override double TransferFunction(double val)
        {
            return 1.0 / (1.0 + Math.Exp(-val));           // Sigmoid Function
        }
    }
}
