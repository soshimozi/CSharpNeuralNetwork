using System;

namespace NeuralNetLib
{
	public class AdalineLink : NeuralLink
	{
		public AdalineLink()
		{
			setValue( NeuralLink.WeightIndex, Utility.RandomRange( -1.0, 1.0 ) );
		}

		public AdalineLink(NeuralNodeBase InNode, NeuralNodeBase OutNode)
		{
			setValue( NeuralLink.WeightIndex, Utility.RandomRange( -1.0, 1.0 ) );

			inNode = InNode;
			outNode = OutNode;
		}
	}

	/// <summary>
	/// Summary description for AdalineNode.
	/// </summary>
	public class AdalineNode : FeedbackNode
	{
		const int LearningRateIndex = 1;
		protected virtual double TransferFunction( double value )
		{   
			// Threshold
			// Transfer Function
			return (value < 0 ? -1.0 : 1.0 );
		}

		public virtual new void Learn( int mode ) 
		{
			// ADALINE learning function
			nodeErrors[NeuralNodeBase.DefaultIndex] = nodeValues[NeuralNodeBase.DefaultIndex]*-2.0;

			double delta;
			foreach(NeuralLink link in InLinks)
			{
				// apply the Delta rule
				delta = nodeValues[AdalineNode.LearningRateIndex] * link.InNode.getNodeValue() * nodeErrors[NeuralNodeBase.DefaultIndex];
				link.updateWeight( delta );
			}
		}
		
		public AdalineNode() : base(2, 1)
		{
		}

		public AdalineNode( double learningRate ) : base(2, 1)
		{
			nodeValues[LearningRateIndex] = learningRate; 
		}
	}

	public class AdalineNetwork : NeuralNetBase
	{
		double learningRate;
		public AdalineNetwork( int size ) : base()
		{
			// Constructor
			nodeCount=size+2;		// num nodes equals input layer 
									// size + bias node + ADALINE Node
			linkCount=size+1;		// num links equal one for each input
									// layer node and one for bias node

			learningRate=0;
			createNetwork();
		}

		protected override void createNetwork() 
		{
			nodes = new NeuralNodeBase[nodeCount];
			links = new NeuralLink[linkCount];

			for (int i=0; i<nodeCount-2; i++)     // Create Input Nodes
				nodes[i] = new InputNode();

			nodes[nodeCount-2] = new BiasNode();      // Create Bias Node
			nodes[nodeCount-1] = new AdalineNode(learningRate);

			for (int i=0; i<linkCount; i++)
			{
				AdalineLink l = new AdalineLink(nodes[i], nodes[nodeCount-1]); // Create links

				//l.InNode = nodes[i];
				//l.OutNode = nodes[nodeCount-1]; 

				links[i] = l;
			}

			for (int i=0; i<linkCount; i++)           // Connect inputs to ADALINE
			{
				Link( nodes[i], nodes[nodeCount-1], links[i] );
			}
		}

		protected override void loadInputs() 
		{
			// If network node has any input links,
			if (InLinks.Count > 0)                 // load the connected node's input values 
			{	
				for (int i=0; i<InLinks.Count; i++ )
				{
					NeuralLink link = InLinks[i];
					setNodeValue( i, link.InNode.getNodeValue() );
				}
			}
		}

		public AdalineNetwork( int size, double lr ) : base()  // Constructor
		{
			nodeCount = size+2;
			linkCount = size+1;
			learningRate =lr;
			createNetwork();
		}

		public override double getNodeValue( int id )
		{
			return nodes[nodeCount-1].getNodeValue();   // Return only ADALINE node value
		}

		public override void setNodeValue( int id, double newValue )
		{
			nodes[id].setNodeValue(newValue);
		}

		public override void Run( int mode )
		{
			loadInputs();
			nodes[nodeCount-1].Run( mode );
		}


		public override void Learn( int mode ) 
		{
			nodes[nodeCount-1].Learn( mode ); // only ADALINE node needs to execute its Learn
		}
	}
}
