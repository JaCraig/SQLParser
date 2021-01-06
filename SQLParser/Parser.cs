/*
Copyright 2017 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SQLParser.Enums;
using SQLParser.Parsers.TSql;
using System.IO;

namespace SQLParser
{
    /// <summary>
    /// SQL parser class
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="listener">The listener.</param>
        /// <param name="sqlType">Type of the SQL.</param>
        public static void Parse(string input, IParseTreeListener listener, SQLType sqlType)
        {
            if (sqlType == SQLType.TSql)
                ParseTSQL(input, listener);
        }

        /// <summary>
        /// Parses the TSQL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="listener">The listener.</param>
        private static void ParseTSQL(string input, IParseTreeListener listener)
        {
            ICharStream Stream = CharStreams.fromString(input);
            Stream = new CaseChangingCharStream(Stream);
            ITokenSource Lexer = new TSqlLexer(Stream, TextWriter.Null, TextWriter.Null);
            ITokenStream Tokens = new CommonTokenStream(Lexer);
            TSqlParser Parser = new TSqlParser(Tokens, TextWriter.Null, TextWriter.Null)
            {
                BuildParseTree = true
            };
            IParseTree tree = Parser.tsql_file();
            ParseTreeWalker.Default.Walk(listener, tree);
        }
    }
}