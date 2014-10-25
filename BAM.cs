namespace NeuralNetLib
{
	public class BAMLink : AdalineLink            // Link for BAM Node
	 {
		public BAMLink()
		{
			this[WeightIndex] = 0.0; // Initialize weight to 0.0
		}

		public BAMLink( NeuralNodeBase inNode, NeuralNodeBase outNode ) : base( inNode, outNode )
		{
			this[WeightIndex] = 0.0; // Initialize weight to 0.0
		}

		public override string ToString()
		{
			return "BAMLink";
		}
	 }



}
