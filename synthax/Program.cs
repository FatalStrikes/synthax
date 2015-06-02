using System;
using System.IO;
using System.Collections.Generic;

namespace synthax
{	
	class MainClass
	{


		public static List<Token> tokens_list = new List<Token>();
		public static int node_index = 0;

		public static void Main (string[] args)
		{
			if (args.Length == 0) {
				Console.Write ("No File Found");
				return;
			}
				

			int counter = 0; // FOR DEBUG
			string line;


			// Read the file and display it line by line and add each token found into tokens_list.
			System.IO.StreamReader file = new System.IO.StreamReader(args.GetValue (0).ToString());
			while((line = file.ReadLine()) != null)
			{
				// FOR DEBUG
				// Console.WriteLine (line);
				counter++;


				string[] line_split = line.Split (' ');
				// Tests if string exists in enum
				TknType t;
				Enum.TryParse (line_split [0], out t);			
				tokens_list.Add( new Token (t, line_split [2]) );


			}
			file.Close();


//			// FOR DEBUG
//			foreach (var item in tokens_list) {
//				Console.WriteLine (item.toString ());
//			}
				
			// FOR DEBUG
			// Console.WriteLine (counter);



			Node tree = new Node ();
			if (tokens_list[node_index].type != TknType.TKN_PROGRAM) {
				Console.WriteLine ("Error: Program not first Token");
				return;
			} 

			// TIME TO ANALYSE AND GENERATE NODE STRUCTURE

			tree = nodeProgram ();

			// printTree (tree);
			Console.WriteLine (" End of program...");
			return;
		}

		public static Node nodeProgram (){
			Node main = new Node ();
			main.node_content = tokens_list [node_index];
			expect (TknType.TKN_LBRACK);
			Node temp = nodeListaDeclaraciones ();
			main.node_successor = temp;
			expect (TknType.TKN_RBRACK);
			return main;
		}

		public static Node nodeListaDeclaraciones(){
			Node node = new Node ();
			Token nextToken = tokens_list [++node_index];

			Node raiz = new Node ();
			while (nextToken.type >= TknType.TKN_FLOAT && nextToken.type >= TknType.TKN_BOOL) {
				Node temp = nodeDeclaraciones ();
				expect (TknType.TKN_SEMICOLON);

				raiz.node_successor = temp;
				raiz = temp
				nextToken = tokens_list [++node_index];
			}

			return node;
		}

//		public static Node nodeProgram (){
//			Node main = new Node ();
//			main.node_content = tokens_list [node_index];
//			Console.WriteLine ("-> program");
//			expect (TknType.TKN_LBRACK);
//			main.node_successor = nodeListaDeDeclaraciones ();
//			// main.node_successor2 = nodeListaDeSentencias ();
//			expect (TknType.TKN_RBRACK);
//			return main;
//		}
//
//
//		// CHECK NODO DE DECLARACIONES Y SECUENCIOAS!!!
//		public static Node nodeListaDeDeclaraciones(){
////			Console.WriteLine ("NODO LISTA DE DECLARACIONES");
//			Node node = new Node ();
//			node.node_content = new Token (TknType.TKN_LISTADEC, "");
//			while (tokens_list[node_index+1].type > TknType.TKN_FLOAT && tokens_list[node_index+1].type < TknType.TKN_BOOL) {
//				node.node_successor = nodeDeclaracion ();
//			}
//			node.depth++;
//			return node;
//		}
//
//		public static Node nodeDeclaracion(){
////			Console.WriteLine ("NODO DELCARACION");
//			Node node = new Node ();
//			node.node_content = tokens_list [++node_index];
//			Console.Write ("-> " + new string(node.node_content.lexema) + "\r\n");
//			expect (TknType.TKN_ID);
//			node.node_successor = nodeId ();
//			expect (TknType.TKN_SEMICOLON);
//			return node;
//		}
//
//		public static Node nodeId(){
////			Console.WriteLine ("NODO ID");
//			Node node = new Node ();
//			node.node_content = tokens_list [node_index];
//			Console.Write ("-> " + new string(node.node_content.lexema) + "\r\n");
//			return node;
//		}

		public static bool expect(TknType t){
			if (tokens_list [++node_index].type == t) {
				return true;
			} else {
				Error ("Error Expected token: " + t);
				return false;
			}
		}


		public static void Error(string s){
			Console.WriteLine (s + "...");
//			Console.ReadKey(true);
//			Environment.Exit (1);
		}


			public static void printTree (Node tree){
				for (int i = 0; i < tree.depth; i++) {
					Console.Write ("-");
				}
				Console.Write ( "->" + tree.node_content.type + "\r\n");
				if (tree.node_successor != null) {
					printTree (tree.node_successor);
				}
			}
	}
}

