using System.Text.RegularExpressions;

var pattern = @"^(?=.{1,90}$)(?:build|feat|ci|chore|docs|fix|perf|refactor|revert|style|test)(?:\(.+\))*(?::).{4,}(?:#\d+)*(?<![\.\s])$";
var lines = File.ReadAllLines(Args[0]);
var header = lines.Length > 0 ? lines[0] : string.Empty;

if (Regex.IsMatch(header, pattern))
   return 0;

var reason = GetFailureReason(header);

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Invalid commit message");
Console.ResetColor();
Console.WriteLine($"Reason: {reason}");
Console.WriteLine("e.g: 'feat(scope): subject' or 'fix: subject'");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("more info: https://www.conventionalcommits.org/en/v1.0.0/");

return 1;

static string GetFailureReason(string header)
{
   if (string.IsNullOrWhiteSpace(header))
      return "The first line of the commit message is empty.";

   if (header.Length > 90)
      return $"The first line is {header.Length} characters; max allowed is 90.";

   if (Regex.IsMatch(header, @"[\.\s]$"))
      return "The subject must not end with a period or trailing whitespace.";

   if (!Regex.IsMatch(header, @"^(?:build|feat|ci|chore|docs|fix|perf|refactor|revert|style|test)(?:\(.+\))*:"))
      return "Header must start with a valid type, optional scope, and colon (for example: feat(scope):).";

   var colonIndex = header.IndexOf(':');
   if (colonIndex < 0 || header.Length - colonIndex - 1 < 4)
      return "Header must include at least 4 characters of subject text after the colon.";

   return "Header does not match the conventional commit format expected by this repository.";
}
