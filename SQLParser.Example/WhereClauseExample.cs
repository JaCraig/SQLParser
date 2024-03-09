using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;

namespace SQLParser.Example
{
    /// <summary>
    /// This is an example of how to use the parser to parse out the where clause of a select statement.
    /// </summary>
    public class WhereClauseExample
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            // Create a printer to store the results of the parse
            var ExamplePrinter = new Printer();

            // Parse a statement and pass the printer to the parser
            Parser.Parse("SELECT * FROM Somewhere WHERE MyColumn = @parameter1 AND MySecondColumn = @parameter2", ExamplePrinter, Enums.SQLType.TSql);

            // ExamplePrinter.WhereClause is now the where clause of the statement
            Console.WriteLine(ExamplePrinter.WhereClause);
        }

        /// <summary>
        /// This is an example of a printer that can be used to parse a statement.
        /// </summary>
        /// <seealso cref="TSqlParserBaseListener"/>
        internal class Printer : TSqlParserBaseListener
        {
            /// <summary>
            /// The final where clause found in the statement.
            /// </summary>
            public string? WhereClause { get; set; }

            /// <summary>
            /// The select statement is found in the query specification. For our purposes, we only
            /// need to check if there is a where clause and if so, store it.
            /// </summary>
            /// <param name="context">The query context object</param>
            public override void EnterQuery_specification([NotNull] TSqlParser.Query_specificationContext context)
            {
                if (context.where is null)
                    return;
                var StartIndex = context.where.Start.StartIndex;
                var StopIndex = context.where.Stop.StopIndex;
                var WhereClauseInterval = new Interval(StartIndex, StopIndex);
                WhereClause = context.Start.InputStream.GetText(WhereClauseInterval);
            }
        }
    }
}