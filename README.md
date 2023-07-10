## SQLParser

This C# library provides a SQL parser and lexer implementation using ANTLR. It allows you to parse SQL queries into an abstract syntax tree (AST) and perform various operations on the parsed queries.

## Features

- Lexical analysis: Tokenizing SQL queries into individual tokens.
- Syntactic analysis: Parsing SQL queries into an abstract syntax tree.
- Query manipulation: Modifying and transforming the parsed SQL queries.
- Query analysis: Extracting metadata and information from SQL queries.

## Installation

You can install the library via NuGet:

```
dotnet add package SQLParser
```

## Usage

To use this library in your C# project, follow these steps:

1. Add a reference to the `SQLParser` package in your project.
2. Import the `SQLParser` namespace in your code:

   ```csharp
   using SQLParser.Parsers.TSql;
   using SQLParser;
   ```

3. Create a parser listener class:

    ```csharp
    /// <summary>
    /// This is an example of a printer that can be used to parse a statement.
    /// </summary>
    /// <seealso cref="TSqlParserBaseListener" />
    internal class Printer : TSqlParserBaseListener
    {
        /// <summary>
        /// Gets or sets a value indicating whether [statement found].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [statement found]; otherwise, <c>false</c>.
        /// </value>
        public bool StatementFound { get; set; }

        /// <summary>
        /// Enter a parse tree produced by <see cref="M:SQLParser.Parsers.TSql.TSqlParser.dml_clause" />.
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

    ```

4. Parse the query:

   ```csharp
   Parser.Parse("SELECT * FROM Somewhere", ExamplePrinter, Enums.SQLType.TSql);
   ```

## Contributing

Contributions are welcome! If you encounter any bugs, issues, or have feature requests, please [create an issue](https://github.com/JaCraig/SQLParser/issues) on this repository.

If you want to contribute to the codebase, follow these steps:

1. Fork the repository.
2. Create a new branch for your feature/bug fix.
3. Make your changes and write tests if applicable.
4. Submit a pull request.

Please ensure that your code follows the existing code style and passes the tests before submitting a pull request.

## License

This library is released under the [Apache 2 License](https://github.com/JaCraig/SQLParser/blob/master/LICENSE).

## Acknowledgments

- This library was built using ANTLR (version 4).