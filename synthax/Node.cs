using System;

namespace synthax
{
	public class Node
	{
		public Token node_content;
		public Node[] node_successor = new Node[3]();
		public Node node_brother;
		public int depth = 0;

		public Node (){
			this.node_brother = null;
			this.node_successor = null;
		}
	}

//	public class ProgramNode : Node{
//		public Node listadedeclaraciones;
//		public Node listadesentencias;
//		public ProgramNode(){
//			this.listadesentencias = null;
//			this.listadedeclaraciones = null;
//		}
//	}
//
//	public class DeclaracionNode : Node
//	{
//		public Token id;
//		public Token tipo;
//
//		public DeclaracionNode(){
//			this.id = null;
//			this.tipo = null;
//		}
//	}
}

