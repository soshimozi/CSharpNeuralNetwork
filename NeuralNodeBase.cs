using System.Collections.Generic;

namespace NeuralNetLib
{

	/// <summary>
	/// Summary description for NeuralNode.
	/// </summary>
	public abstract class NeuralNodeBase
	{
		protected double [] NodeValues;
		protected double [] NodeErrors;
		
		public List<NeuralLink> InLinks;
		public List<NeuralLink> OutLinks;


	    public const int DefaultIndex = 0;

	    protected NeuralNodeBase(int numValues, int numErrors)
		{
			NodeValues = new double[numValues];
			NodeErrors = new double[numErrors];

            OutLinks = new List<NeuralLink>();
            InLinks = new List<NeuralLink>();
		}

		public virtual double GetNodeValue( int index )
		{
			return NodeValues[index];
		}

		public virtual void SetNodeValue( int index, double newVal )
		{
			NodeValues[index] = newVal;
		}

		public void SetNodeError(double val)
		{
			SetNodeError(0,val);
		}

		public virtual void SetNodeError( int index, double val )
		{
			NodeErrors[index] = val;
		}

		public double GetNodeError()
		{
			return GetNodeError(0);
		}

		public virtual double GetNodeError( int index )
		{
			return NodeErrors[index];
		}

		public abstract void Run();

		public abstract void Epoch();

		public abstract void Learn();

		public virtual void Save( System.IO.Stream outfile )
		{
			var bw = new System.IO.BinaryWriter(outfile);

			bw.Write(NodeValues.Length);
			for( var i=0; i<NodeValues.Length; i++ )
			{
				bw.Write(NodeValues[i]);
			}
			bw.Write(NodeErrors.Length);
			for( var i=0; i<NodeErrors.Length; i++ )
			{
				bw.Write(NodeErrors[i]);
			}
		}

		public virtual void Load( System.IO.Stream infile )
		{
			var br = new System.IO.BinaryReader(infile);

			var nodeLength = br.ReadInt32();
			NodeValues = new double[nodeLength];
			for( var i=0; i<NodeValues.Length; i++ )
			{
				NodeValues[i] = br.ReadDouble();
			}

			var errorLength = br.ReadInt32();
			NodeErrors = new double[errorLength];
			for( var i=0; i<NodeErrors.Length; i++ )
			{
				NodeErrors[i] = br.ReadDouble();
			}
		}

        public string Name
        {
            get;
            set;
        }

		public override string ToString()
		{
			return Name;
		}
	}



}
