using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetLib
{
    public class BAMNode : AdalineNode
    {
        public const int LastNodeValueIndex = 1;
        // Reuses the ADALINE_Node's two value set members,
        public override void Run()
        {
            NodeValues[LastNodeValueIndex] = NodeValues[DefaultIndex];   // Store previous value

            // call base run function
            base.Run();
        }

        public override void Learn()
        {
            for (var i = 0; i < InLinks.Count; i++)   // for each of the node's input links
            {
                var link = InLinks[i];

                link.UpdateWeight(link.GetInValue(DefaultIndex) * link.GetOutValue(DefaultIndex));
            }
        }

        public void SetNodeValue(double val)
        {
            SetNodeValue(val, 0);
        }

        public void SetNodeValue(double val, int id)
        {
            NodeErrors[DefaultIndex] = val;   // stored for error computation
            NodeValues[DefaultIndex] = val;
        }

        public override string ToString()
        {
            return "BAMNode";
        }
    }

}
