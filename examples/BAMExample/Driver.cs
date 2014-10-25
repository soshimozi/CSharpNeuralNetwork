using System;
using NeuralNetLib;

namespace ConsoleApplication1
{
	public class StringPattern : Pattern
	{
		public StringPattern(string pattern, double [] outputValues) : base( height, outputValues.Length )
		{
			// pattern is calculated as such
			// 1/2 1/4 1/16 1/32 1/64 1/128 1/256 1/512 1/1024 ... 1/2^n (or 2^-n) 
//			for( int y=0; y<height; y++ )
//			{
//				double val = 0.0;
//				for( int x=0; x<width; x++ )
//				{
//					if( pattern[ (y * width) + x ] == '1' )
//					{
//						val += Math.Pow( 2, -x-1 );
//					}
//				}
//
//				setIn( y, val );
//			}

//				if( pattern[i] == '1' )
//				{
//					setIn( i, Math.Pow( 2, -i ) );
//				}
//				else
//				{
//					setIn( i, 0.0 );
//				}

			for( int i=0; i<pattern.Length; i++ )
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

			for( int i=0; i<outputValues.Length; i++ )
			{
				setOut( i, outputValues[i] );
			}
		}
	}
		/// <summary>
		/// Summary description for Class1.
		/// </summary>
		class Driver
		{
			static string [] PatternStrings = new string[11];
			static double [][] OutputValues = new double [10][];

			static void CreateOutputValues()
			{
				for( int i=0; i<10; i++ )
				{
					OutputValues[i] = new double[10];
					for( int j=0; j<10; j++ )
					{
						if( i == j )
						{
							OutputValues[i][j] = 1.0;
						}
						else
						{
							OutputValues[i][j] = 0.0;
						}
					}
				}

			}

			static void CreateInputPatterns()
			{
				PatternStrings[0]  = "01110";
				PatternStrings[0] += "10001";
				PatternStrings[0] += "10001";
				PatternStrings[0] += "10001";
				PatternStrings[0] += "10001";
				PatternStrings[0] += "10001";
				PatternStrings[0] += "01110";

				PatternStrings[1]  = "00100";
				PatternStrings[1] += "01100";
				PatternStrings[1] += "10100";
				PatternStrings[1] += "00100";
				PatternStrings[1] += "00100";
				PatternStrings[1] += "00100";
				PatternStrings[1] += "00100";
		                                               
				PatternStrings[2]  = "01110";
				PatternStrings[2] += "10001";
				PatternStrings[2] += "00001";
				PatternStrings[2] += "00010";
				PatternStrings[2] += "00100";
				PatternStrings[2] += "01000";
				PatternStrings[2] += "11111";

				PatternStrings[3]  = "01110";
				PatternStrings[3] += "10001";
				PatternStrings[3] += "00001";
				PatternStrings[3] += "01110";
				PatternStrings[3] += "00001";
				PatternStrings[3] += "10001";
				PatternStrings[3] += "01110";

				PatternStrings[4]  = "00010";
				PatternStrings[4] += "00110";
				PatternStrings[4] += "01010";
				PatternStrings[4] += "10010";
				PatternStrings[4] += "11111";
				PatternStrings[4] += "00010";
				PatternStrings[4] += "00010";

				PatternStrings[5]  = "11111";
				PatternStrings[5] += "10000";
				PatternStrings[5] += "10000";
				PatternStrings[5] += "11110";
				PatternStrings[5] += "00001";
				PatternStrings[5] += "10001";
				PatternStrings[5] += "01110";

				PatternStrings[6]  = "01110";
				PatternStrings[6] += "10000";
				PatternStrings[6] += "10000";
				PatternStrings[6] += "11110";
				PatternStrings[6] += "10001";
				PatternStrings[6] += "10001";
				PatternStrings[6] += "01110";

				PatternStrings[7]  = "11111";
				PatternStrings[7] += "00001";
				PatternStrings[7] += "00001";
				PatternStrings[7] += "00010";
				PatternStrings[7] += "00100";
				PatternStrings[7] += "01000";
				PatternStrings[7] += "10000";

				PatternStrings[8]  = "01110";
				PatternStrings[8] += "10001";
				PatternStrings[8] += "10001";
				PatternStrings[8] += "01110";
				PatternStrings[8] += "10001";
				PatternStrings[8] += "10001";
				PatternStrings[8] += "01110";

				PatternStrings[9]  = "01110";
				PatternStrings[9] += "10001";
				PatternStrings[9] += "10001";
				PatternStrings[9] += "01111";
				PatternStrings[9] += "00001";
				PatternStrings[9] += "00001";
				PatternStrings[9] += "01110";

				PatternStrings[10]  = "01100";
				PatternStrings[10] += "10001";
				PatternStrings[10] += "10001";
				PatternStrings[10] += "00111";
				PatternStrings[10] += "00001";
				PatternStrings[10] += "00001";
				PatternStrings[10] += "01110";
			}



