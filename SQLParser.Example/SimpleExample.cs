using SQLParser.Parsers.TSql;
using System.Diagnostics.CodeAnalysis;

namespace SQLParser.Example
{
    /// <summary>
    /// This is an example of how to use the parser to detect a SELECT statement.
    /// </summary>
    public class SimpleExample
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            // Create a printer to store the results of the parse
            var ExamplePrinter = new Printer();

            // Parse a statement and pass the printer to the parser
            Parser.Parse("SELECT * FROM Somewhere", ExamplePrinter, Enums.SQLType.TSql);

            // ExamplePrinter.StatementFound is now true if the statement is a SELECT statement
            Console.WriteLine(ExamplePrinter.StatementFound);
        }

        /// <summary>
        /// This is an example of a printer that can be used to parse a statement.
        /// </summary>
        /// <seealso cref="TSqlParserBaseListener"/>
        internal class Printer : TSqlParserBaseListener
        {
            /// <summary>
            /// Gets or sets a value indicating whether [statement found].
            /// </summary>
            /// <value><c>true</c> if [statement found]; otherwise, <c>false</c>.</value>
            public bool StatementFound { get; set; }

            /// <summary>
            /// Enter a parse tree produced by <see cref="M:SQLParser.Parsers.TSql.TSqlParser.dml_clause"/>.
            /// <para>The default implementation does nothing.</para>
            /// </summary>
            /// <param name="context">The parse tree.</param>
            public override void EnterDml_clause([NotNull] TSqlParser.Dml_clauseContext context)
            {
                // This is a select statement if the select_statement_standalone is not null
                StatementFound |= context.select_statement_standalone() != null;
                base.EnterDml_clause(context);
            }
        }
    }
}