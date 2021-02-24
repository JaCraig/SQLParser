using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;
using System.Collections.Generic;

namespace SQLParser.Tests
{
    public class DDLPrinter : TSqlParserBaseListener
    {
        public DDLPrinter()
        {
            SearchList = new List<string>();
        }

        public List<string> SearchList { get; set; }

        public override void EnterCreate_table([NotNull] TSqlParser.Create_tableContext context)
        {
            var TableName = context?.table_name().table.GetText();
            if (!string.IsNullOrEmpty(TableName))
                SearchList.Add(TableName);
            base.EnterCreate_table(context);
        }
    }
}