			/// <summary>
			/// The main entry point for the application.
			/// </summary>
			[STAThread]
			static void Main(string[] args)
			{

				//  Load Training Set
//				Pattern [] data = new Pattern[250];
//				System.IO.FileStream fstream;
//				
//				try
//				{
//					fstream = new System.IO.FileStream( "lin2var.trn", System.IO.FileMode.Open );
//				}
//				catch( Exception ex )
//				{
//					Console.WriteLine( "Failed to open pattern file." );
//					return;
//				}
//
//				System.IO.StreamReader sr = new System.IO.StreamReader( fstream );
//				for (int i=0; i<250; i++)
//					data[i]=new Pattern(2,1,sr);
//				fstream.Close();
//
//				// Create ADALINE
//				AdalineNetwork net = new AdalineNetwork(2, 0.45 ); // Two Input nodes, Learning rate 0.45
//
//				// Run network
//				double deltaError = 0.0;
//				for (int i=0; i<250; i++)
//				{
//					net.setNodeValue(data[i]);
//
//					net.Run();                // Run Network
//
//					deltaError += Math.Abs( net.getNodeValue() - data[i].getOut(0) );
////					Console.WriteLine( "{0}  Net: {1}  Actual: {2}",
////						i.ToString("D"),
////						net.getNodeValue().ToString("F7"),
////						data[i].getOut(0).ToString("F7") );
//				}
//
//				Console.WriteLine( "Total error: {0}  Average error: {1}  Error Percent: {2}",
//									deltaError,
//									deltaError/250,
//									((double)(deltaError/250)).ToString("P") );
//				// Train ADALINE
//				int iteration=0;
//				int good=0;
//
//				while (good<250)     // Train until all patterns are good
//				{
//					good=0;
//					for (int i=0; i<250; i++)
//					{
//						net.setNodeValue(data[i]);               // Set input node values
//
//						net.Run();                            // Run the Network
//
//						if (data[i].getOut(0) != net.getNodeValue()) // If Network produced an
//						{                              // error, perform
//							net.Learn();                     // Learnining function
//							break;
//						}
//						else good++;
//					}
//
//					Console.Write("iteration {0}.  {1}/250\r", iteration, good );
//					iteration++;
//				}
//
//				Console.WriteLine();
//
//				// Run ADALINE
//				deltaError = 0.0;
//				for (int i=0; i<250; i++)
//				{
//					net.setNodeValue(data[i]);
//
//					net.Run();                // Run Network
//
//					deltaError += Math.Abs( net.getNodeValue() - data[i].getOut(0) );
//					Console.WriteLine( "{0}  Net: {1}  Actual: {2}",
//										i.ToString("D"),
//										net.getNodeValue().ToString("F7"),
//										data[i].getOut(0).ToString("F7") );
//				}
//
//				Console.WriteLine( "Total error: {0}  Average error: {1}  Error Percent: {2}",
//					deltaError,
//					deltaError/250,
//					((double)(deltaError/250)).ToString("P") );

				CreateInputPatterns();
				CreateOutputValues();
				//
				// TODO: Add code to start application here
				//

				Console.WriteLine( "Adaline Network class test......" );
				AdalineNetwork [] net = new AdalineNetwork[10];
					
				for( int i=0; i<10; i++ )
				{
					net[i] = new AdalineNetwork( 7, 0.05 );
				}

				int netgood = 0;
				long iteration = 0;
				while (netgood<10)     // Train until all patterns are good
				{
					int good = 0;
					netgood = 0;
					for( int j=0; j<10; j++ )
					{
						good=0;
						for (int i=0; i<10; i++)
						{
							// let's train for a number one
							StringPattern inputPattern = new StringPattern( 7, 5, PatternStrings[i], OutputValues[i] );

							net[i].setNodeValue( inputPattern );		// Set input node values

							net[i].Run(0);											// Run the Network
							if ( inputPattern.getOut(i) != net[i].getNodeValue(AdalineNode.DefaultIndex) )	// If Network produced an
							{													// error, perform
								net[i].Learn(0);									// Learnining function
								break;
							}
							else good++;

							//Console.Write( "iteration {0}.  Step {1} in Pass {2} Of 10 Passes complete.  {3} good patterns detected\r", iteration, i%10, (int)i/10, good );
						}

						if( good == 10 )
							netgood++;

						Console.Write( "iteration {0}, {1} good patterns detected\r", iteration, good );
					}

					iteration++;
				}

				Console.WriteLine();
				Console.WriteLine("Testing with pattern 5, should fire on 5th iteration." );
				Console.WriteLine("Input values:");
				for (int i=0; i<10; i++)
				{
					for( int j=0; j<10; j++ )
					{
						StringPattern inputPattern = new StringPattern( 7, 5, PatternStrings[5],  OutputValues[5] );
						net[i].setNodeValue(inputPattern);

						net[i].Run(0);                // Run Network

						Console.WriteLine( "{0}  Net: {1}  Actual: {2}",
							i.ToString("D"),
							net[i].getNodeValue(AdalineNode.DefaultIndex).ToString("F7"),
							inputPattern.getOut(i).ToString("F7") );
					}
				}

//				Console.WriteLine( "Testing last pattern learned..." );
//				
//				StringPattern input = new StringPattern( PatternStrings[10], OutputValues[9] );
//				net[9].setNodeValue( input );
//				net[9].Run();
//				Console.WriteLine( "Output of network: {0}", net[9].getNodeValue() );
//
//				input = new StringPattern( PatternStrings[0], OutputValues[0] );
//				net[9].setNodeValue( input );
//				net[9].Run();
//				Console.WriteLine( "Output of network after invalid input: {0}", net[9].getNodeValue() );

				//NeuralNodeBase [] nodes = new NeuralNodeBase[5];

				//			for( int i=0; i<5; i++ )
				//			{
				//				nodes[i] = new FeedbackNode();
				//				nodes[i].Name = string.Format( "Node:{0}", i.ToString() );
				//			}
				//
				//			Console.WriteLine("Creatiung a network, it should look like this:");
				//			Console.WriteLine("(0)--\\");
				//			Console.WriteLine("(1)===\\");
				//			Console.WriteLine("		 (4)");
				//			Console.WriteLine("(2)===/");
				//			Console.WriteLine("(3)--/");


				//NeuralNodeBase [] nodes = new NeuralNodeBase[4];
				//NeuralLink [] Link = new NeuralLink[3];

				//nodes[0]=new InputNode();            // Create Nodes for Network
				//nodes[1]=new InputNode();	

				//nodes[0].Name = "Input1";
				//nodes[1].Name = "Input2";

				//nodes[2]=new BiasNode();
				//nodes[2].Name = "BiasNode";

				//nodes[3]=new AdalineNode( 0.45 );  // ADALINE node with learning rate of 0.45
				//nodes[3].Name = "AdalineNode";

				//Link[0]=new AdalineLink( nodes[0], nodes[3] );          // Create Links for Network
				//Link[1]=new AdalineLink( nodes[1], nodes[3] );
				//Link[2]=new AdalineLink( nodes[2], nodes[3] );

				//NeuralNodeBase.Link( nodes[0], nodes[3], Link[0] ); // Connect Network
				//NeuralNodeBase.Link( nodes[1], nodes[3], Link[1]);
				//NeuralNodeBase.Link( nodes[2], nodes[3], Link[2]);

				// add output links
				//			nodes[0].Link( nodes[4], LinkDirection.OUTPUT );
				//			nodes[1].Link( nodes[4], LinkDirection.OUTPUT );
				//			nodes[2].Link( nodes[4], LinkDirection.OUTPUT );
				//			nodes[3].Link( nodes[4], LinkDirection.OUTPUT );
				//
				//			// add input links
				//			nodes[4].Link( nodes[0], LinkDirection.INPUT );
				//			nodes[4].Link( nodes[1], LinkDirection.INPUT );
				//			nodes[4].Link( nodes[2], LinkDirection.INPUT );
				//			nodes[4].Link( nodes[3], LinkDirection.INPUT );

				// let's see
//				Console.WriteLine( "Examining Links....." );
//
//				for( int i=0; i<nodes.Length; i++ )
//				{
//					Console.WriteLine( "Node Index: {0}, Node Name: {1}", i, nodes[i].Name );
//					Console.WriteLine( "   Input Nodes ({0}):", nodes[i].InLinks.Count );
//					foreach( NeuralLink link in nodes[i].InLinks )
//					{
//						Console.WriteLine("      In Node:{0} Out Node:{1}", link.InNode.Name, link.OutNode.Name );
//					}
//
//					Console.WriteLine( "   Output Nodes ({0}):", nodes[i].OutLinks.Count );
//					foreach( NeuralLink link in nodes[i].OutLinks )
//					{
//						Console.WriteLine("      In Node:{0} Out Node:{1}", link.InNode.Name, link.OutNode.Name );
//					}
//
//				}
//
//				Console.WriteLine( "Press enter to continue....");
//				Console.Read();
			}
		}
	}
