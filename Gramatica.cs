
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    #region TERMINALES Y NO TERMINALES
    enum SymbolConstants : int
    {
        SYMBOL_EOF        =  0, // (EOF)
        SYMBOL_ERROR      =  1, // (Error)
        SYMBOL_WHITESPACE =  2, // Whitespace
        SYMBOL_MINUS      =  3, // '-'
        SYMBOL_LPAREN     =  4, // '('
        SYMBOL_RPAREN     =  5, // ')'
        SYMBOL_TIMES      =  6, // '*'
        SYMBOL_COMMA      =  7, // ','
        SYMBOL_DIV        =  8, // '/'
        SYMBOL_PLUS       =  9, // '+'
        SYMBOL_DECIMAL    = 10, // Decimal
        SYMBOL_ENTERO     = 11, // Entero
        SYMBOL_SQRT       = 12, // sqrt
        SYMBOL_TAN        = 13, // tan
        SYMBOL_E          = 14, // <E>
        SYMBOL_F          = 15, // <F>
        SYMBOL_G          = 16, // <G>
        SYMBOL_H          = 17, // <H>
        SYMBOL_T          = 18  // <T>
    };

    enum RuleConstants : int
    {
        RULE_E_PLUS                     =  0, // <E> ::= <E> '+' <T>
        RULE_E_MINUS                    =  1, // <E> ::= <E> '-' <T>
        RULE_E                          =  2, // <E> ::= <T>
        RULE_T_TIMES                    =  3, // <T> ::= <T> '*' <F>
        RULE_T_DIV                      =  4, // <T> ::= <T> '/' <F>
        RULE_T                          =  5, // <T> ::= <F>
        RULE_F_LPAREN_RPAREN            =  6, // <F> ::= '(' <E> ')'
        RULE_F_ENTERO                   =  7, // <F> ::= Entero
        RULE_F_DECIMAL                  =  8, // <F> ::= Decimal
        RULE_F                          =  9, // <F> ::= <G>
        RULE_G_SQRT_LPAREN_COMMA_RPAREN = 10, // <G> ::= sqrt '(' <E> ',' <E> ')'
        RULE_G                          = 11, // <G> ::= <H>
        RULE_H_TAN_LPAREN_RPAREN        = 12  // <H> ::= tan '(' <E> ')'
    };
    #endregion

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            parser.Parse(source);

        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        #region TOKENS TERMINALES
        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                return token.Text;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                return token.Text;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                return token.Text;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                return token.Text;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                return token.Text;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                return token.Text;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                return token.Text;

                case (int)SymbolConstants.SYMBOL_DECIMAL :
                //Decimal
                return token.Text;

                case (int)SymbolConstants.SYMBOL_ENTERO :
                //Entero
                return token.Text;

                case (int)SymbolConstants.SYMBOL_SQRT :
                //sqrt
                return token.Text;

                case (int)SymbolConstants.SYMBOL_TAN :
                //tan
                return token.Text;

                case (int)SymbolConstants.SYMBOL_E :
                //<E>
                return null;

                case (int)SymbolConstants.SYMBOL_F :
                //<F>
                return null;

                case (int)SymbolConstants.SYMBOL_G :
                //<G>
                return null;

                case (int)SymbolConstants.SYMBOL_H :
                //<H>
                return null;

                case (int)SymbolConstants.SYMBOL_T :
                //<T>
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        #endregion
        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        #region TOKENS NO TERMINALES
        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_E_PLUS :
                //<E> ::= <E> '+' <T>
                    return Convert.ToInt32(token.Tokens[0].UserObject) + Convert.ToInt32(token.Tokens[2].UserObject);

                case (int)RuleConstants.RULE_E_MINUS :
                //<E> ::= <E> '-' <T>
                    return Convert.ToInt32(token.Tokens[0].UserObject) - Convert.ToInt32(token.Tokens[2].UserObject);

                case (int)RuleConstants.RULE_E :
                //<E> ::= <T>
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_T_TIMES :
                //<T> ::= <T> '*' <F>
                return Convert.ToInt32(token.Tokens[0].UserObject) * Convert.ToInt32(token.Tokens[2].UserObject);

                case (int)RuleConstants.RULE_T_DIV :
                //<T> ::= <T> '/' <F>
                return Convert.ToInt32(token.Tokens[0].UserObject) / Convert.ToInt32(token.Tokens[2].UserObject);

                case (int)RuleConstants.RULE_T :
                //<T> ::= <F>
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_F_LPAREN_RPAREN :
                    //<F> ::= '(' <E> ')'
                    return token.Tokens[1].UserObject;

                case (int)RuleConstants.RULE_F_ENTERO :
                    //<F> ::= Entero
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_F_DECIMAL:
                    //<F> ::= Decimal
                return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_F :
                    //<F> ::= <G>
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_G_SQRT_LPAREN_COMMA_RPAREN:
                    //<G> ::= sqrt '(' <E> ',' <E> ')'
                    double valor = Convert.ToDouble(token.Tokens[2].UserObject); // Valor al que se debe sacar la raiz
                    double n = Convert.ToDouble(token.Tokens[4].UserObject); // El segundo <E> es el índice de la raíz (La Enesima)
                    double resultado = Math.Pow(valor, 1.0 / n);

                    return resultado;

                case (int)RuleConstants.RULE_G :
                    //<G> ::= <H>
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_H_TAN_LPAREN_RPAREN:
                    //<H> ::= tan '(' <E> ')'
                    double anguloEnGrados = Convert.ToDouble(token.Tokens[2].UserObject); // VALOR DEL ANGULO A INGRESAR
                    double anguloEnRadianes = anguloEnGrados * Math.PI / 180; // CONVIERTE A RADIANES
                    double tangente = Math.Tan(anguloEnRadianes); // CALCULA LA TANGENTE
                    return tangente;


            }
            throw new RuleException("Unknown rule");
        }
        #endregion

        public string resultado;
        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            try
            {
                resultado = Convert.ToString(args.Token.UserObject);
            }
            catch (Exception e)
            {
                resultado = "Error";
            }
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            resultado = "Error Léxico: " + args.Token.ToString();
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            resultado = "Error Sintáctico: " + args.UnexpectedToken.ToString();
        }


    }
}
