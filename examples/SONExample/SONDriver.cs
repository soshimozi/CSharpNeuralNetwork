using System;
using NeuralNetLib;

namespace SONExample
{ 

	/// <summary>
	/// Test driver class for the SON network utilizing kohonen layers
	/// </summary>
	class SONDriver
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			var reader = new System.IO.StreamReader("son.trn");
			var data = new Pattern[300];
			for( var i=0; i<300; i++ )
			{
				data[i] = new Pattern(2, 0, reader);
			}
			reader.Close();


		    var iteration = 0;
			const double initialLearningRate = 0.5;
		    const double finalLearningRate = 0.01;
		    const int initialNeighSize = 5;
			const int neighborhoodDecrementInterval = 1000;
			const int numIterations = 5000;
			const int frameRate = 500;

			var son = new SONNetwork(2, 10, 10, initialLearningRate, finalLearningRate,
														initialNeighSize, neighborhoodDecrementInterval,
														numIterations);

            // always have to create network or nothing works!
            son.CreateNetwork();

			var outstream = new System.IO.FileStream("output.dta", System.IO.FileMode.Create);
			son.Save(outstream);

			for( iteration=0; iteration<numIterations; iteration++ )
			{
				// preset pattern set
				for( var i=0; i<300; i++ )
				{
					son.SetNodeValue(data[i]);
					son.Run();
					son.Learn();
				}

				son.Epoch();

				if((iteration%10)==0) // print status
				{
					Console.WriteLine( string.Format("Iteration: {0}.  Learning Rate: {1}  Neighborhood: {2}", iteration, son.GetLearningRate(), son.GetNeighborhoodSize() ) );
				}

				if((iteration%frameRate)==0) // store link values
				{
					son.Save(outstream);
				}

			}

			outstream.Close();
		}
	}
}
