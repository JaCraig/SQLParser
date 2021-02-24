using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;

namespace SQLParser.Tests
{
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