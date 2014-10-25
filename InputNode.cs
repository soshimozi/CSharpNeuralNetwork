using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetLib
{
    /// <summary>
    // The InputNode class is a generic Input Node.  It can be used with most networks.
    /// </summary>
    public class InputNode : NeuralNodeBase
    {
        public InputNode(int size)
            : base(size, size)
        {
            for (var i = 0; i < size; i++)
            {
                NodeErrors[i] = 0.0;
                NodeValues[i] = 0.0;
            }

        }

        public InputNode()
            : base(1, 1) // Default of one value
        {
            NodeErrors[0] = 0.0;
            NodeValues[0] = 0.0;
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void Epoch()
        {
            throw new NotImplementedException();
        }

        public override void Learn()
        {
            throw new NotImplementedException();
        }
    }

}
