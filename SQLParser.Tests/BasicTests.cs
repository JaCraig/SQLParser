using Xunit;

namespace SQLParser.Tests
{
    public class BasicTests
    {
        [Fact]
        public void DDLParseTest()
        {
            var TestPrinter = new DDLPrinter();
            SQLParser.Parser.Parse(@"CREATE TABLE HUB_POLICY
(
POLICY_HK BINARY() NOT NULL,
LOAD_TIMESTAMP TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
RECORD_SOURCE VARCHAR(100) NULL,
POLICY_NUMBER VARCHAR(50) NULL
);", TestPrinter, Enums.SQLType.TSql);
            Assert.Single(TestPrinter.SearchList);
            Assert.Equal("HUB_POLICY", TestPrinter.SearchList[0]);
        }

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
}