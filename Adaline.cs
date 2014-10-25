using System;

namespace NeuralNetLib
{
    /// <summary>
	/// Summary description for AdalineNode.
	/// </summary>
	public class AdalineNode : FeedbackNode
	{
		const int LearningRateIndex = 1;
		protected override double TransferFunction( double value )
		{   
			// Threshold
			// Transfer Function
			return (value < 0 ? -1.0 : 1.0 );
		}

		public override void Learn() 
		{
			// ADALINE learning function
			NodeErrors[DefaultIndex] = NodeValues[DefaultIndex]*-2.0;

		    foreach(var link in InLinks)
			{
			    // apply the Delta rule
			    var delta = NodeValues[LearningRateIndex] * link.GetInValue(DefaultIndex) * NodeErrors[DefaultIndex];
			    link.UpdateWeight( delta );
			}
		}
		
		public AdalineNode() : base(2, 1)
		{
		}

		public AdalineNode( double learningRate ) : base(2, 1)
		{
			NodeValues[LearningRateIndex] = learningRate; 
		}

        public override void Epoch()
        {
            throw new NotImplementedException();
        }
    }

}
