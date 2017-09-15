using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;
using Xunit;

namespace SQLParser.Tests
{
    public class BasicTests
    {
        [Fact]
        public void Test()
        {
            var TestPrinter = new Printer();
            SQLParser.Parser.Parse("SELECT * FROM Somewhere", TestPrinter, Enums.SQLType.TSql);
            Assert.True(TestPrinter.StatementFound);
        }
    }

    public class Printer : TSqlParserBaseListener
    {
        public bool StatementFound { get; set; }

        public override void EnterDml_clause([NotNull] TSqlParser.Dml_clauseContext context)
        {
            StatementFound |= context.select_statement() != null;
            base.EnterDml_clause(context);
        }
    }
}