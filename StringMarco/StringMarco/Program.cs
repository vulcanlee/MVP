using System.Text.RegularExpressions;

namespace StringMarco
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // https://stackoverflow.com/questions/27894289/regex-to-validate-macros-in-text
            string str = @"Some text (* Invalid Macro Insert some text (*ValidMacroInsert*)
Some text Invalid Macro Insert *) some text (*ValidMacroInsert*)";
            string result1 = Regex.Replace(str, @"(?m)\(\*(?=(?:(?!\(\*|\*\)).)*\(|$)", "<");
            string result2 = Regex.Replace(result1, @"(?<!\(\*(?:(?!\(\*|\*\)).)*)\*\)", ">");
            Console.WriteLine(result2);

            var result3 = Regex.Replace("Godan verb with ru ending, Transitive Verb", "(v)erb|(end)ing", "$1$2", RegexOptions.IgnoreCase);
            Console.WriteLine(result3);

            {
                // Consider this string - Note that it may be much more complicated, with many more matches
                string output = "\"$type\": \"SomeType\",";
                // The variable to search and replace - In this case is set to "SomeType"
                string myType = "SomeType";
                output = Regex.Replace(output, $@"(?<=""\$type"":\s""){Regex.Escape(myType)}(?="",)", "NewType");
                Console.WriteLine(output);
            }
        }
    }
}