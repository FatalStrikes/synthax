﻿using System;

namespace synthax
{
	public enum TknType
	{
		TKN_NaN,
		// TOKENS COMPARACION
		TKN_IF,TKN_THEN,TKN_ELSE,TKN_FI,
		// TOKENS CICLO
		TKN_DO,TKN_UNTIL,TKN_WHILE,
		// TOKENS IO
		TKN_READ,TKN_WRITE,
		// TOKENS TIPO
		TKN_FLOAT,TKN_INT,TKN_BOOL,
		// TOKENS PROGRAM
		TKN_PROGRAM,
		// TOKENS COMPARATORS
		TKN_AND,TKN_OR,TKN_NOT,
		// TOKENS SIMBOLOS
		TKN_ADD,TKN_MINUS,TKN_MULTI,TKN_DIV,TKN_LESS,TKN_LESSEQ,TKN_GRT,TKN_GRTEQ,TKN_EQUALS,TKN_SEMICOLON,TKN_COMA,TKN_LPAREN,TKN_RPAREN,TKN_LBRACK,TKN_RBRACK,TKN_COMPARE,
		// TOKENS ESPECIALES
		TKN_ID,TKN_NUM,TKN_ASSIGN,TKN_EOF,
		// TOKENS AUX
		TKN_LISTADEC, TKN_DEC
	}

	public enum State{
		IN_START, IN_ID,      IN_NUM, IN_LPAREN,  IN_RPAREN,  IN_SEMICOLON, IN_RBRACKET, IN_LBRACKET,
		IN_COMMA, IN_ASSIGN,  IN_ADD, IN_MINUS,   IN_EOF,     IN_ERROR,   IN_DONE,  IN_MULTI, IN_DIV,
		IN_COMPARE,//, IN_GREATER, IN_LESS
		IN_COMMENT
	}

	public class Token
	{
		public TknType type;
		public char[] lexema = new char[70];

		public Token(){

		}

		public Token(TknType t ,string l){
			type = t;
			lexema = l.ToCharArray();
		}

		public string toString(){
			return this.type + " --> " + new string (this.lexema);
		}
	}
}

