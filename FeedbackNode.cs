using System;

namespace NeuralNetLib
{
	/// <summary>
	/// Summary description for FeedbackNode.
	/// </summary>
	public abstract class FeedbackNode : NeuralNodeBase
	{
	    protected FeedbackNode() : base( 1, 1 )
		{}

	    protected FeedbackNode( int numValues, int numErrors ) : base( numValues, numErrors )
		{}

		protected virtual double TransferFunction( double val ) { return val; }

		public override void Run()
		{
			var total=0.0;
			for( var i=0; i<InLinks.Count; i++ )
			{
				var link = InLinks[i];

				// calculate weighted value
				total +=  link.GetWeightedValue(0, LinkDirection.Input);
			}

			// set node value to result of transfer function
			SetNodeValue( 0, TransferFunction(total) );
		}
	}
}
