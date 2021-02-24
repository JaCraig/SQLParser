using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;
using System.Collections.Generic;

namespace SQLParser.Tests
{
    public class WherePrinter : TSqlParserBaseListener
    {
        public WherePrinter()
        {
            SearchList = new List<string>();
        }

        public List<string> SearchList { get; set; }

        public override void EnterExpression([NotNull] TSqlParser.ExpressionContext context)
        {
            var LocalID = context?.primitive_expression()?.LOCAL_ID()?.GetText();
            if (!string.IsNullOrEmpty(LocalID))
                SearchList.Add(LocalID.Replace("@", ""));
            base.EnterExpression(context);
        }
    }
}