using System;

namespace NeuralNetLib
{
	public class SONNode : NeuralNodeBase    // SON processing Node
	{
		public SONNode() : base(2,0) {} // Default of 2 value set members
										// (NODE_VALUE, LEARNING_RATE)
		public SONNode( double lr ) : base(2,0)		// Constructor with 
		{											// Learning Rate
			NodeValues[(int)NodeValueIndex.IdxLearningRate] = lr;                // specified
		}
		public override void Run() 
		{
			var total = 0.0;
			for( var i=0; i<InLinks.Count; i++ )
			{
				var link = InLinks[i];

				total += Math.Pow( link.GetInValue(0) - link[NeuralLink.WeightIndex], 2 );
			}
			NodeValues[(int)NodeValueIndex.IdxNodeValue] = Math.Sqrt( total );
		}

		public override void Learn() 
		{
		    for (var i=0; i<InLinks.Count; i++)      // for each of the node's input link
			{
                var delta = NodeValues[(int)NodeValueIndex.IdxLearningRate] * (InLinks[i].GetInValue(0) - InLinks[i][NeuralLink.WeightIndex]);
			    InLinks[i].UpdateWeight(delta);
			}
		}

		public override void Epoch()
		{
			throw new NotImplementedException();
		}

	}

	public class SONLink : AdalineLink          // Link for SON Node
	{
		public SONLink()
		{
		    this[WeightIndex] = Utility.RandomRange(0.0, 1.0);
		}
	}
}
