using System;

namespace NeuralNetLib
{
	public enum NodeValueIndex
	{
		IdxNodeError=0,
		IdxNodeValue=0,
		IdxNodeWeight=0,
		IdxLearningRate=1,
		IdxMomentum=2
	}

	/// <summary>
	/// Summary description for NeuralNetBase.
	/// </summary>
	///
	public abstract class NeuralNet : NeuralNodeBase
	{
		protected int NodeCount;					// Number of nodes in Network
		protected int LinkCount;					// Number of links in Network
		protected NeuralNodeBase [] Nodes;							// Array of base nodes
		protected NeuralLink [] Links;							// Array of base links

	    protected NeuralNet() : base( 0, 0 )
		{
			NodeCount = 0;
			LinkCount = 0;
		}

        public abstract void CreateNetwork();
        public abstract void LoadInputs();

		protected virtual void LoadLinksAndNodes(System.IO.Stream infile)	
		{
			var br = new System.IO.BinaryReader(infile);

			NodeCount = br.ReadInt32();
			LinkCount = br.ReadInt32();
			CreateNetwork();
			for( var i=0; i<NodeCount; i++ )
			{
				Nodes[i].Load(infile);
			}

			for( var i=0; i<LinkCount; i++ )
			{
				Links[i].Load(infile);
			}
		}

		protected virtual void SaveLinksAndNodes(System.IO.Stream outfile) 
		{
			var bw = new System.IO.BinaryWriter(outfile);

			bw.Write(NodeCount);
			bw.Write(LinkCount);
			for( var i=0; i<NodeCount; i++ )
			{
				Nodes[i].Save(outfile);
			}

			for( var i=0; i<LinkCount; i++ )
			{
				Links[i].Save(outfile);
			}
		}

		public override void Load(System.IO.Stream infile)	
		{
			LoadLinksAndNodes(infile);
		}
		
		public override void Save(System.IO.Stream outfile) 
		{
			SaveLinksAndNodes(outfile);
		}

		public override void Epoch() 
		{
			// runn Epoc for each node in network
			for( var i=0; i<Nodes.Length; i++ )
			{ 
				Nodes[i].Epoch();
			}

			// Run Epoch for each link in network
			for( var i=0; i<Links.Length; i++ )
			{
				Links[i].Epoch(0);
			}
		}

	}
}
