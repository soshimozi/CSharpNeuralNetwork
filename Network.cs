using System;
using System.Collections;

namespace NeuralNetLib
{
	public struct Constraint
	{
		public double minVal;
		public double maxVal;
		public Constraint( double min, double max )
		{
			minVal = min;
			maxVal = max;
		}
	}

	public struct Weight
	{
		private double curValue;
		private Constraint _constraint;

		public Weight( Constraint constraint, double val )
		{
			curValue = val;
			_constraint = constraint;
		}

		public Weight( double val )
		{
			curValue = val;
			_constraint = new Constraint(0.0, 1.0);
		}

		public double CurrentValue
		{
			get { return curValue; }
			set { curValue = value; }
		}

		public double MaxValue
		{
			get { return _constraint.maxVal; }
			set { _constraint.maxVal = value; }
		}

		public double MinValue
		{
			get { return _constraint.minVal; }
			set { _constraint.minVal = value; }
		}
	}

	public class Neurode
	{
		private Weight _weight;
		public Neurode(Weight weight)
		{
			_weight = weight;
		}

		public Weight weight
		{
			get { return _weight; }
			set { _weight = value; }
		}
	}

	public class NeurodeCollection : CollectionBase  
	{
		public Neurode this[ int index ]  
		{
			get  
			{
				return( (Neurode) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}

		public int Add( Neurode value )  
		{
			return( List.Add( value ) );
		}

		public int IndexOf( Neurode value )  
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, Neurode value )  
		{
			List.Insert( index, value );
		}

		public void Remove( Neurode value )  
		{
			List.Remove( value );
		}

		public bool Contains( Neurode value )  
		{
			// If value is not of type Int16, this will return false.
			return( List.Contains( value ) );
		}

		protected override void OnInsert( int index, Object value )  
		{
			if ( value.GetType() != Type.GetType("NeuralNetLib.Neurode") )
				throw new ArgumentException( "value must be of type NeuralNetLib.Neurode.", "value" );
		}

		protected override void OnRemove( int index, Object value )  
		{
			if ( value.GetType() != Type.GetType("NeuralNetLib.Neurode") )
				throw new ArgumentException( "value must be of type NeuralNetLib.Neurode.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )  
		{
			if ( newValue.GetType() != Type.GetType("NeuralNetLib.Neurode") )
				throw new ArgumentException( "newValue must be of type NeuralNetLib.Neurode.", "newValue" );
		}

		protected override void OnValidate( Object value )  
		{
			if ( value.GetType() != Type.GetType("NeuralNetLib.Neurode") )
				throw new ArgumentException( "value must be of type NeuralNetLib.Neurode." );
		}

	}

	public class NeuralLayer : NeurodeCollection
	{
		public delegate double WeightInitDelegate();
		public NeuralLayer(long numNodes, WeightInitDelegate calcFunc)
		{
			// create the layer
//
			for( int i=0; i<numNodes; i++ )
			{
				Random rnd = new Random( DateTime.Now.Millisecond );
				Add( new Neurode( new Weight(calcFunc()) ) );
			}
		}
		public NeuralLayer(long numNodes)
		{
			// create the layer
			//
			for( int i=0; i<numNodes; i++ )
			{
				Random rnd = new Random( DateTime.Now.Millisecond );
				Add( new Neurode( new Weight(rnd.NextDouble()) ) );
			}
		}
	}

	/// <summary>
	/// Summary description for Network.
	/// </summary>
	public class AdelineNetwork
	{
		const int bias = 1;
		const int invalidState = -1;
		const double errorFactor = 0.5;

		private double biasWeight;

		private NeuralLayer [] inputLayer;
		private NeuralLayer  [] outputLayer;
		private Random rnd;

		public AdelineNetwork(int inputLayerSize, int outputLayerSize)
		{
			//
			// TODO: Add constructor logic here
			//
			rnd = new Random( DateTime.Now.Millisecond );
			biasWeight = rnd.NextDouble() - 0.5; // normalize number between (-.5 and .5)

			inputLayer = new NeuralLayer[outputLayerSize];
			outputLayer = new NeuralLayer[outputLayerSize];
			
			for( int i=outputLayerSize; i>0; i-- )
			{
				inputLayer[i] = new NeuralLayer(inputLayerSize, new NeuralLayer.WeightInitDelegate( InputInitFunction ) );
			}

			for( int i=outputLayerSize; i>0; i-- )
			{
				// output layer is one node only (summation of input nodes associated with this node)
				// can also build a map/graph that associates the various input clusters (sensors) with
				// output nodes, a complicated neural net can begin to emerge from this
				outputLayer[i] = new NeuralLayer(1);	
			}
		}

		public void TrainNetwork( NeuralLayer [] inputs,  NeuralLayer [] desiredOutput )
		{
		}

		private double InputInitFunction()
		{
			return NormalizeRandom( rnd.NextDouble(), new Constraint( -1, 1 ) );
		}

		private double NormalizeRandom(double input, Constraint constraint)
		{
			return input * (constraint.maxVal-constraint.minVal) - constraint.minVal;
		}
	}
}
