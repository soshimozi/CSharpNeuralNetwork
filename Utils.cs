using System;
using System.IO;

namespace NeuralNetLib
{
	public class Utility
	{
		private static Random globalRandom = new Random(DateTime.Now.Millisecond);
		public static double RandomRange( double lo, double hi )
		{
			return globalRandom.NextDouble() * (hi-lo) + lo;
		}
	}

	public class Pattern
	{
		double [] in_set;          // Pointer to input pattern array
		double [] out_set;         // Pointer to output pattern array
		int id;                  // Pattern Identification Number
		int in_size, out_size;   // Input and Output pattern sizes

		public Pattern( int num_in, int num_out ) 
		{
			in_size=num_in;
			out_size= num_out;
			in_set=new double[in_size];
			out_set=new double[out_size];
		}

		public Pattern( double [] in_array, double [] out_array ) 
		{
			in_size=in_array.Length;
			out_size=out_array.Length;

			in_set = in_array;
			out_set = out_array;
		}
		public Pattern( int num_in, int num_out, System.IO.StreamReader sr ) 
		{
			in_size=num_in;
			out_size=num_out;

			in_set=new double[in_size];
			out_set=new double[out_size];

			string sFileRec = sr.ReadLine();
			System.IO.StringReader reader = new StringReader( sFileRec );

			string sField = ReadNextField(reader);
			if( sField != string.Empty )
			{
				id = Convert.ToInt32(sField);
			}

			for (int i=0; i<in_size; i++)
			{
				sField = ReadNextField(reader);
				if( sField != string.Empty )
				{
					 in_set[i] = Convert.ToDouble(sField);
				}
			}

			for (int i=0; i<out_size; i++)
			{
				sField = ReadNextField(reader);
				if( sField != string.Empty )
				{
					out_set[i] = Convert.ToDouble(sField);
				}
			}
		}
		public virtual double getIn(int id) { return in_set[id]; }
		public virtual double getOut(int id) { return out_set[id]; }

		public virtual void setIn( int id, double val ) { in_set[id] = val; }
		public virtual void setOut( int id, double val ) { out_set[id] = val; }

		public virtual int getInSize() { return in_size; }
		public virtual int getOutSize() { return out_size; }

		protected string ReadNextField(StringReader sField)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			string sOut = string.Empty;
			int ch;
			do
			{
				ch = sField.Read();
			} while( ch == 32 && ch != -1);


			if( ch != -1 )
				sb.Append((char)ch);

			do
			{

				ch = sField.Read();
				if( ch != 32 && ch != -1 )
				{
					sb.Append((char)ch);
				}

			} while( (ch != 32) && (ch != -1) );

			return sb.ToString();
		}

		protected string ConvertByte( byte val )
		{
			byte [] inbuff = new byte[1];
			char [] outbuff = new char[1];
			inbuff[0] = val;
			System.Text.Decoder dec = System.Text.Encoding.ASCII.GetDecoder();
			dec.GetChars(inbuff, 0, 1, outbuff, 0);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append( inbuff );
			return sb.ToString();
		}
	}
}