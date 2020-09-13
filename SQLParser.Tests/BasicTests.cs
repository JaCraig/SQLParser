using Antlr4.Runtime.Misc;
using SQLParser.Parsers.TSql;
using System.Collections.Generic;
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
            TestPrinter = new Printer();
            SQLParser.Parser.Parse("ALTER TABLE [dbo].[SelectOption_] ADD FOREIGN KEY ([User_Creator_ID_]) REFERENCES [dbo].[User_]([ID_])", TestPrinter, Enums.SQLType.TSql);
            Assert.False(TestPrinter.StatementFound);
        }

        [Fact]
        public void WhereClause()
        {
            var TestPrinter = new WherePrinter();
            SQLParser.Parser.Parse(@"SELECT [ID_],[UserName_],[StartDate_],[EndDate_],[EmployeeNumber_]
      ,[OrientationDate_],[FirstName_],[LastName_],[Title_],[MiddleName_]
      ,[NickName_],[Prefix_],[Suffix_],[Active_]
FROM [User_]
where [UserName_]=@UserName", TestPrinter, Enums.SQLType.TSql);
            Assert.Single(TestPrinter.SearchList);
            Assert.Equal("UserName", TestPrinter.SearchList[0]);
        }

        [Fact]
        public void WhereLikeClause()
        {
            var TestPrinter = new WherePrinter();
            SQLParser.Parser.Parse(@"SELECT [ID_],[UserName_],[StartDate_],[EndDate_],[EmployeeNumber_]
      ,[OrientationDate_],[FirstName_],[LastName_],[Title_],[MiddleName_]
      ,[NickName_],[Prefix_],[Suffix_],[Active_]
FROM [User_]
where [UserName_] LIKE @UserName+'%'", TestPrinter, Enums.SQLType.TSql);
            Assert.Single(TestPrinter.SearchList);
            Assert.Equal("UserName", TestPrinter.SearchList[0]);
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