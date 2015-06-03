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
				

			/*
			 * 
			 * FOR DEBUG PURPOSES ONLY
			 * 
			 * 
			 * 
			 * 
			 * 
			 * 
			 * 
			*/






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




			if (tokens_list[node_index].type != TknType.TKN_PROGRAM) {
				Console.WriteLine ("Error: Program not first Token");
				return;
			} 

			// TIME TO ANALYSE AND GENERATE NODE STRUCTURE
			Node tree = nodeProgram ();

			printTree (tree);
			Console.WriteLine ("\r\n\r\nAnalysis completed...\r\nEnd of program...");
			return;
		}

		public static Node nodeProgram (){
			Node main = new Node ();
			Console.WriteLine (node_index);
			main.node_content = tokens_list [node_index];
			expect (TknType.TKN_LBRACK);
			Node temp = nodeDeclarationList ();
			main.node_successor[0] = temp;
			temp = nodeSentenceList ();
			main.node_successor[1] = temp;
			Console.WriteLine (node_index);
			expect (TknType.TKN_RBRACK);
			return main;
		}

		public static Node nodeSentenceList(){
			Node node = new Node ();
			Token nextToken = tokens_list [++node_index];
			Node root = new Node ();
			if (nextToken.type != TknType.TKN_EOF) {
				root = nodeSentence ();
			}else{
				return null;
			}

			node = root;

			nextToken = tokens_list [++node_index];
			while (nextToken.type != TknType.TKN_RBRACK) {
				Node temp = nodeSentence ();

				root.node_brother = temp;
				root = temp;
				nextToken = tokens_list [++node_index];
			}
			node_index--;
			return node;
		}

		public static Node nodeSentence(){
			Node node = new Node ();
			Token t = tokens_list [node_index];
			Node temp = new Node ();
			switch (t.type) {

			case TknType.TKN_IF:
				temp = nodeIf ();
				break;
			case TknType.TKN_THEN:
				temp = nodeThen ();
				break;
//			case TknType.TKN_WRITE:
			case TknType.TKN_READ:	
				temp = nodeRead ();
				break;
			

			default:
				break;
			}


			node = temp;
			return node;
		}

		public static Node nodeIf(){
			Node node = new Node ();
			Token t = tokens_list [node_index];
			node.node_content = t;
			expect (TknType.TKN_LPAREN);
			// TODO: b-expresions
			expect (TknType.TKN_RPAREN);
//			expect (TknType.TKN_THEN);
//			expect (TknType.TKN_LBRACK);
//			// TODO: lista de sentencias
//			expect (TknType.TKN_RBRACK);
//			//TODO: ELSE
//			expect(TknType.TKN_FI);
			return node;
		}

		public static Node nodeThen(){
			Node node = new Node ();
			node.node_content = tokens_list [node_index];
			Token t = tokens_list [node_index];
			expect (TknType.TKN_LBRACK);
			// TODO: lista de sentencias
			expect (TknType.TKN_RBRACK);
			return node;
		}

		public static Node nodeRead(){
			Node node = new Node ();
			Token t = tokens_list [node_index];
			node.node_content = t;
			expect (TknType.TKN_ID);
			node.node_successor [0] = new Node (tokens_list [node_index]);
			expect (TknType.TKN_SEMICOLON);
			return node;
		}

		public static Node nodeDeclarationList(){
			Node node = new Node ();
			Token nextToken = tokens_list [++node_index];
			Node root = new Node ();
			if (nextToken.type == TknType.TKN_FLOAT || nextToken.type == TknType.TKN_BOOL || nextToken.type == TknType.TKN_INT) {
				root = nodeDeclaration ();
				expect (TknType.TKN_SEMICOLON);
			}else{
				return null;
			}

			node = root;

			nextToken = tokens_list [++node_index];
			while (nextToken.type == TknType.TKN_FLOAT || nextToken.type == TknType.TKN_BOOL || nextToken.type == TknType.TKN_INT) {
				Node temp = nodeDeclaration ();
				expect (TknType.TKN_SEMICOLON);

				root.node_brother = temp;
				root = temp;
				nextToken = tokens_list [++node_index];
			}
			node_index--;
			return node;
		}

		public static Node nodeDeclaration(){
			Node node = new Node ();
			node.node_content = tokens_list [node_index];;
			if (!expect (TknType.TKN_ID)) {
				return node;
			}
			node.node_successor [0] = new Node (tokens_list [node_index]);
			return node;
		}


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
			if (tree != null) {

				if (tree.node_content != null) {
					Console.WriteLine ("->" + tree.node_content.type + "  " + new string (tree.node_content.lexema) + "\r\n");
				}

				if (tree.node_successor != null) {
					Console.WriteLine ("Printing succesor");
					foreach (var item in tree.node_successor) {
						printTree (item);
					}
				}	

				if (tree.node_brother != null) {
					Console.WriteLine ("Printing brother");
					printTree (tree.node_brother);
				}

			}
			return;
		}
}
}

