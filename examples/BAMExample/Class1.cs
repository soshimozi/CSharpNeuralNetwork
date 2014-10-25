using System;
using NeuralNetLib;

namespace ConsoleApplication1
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Driver
	{
		static string [] PatternStrings = new string[10];

		static void CreateInputPatterns()
		{
			PatternStrings[0]  = " OOO ";
			PatternStrings[0] += "O   O";
			PatternStrings[0] += "O   O";
			PatternStrings[0] += "O   O";
			PatternStrings[0] += "O   O";
			PatternStrings[0] += "O   O";
			PatternStrings[0] += " OOO ";
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			CreateInputPatterns();
			//
			// TODO: Add code to start application here
			//

			Console.WriteLine( "Network class test......" );

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


			NeuralNodeBase [] nodes = new NeuralNodeBase[4];
			NeuralLink [] Link = new NeuralLink[3];

			nodes[0]=new InputNode();            // Create Nodes for Network
			nodes[1]=new InputNode();	

			nodes[0].Name = "Input1";
			nodes[1].Name = "Input2";

			nodes[2]=new BiasNode();
			nodes[2].Name = "BiasNode";

			nodes[3]=new AdalineNode( 0.45 );  // ADALINE node with learning rate of 0.45
			nodes[3].Name = "AdalineNode";

			Link[0]=new AdalineLink( nodes[0], nodes[3] );          // Create Links for Network
			Link[1]=new AdalineLink( nodes[1], nodes[3] );
			Link[2]=new AdalineLink( nodes[2], nodes[3] );

			NeuralNodeBase.Link( nodes[0], nodes[3], Link[0] ); // Connect Network
			NeuralNodeBase.Link( nodes[1], nodes[3], Link[1]);
			NeuralNodeBase.Link( nodes[2], nodes[3], Link[2]);

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
			Console.WriteLine( "Examining Links....." );

			for( int i=0; i<nodes.Length; i++ )
			{
				Console.WriteLine( "Node Index: {0}, Node Name: {1}", i, nodes[i].Name );
				Console.WriteLine( "   Input Nodes ({0}):", nodes[i].InLinks.Count );
				foreach( NeuralLink link in nodes[i].InLinks )
				{
					Console.WriteLine("      In Node:{0} Out Node:{1}", link.InNode.Name, link.OutNode.Name );
				}

				Console.WriteLine( "   Output Nodes ({0}):", nodes[i].OutLinks.Count );
				foreach( NeuralLink link in nodes[i].OutLinks )
				{
					Console.WriteLine("      In Node:{0} Out Node:{1}", link.InNode.Name, link.OutNode.Name );
				}

			}

			Console.WriteLine( "Press enter to continue....");
			Console.Read();
		}
	}
}
