using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NeuralNetLib;

namespace BAMExample
{
	public sealed class StringPattern : Pattern
	{
		public StringPattern(string pattern, IList<double> outputValues) : base( pattern.Length, outputValues.Count )
		{
			for( var i=0; i<pattern.Length; i++ )
			{
				double val;
				if( pattern[i] == '1' )
				{
					val = 1.0;
				}
				else
				{
					val = -1.0;
				}
				setIn( i, val );
			}

			for( int i=0; i<outputValues.Count; i++ )
			{
				setOut( i, outputValues[i] );
			}
		}
	}
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class BAMDriver
	{
		static readonly string [] PatternStrings = new string[10];
		static readonly double [][] OutputValues = new double [10][];

		static void GenerateOutputValues()
		{
			for( var i=0; i<10; i++ )
			{
				OutputValues[i] = new double[10];
				for( var j=0; j<10; j++ )
				{
					if( i == j )
					{
						OutputValues[i][j] = 1.0;
					}
					else
					{
						OutputValues[i][j] = -1.0;
					}
				}
			}

		}

		static void GenerateInputPatterns()
		{
			PatternStrings[0]  = "11111";
			PatternStrings[0] += "1   1";
			PatternStrings[0] += "11  1";
			PatternStrings[0] += "1 1 1";
			PatternStrings[0] += "1  11";
			PatternStrings[0] += "1   1";
			PatternStrings[0] += "11111";

			PatternStrings[1]  = "  1  ";
			PatternStrings[1] += "  1  ";
			PatternStrings[1] += "  1  ";
			PatternStrings[1] += "  1  ";
			PatternStrings[1] += "  1  ";
			PatternStrings[1] += "  1  ";
			PatternStrings[1] += "  1  ";
		                                               
			PatternStrings[2]  = " 1111";
			PatternStrings[2] += "    1";
			PatternStrings[2] += "    1";
			PatternStrings[2] += " 111 ";
			PatternStrings[2] += "1    ";
			PatternStrings[2] += "1    ";
			PatternStrings[2] += "1111 ";

			PatternStrings[3]  = "11111";
			PatternStrings[3] += "    1";
			PatternStrings[3] += "    1";
			PatternStrings[3] += "11111";
			PatternStrings[3] += "    1";
			PatternStrings[3] += "    1";
			PatternStrings[3] += "11111";

			PatternStrings[4]  = "1  1 ";
			PatternStrings[4] += "1  1 ";
			PatternStrings[4] += "1  1 ";
			PatternStrings[4] += "11111";
			PatternStrings[4] += "   1 ";
			PatternStrings[4] += "   1 ";
			PatternStrings[4] += "   1 ";

			PatternStrings[5]  = "11111";
			PatternStrings[5] += "1    ";
			PatternStrings[5] += "11111";
			PatternStrings[5] += "    1";
			PatternStrings[5] += "    1";
			PatternStrings[5] += "    1";
			PatternStrings[5] += "11111";

			PatternStrings[6]  = "     ";
			PatternStrings[6] += "1    ";
			PatternStrings[6] += "1    ";
			PatternStrings[6] += "1    ";
			PatternStrings[6] += "11111";
			PatternStrings[6] += "1   1";
			PatternStrings[6] += "11111";

			PatternStrings[7]  = "     ";
			PatternStrings[7] += "11111";
			PatternStrings[7] += "    1";
			PatternStrings[7] += "    1";
			PatternStrings[7] += "    1";
			PatternStrings[7] += "    1";
			PatternStrings[7] += "    1";

			PatternStrings[8]  = " 111 ";
			PatternStrings[8] += "1   1";
			PatternStrings[8] += "1   1";
			PatternStrings[8] += " 111 ";
			PatternStrings[8] += "1   1";
			PatternStrings[8] += "1   1";
			PatternStrings[8] += " 111 ";

			PatternStrings[9]  = "     ";
			PatternStrings[9] += "11111";
			PatternStrings[9] += "1   1";
			PatternStrings[9] += "11111";
			PatternStrings[9] += "    1";
			PatternStrings[9] += "    1";
			PatternStrings[9] += "  111";
		}

		static Pattern [] CreatePatterns(int numInputs, int numPatterns)
		{
			var inputs = new double[numPatterns][];
			var outputs = new double[numPatterns][];

			for( var i=0; i<numPatterns; i++ )
			{
				inputs[i] = new double[numInputs];
				for( var j=0; j<numInputs; j++ )
				{
					if( PatternStrings[i][j] == '1' )
						inputs[i][j] = 1.0;
					else
						inputs[i][j] = -1.0;
				}

				outputs[i] = new double[numPatterns];
				for( var j=0; j<numPatterns; j++ )
				{
					outputs[i][j] = ( i == j ? 1.0 : -1.0 );
				}
			}



//			inputs[0] = new double[6];
//			inputs[0][0] = -1.0;
//			inputs[0][1] = 1.0;
//			inputs[0][2] = -1.0;
//			inputs[0][3] = 1.0;
//			inputs[0][4] = -1.0;
//			inputs[0][5] = -1.0;
//
//			inputs[1] = new double[6];
//			inputs[1][0] = -1.0;
//			inputs[1][1] = -1.0;
//			inputs[1][2] = -1.0;
//			inputs[1][3] = 1.0;
//			inputs[1][4] = 1.0;
//			inputs[1][5] = 1.0;
//
//			inputs[2] = new double[6];
//			inputs[2][0] = 1.0;
//			inputs[2][1] = 1.0;
//			inputs[2][2] = 1.0;
//			inputs[2][3] = -1.0;
//			inputs[2][4] = -1.0;
//			inputs[2][5] = -1.0;
//
//			inputs[3] = new double[6];
//			inputs[3][0] = 1.0;
//			inputs[3][1] = -1.0;
//			inputs[3][2] = -1.0;
//			inputs[3][3] = -1.0;
//			inputs[3][4] = -1.0;
//			inputs[3][5] = 1.0;
//
//			outputs[0] = new double[4];
//			outputs[0][0] = 1.0;
//			outputs[0][1] = -1.0;
//			outputs[0][2] = 1.0;
//			outputs[0][3] = -1.0;
//
//			outputs[1] = new double[4];
//			outputs[1][0] = -1.0;
//			outputs[1][1] = -1.0;
//			outputs[1][2] = 1.0;
//			outputs[1][3] = 1.0;
//
//			outputs[2] = new double[4];
//			outputs[2][0] = 1.0;
//			outputs[2][1] = 1.0;
//			outputs[2][2] = -1.0;
//			outputs[2][3] = -1.0;
//
//			outputs[3] = new double[4];
//			outputs[3][0] = 1.0;
//			outputs[3][1] = -1.0;
//			outputs[3][2] = -1.0;
//			outputs[3][3] = 1.0;
////
//			// Input Pattern                     Output Pattern
//			// -------------                     --------------
			var data = new Pattern[numPatterns];

			for( var i=0; i<numPatterns; i++ )
				data[i] = new Pattern(inputs[i], outputs[i]);

			return data;
		}

		static Pattern [] CreateUpperPatterns(int numInputs, int numPatterns)
		{
			var inputs = new double[numPatterns][];
			var outputs = new double[numPatterns][];

			for( var i=0; i<numPatterns; i++ )
			{
				inputs[i] = new double[numInputs];
				for( var j=0; j<numInputs; j++ )
				{
					if( PatternStrings[i+5][j] == '1' )
						inputs[i][j] = 1.0;
					else
						inputs[i][j] = -1.0;
				}

				outputs[i] = new double[numPatterns];
				for( var j=0; j<numPatterns; j++ )
				{
					outputs[i][j] = ( i == j ? 1.0 : -1.0 );
				}
			}


			var data = new Pattern[numPatterns];

			for( var i=0; i<numPatterns; i++ )
				data[i] = new Pattern(inputs[i], outputs[i]);

			return data;
		}

		[STAThread]
		static void Main(string[] args)
		{
//			if( args.Length != 2 )
//			{
//				Console.WriteLine( "Invalid argument list." );
//				return;
//			}
			const int patterns = 5;
			const int inputs = 35;
			const int outputs = 5;

			GenerateOutputValues();
			GenerateInputPatterns();

		    var data = CreatePatterns(inputs, outputs);

			var upperdata = CreateUpperPatterns(inputs, outputs);

		    var builder = new StringBuilder();

			// Print Training Set
			Console.WriteLine( "Training Set" );
			for (var i=0; i<patterns; i++)
			{
				Console.WriteLine( "Index: {0}", i );

			    builder.Remove(0, builder.Length);

				for( var j=0; j<data[i].getInSize(); j++ )
				{
					if( builder.Length > 0 )
						builder.Append(",");
					builder.Append(data[i].getIn(j));
				}
				Console.WriteLine( "   Inputs: {0}", builder );

			    builder.Remove(0, builder.Length);
				for( var j=0; j<data[i].getOutSize(); j++ )
				{
                    if (builder.Length > 0)
                        builder.Append(",");
				    builder.Append(data[i].getOut(j));
				}

				Console.WriteLine( "   Outputs: {0}", builder );
			}


			for (var i=0; i<patterns; i++)
			{
				Console.WriteLine( "Index: {0}", i + 5 );

			    builder.Remove(0, builder.Length);

				for( var j=0; j<upperdata[i].getInSize(); j++ )
				{
                    if (builder.Length > 0)
                        builder.Append(",");
				    builder.Append(upperdata[i].getIn(j));
				}

				Console.WriteLine( "   Inputs: {0}", builder );

                builder.Remove(0, builder.Length);
				for( int j=0; j<upperdata[i].getOutSize(); j++ )
				{
                    if (builder.Length > 0)
                        builder.Append(","); 
                    
                    builder.Append(upperdata[i].getOut(j));
				}
				Console.WriteLine( "   Outputs: {0}", builder );
			}
			
			// Create BAM
			var bam = new BAMNetwork(inputs, outputs);
			var upperbam = new BAMNetwork(inputs, outputs);

            // always have to create the network first, or nothing works
            bam.CreateNetwork();
            upperbam.CreateNetwork();

			// Train the BAM
			for (var i=0; i<patterns; i++)     // Add All Patterns to the BAM
			{
				bam.SetNodeValue(data[i]);    // Load Input Node Values
				bam.Learn();          
			}

			for ( var i=0; i<patterns; i++)
			{
				upperbam.SetNodeValue(upperdata[i]);
				upperbam.Learn();
			}

     
			// Run the BAM
			for (var i=0; i<patterns; i++)
			{
				bam.SetNodeValue(data[i]);    // Load Input Node Values

				bam.Run();

				Console.WriteLine( "Pattern: {0}", i );

				for (int j=0; j<outputs; j++)  // Print results
				{
					Console.WriteLine( "     BAM output: {0}   Desired output: {1}",
						bam.GetNodeValue(j).ToString("F3"),
						data[i].getOut(j).ToString("F3") );
				}
			}   // for i

			// run the second bam
			for (var i=0; i<patterns; i++)
			{
				upperbam.SetNodeValue(upperdata[i]);    // Load Input Node Values

				upperbam.Run();

				Console.WriteLine( "Pattern: {0}", i+5 );

				for (var j=0; j<outputs; j++)  // Print results
				{
					Console.WriteLine( "     BAM output: {0}   Desired output: {1}",
						upperbam.GetNodeValue(j).ToString("F3"),
						upperdata[i].getOut(j).ToString("F3") );
				}
			}   // for i

//			CreateInputPatterns();
//			CreateOutputValues();
//
//			// Create BAM
//			BAMNetwork bam = new BAMNetwork(35, 10);
//
//			// Train the BAM
//			for (int i=0; i<10; i++)     // Add All Patterns to the BAM
//			{
//				StringPattern pattern = new StringPattern( PatternStrings[i], OutputValues[i] );
//				bam.setNodeValue(pattern);    // Load Input Node Values
//				bam.Learn(0);          
//			}
//     
//			// Run the BAM
//			for (int i=0; i<10; i++)
//			{
//				StringPattern pattern = new StringPattern( PatternStrings[i], OutputValues[i] );
//				bam.setNodeValue(pattern);    // Load Input Node Values
//
//				bam.Run(0);
//
//				Console.WriteLine( "Pattern: {0}",  i );
//				for (int j=0; j<10; j++)  // Print results
//				{
//					Console.WriteLine( "BAM Output: {0}   Desired Output: {1}", bam.getNodeValue(j).ToString("F3"), OutputValues[i][j].ToString("F3") );
//				}
//			}   // for i

			Console.Read();
		}
	}
}
