namespace SQLParser.Example
{
    /// <summary>
    /// This is an example of how to use the parser.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            int Choice;
            while (true)
            {
                Console.WriteLine("Which example would you like to run?");
                Console.WriteLine("1. Simple Example");
                Console.WriteLine("2. Where Clause Parsing Example");
                if (int.TryParse(Console.ReadLine(), out Choice))
                    break;
            }
            switch (Choice)
            {
                case 1:
                    new SimpleExample().Run();
                    break;

                case 2:
                    new WhereClauseExample().Run();
                    break;
            }
        }
    }
}