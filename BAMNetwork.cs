using System;

namespace NeuralNetLib
{
    public class BAMNetwork : AdalineNetwork          // BAM Network Node
    {
        public const int MaxError = 99999;

        protected int InputCount;      // Number of Input Layer Nodes
        protected int OutputCount;     // Number of Output Layer Nodes

        public override void CreateNetwork()
        {
            Nodes = new NeuralNodeBase[NodeCount];
            Links = new NeuralLink[LinkCount];

            for (var i = 0; i < InputCount; i++)              // Create Input Nodes
                Nodes[i] = new BAMInputNode();

            for (var i = InputCount; i < InputCount + OutputCount; i++)     // Create Output Nodes
                Nodes[i] = new BAMNode();

            for (var i = 0; i < LinkCount; i++)               // Create links
                Links[i] = new BAMLink();

            var currLink = 0;
            for (var i = 0; i < InputCount; i++)                  // Connect inputs to ADALINE
            {
                for (var j = InputCount; j < InputCount + OutputCount; j++)
                {
                    Links[currLink].InNode = Nodes[i];
                    Links[currLink].OutNode = Nodes[j];

                    var inNode = Nodes[i];
                    var outNode = Nodes[j];
                    var link = Links[currLink++];

                    outNode.InLinks.Add(link);
                    inNode.OutLinks.Add(link);

                    link.InNode = inNode;
                    link.OutNode = outNode;
                }
            }
        }

        public override void LoadInputs()
        {
            if (InLinks.Count <= 0) return;
            for (var i = 0; i < InLinks.Count; i++)
            {
                SetNodeValue(i, InLinks[i].GetInValue(DefaultIndex));
            }
        }

        public BAMNetwork(int inputs, int outputs)
        {
            InputCount = inputs;
            OutputCount = outputs;
            NodeCount = inputs + outputs;
            LinkCount = inputs * outputs;
            //CreateNetwork();
        }

        public override double GetNodeValue(int index)
        {
            return Nodes[InputCount + index].GetNodeValue(DefaultIndex);   // Get values from output layer
        }

        public override double GetNodeError(int id)
        {
            var sum1 = 0.0;   // Energy for resulting pattern pair
            var sum2 = 0.0;   // Energy for Input pattern and resulting output pattern pair

            for (var i = InputCount; i < InputCount + OutputCount; i++)   // For each output node
            {
                for (var j = 0; j < Nodes[i].InLinks.Count; j++)  // for each input link
                {
                    var link = Nodes[i].InLinks[j];
                    sum1 += link.GetWeightedValue(DefaultIndex, LinkDirection.Input) * link.GetOutValue(DefaultIndex);
                    sum2 += link.GetWeightedError(DefaultIndex, LinkDirection.Input) * link.GetOutValue(DefaultIndex);
                }
            }

            double orthoEnergy = -InputCount * OutputCount;

            return Math.Abs(sum1 - sum2) <= float.Epsilon ? Math.Abs(orthoEnergy - (-sum1)) : MaxError;
        }
        public override void SetNodeValue(Pattern inputPattern)
        {
            for (var i = 0; i < inputPattern.getInSize(); i++)   // Load input layer
                Nodes[i].SetNodeValue(DefaultIndex, inputPattern.getIn(i));

            for (var i = 0; i < inputPattern.getOutSize(); i++)      // Load output layer
                Nodes[i + InputCount].SetNodeValue(DefaultIndex, inputPattern.getOut(i));
        }

        public override void SetNodeValue(int index, double newVal)
        {
            Nodes[index].SetNodeValue(DefaultIndex, newVal);
        }

        public override void Run()
        {
            LoadInputs();                      // Load Input Layer

            bool stable = false;

            int iteration = 0;
            while (!stable)                     // Run until stable
            {
                stable = true;
                iteration++;

                for (int j = InputCount + OutputCount - 1; j >= 0; j--)   // Run all nodes, output layer first
                    Nodes[j].Run();

                if (iteration > 1)   // Check to set if BAM is stable
                {
                    for (var j = 0; j < InputCount + OutputCount && stable; j++)
                        if (Math.Abs(Nodes[j].GetNodeValue(DefaultIndex) - Nodes[j].GetNodeValue(BAMNode.LastNodeValueIndex)) > float.Epsilon)
                            stable = false;
                }
                else
                    stable = false;
            }    // while stable
        }

        public override void Learn()
        {
            for (int i = InputCount; i < InputCount + OutputCount; i++)
                Nodes[i].Learn();
        }

        public virtual void UnLearn()
        {
            for (var i = InputCount; i < InputCount + OutputCount; i++)
                Nodes[i].SetNodeValue(DefaultIndex, -Nodes[i].GetNodeValue(DefaultIndex));

            Learn();
        }
        public override string ToString()
        {
            return "BAMNetwork";
        }
    }

}
