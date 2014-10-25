using System;
using System.Collections.Generic;

namespace NeuralNetLib
{
	/// <summary>
	/// Summary description for NeuralLink.
	/// </summary>
	public abstract class NeuralLink
	{
		public const int WeightIndex = 0;
		public const int DeltaIndex = 1;
		public const int MomentumIndex = 2;

		protected List<double> Values;

	    protected NeuralLink()
		{
			InitValues(1);
		}

	    protected NeuralLink(NeuralNodeBase inNode, NeuralNodeBase outNode, int size)
		{
            InNode = inNode;
            OutNode = outNode;

            var numValues = size;
			if( numValues < 1 )
			{
				numValues = 1;
			}

			InitValues(numValues);
		}

	    protected NeuralLink(int size)
		{
			var numValues = size;

			if( numValues < 1 )
			{
				numValues = 1;
			}
			
			InitValues(numValues);
		}

	    protected NeuralLink(NeuralNodeBase inNode, NeuralNodeBase outNode)
		{
			InNode = inNode;
			OutNode = outNode;

			InitValues(1);
		}

		private void InitValues(int numValues)
		{
            Values = new List<double>();

			for( var i=0; i<numValues; i++ )
			{
                Values.Add(0.0);
			}
		}

        public NeuralNodeBase InNode
        {
            get;
            set;
        }

		public NeuralNodeBase OutNode
		{
			get;
			set;
		}

        public abstract void Epoch(int reserved);

		public virtual int ValueCount
		{
			get { return Values.Count; }
		}

	    public virtual double this[int index] { 
            get
	        {
                if (index >= Values.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return Values[index];	        
	        }

            set
            {
                if (index >= Values.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                Values[index] = value;
            }
        }

		public virtual double GetInValue( int index )
		{
			return InNode.GetNodeValue( index );
		}
	
		public virtual double GetOutValue( int index )
		{
			return OutNode.GetNodeValue( index );
		}

		public virtual double GetWeightedValue( int index, LinkDirection direction)
		{
			var val = 0.0;
			switch (direction)
			{
			    case LinkDirection.Input:
			        val =  Values[WeightIndex] * InNode.GetNodeValue(index);
			        break;
			    case LinkDirection.Output:
			        val =  Values[WeightIndex] * OutNode.GetNodeValue(index);
			        break;
			}
			return val;
		}

		public virtual double GetWeightedError(int index, LinkDirection direction)
		{
			var val = 0.0;
			switch (direction)
			{
			    case LinkDirection.Input:
			        val = Values[WeightIndex] * InNode.GetNodeError(index);
			        break;
			    case LinkDirection.Output:
			        val = Values[WeightIndex] * OutNode.GetNodeError(index);
			        break;
			}
			return val;
		}

		public virtual void UpdateWeight(double newValue)
		{
			Values[WeightIndex] += newValue;
		}

		public void Save(System.IO.Stream outfile)
		{
			var bw = new System.IO.BinaryWriter(outfile);
			bw.Write(Values.Count);
			for( var i=0; i<Values.Count; i++ )
			{
				bw.Write(Values[i]);
			}
		}

		public void Load(System.IO.Stream infile)
		{
			var br = new System.IO.BinaryReader(infile);
			var nodeLength = br.ReadInt32();
            Values = new List<double>();
			for( var i=0; i<nodeLength; i++ )
			{
				Values.Add(br.ReadDouble());
			}
		}
	}

    public enum LinkDirection
    {
        Input,
        Output
    }
}
