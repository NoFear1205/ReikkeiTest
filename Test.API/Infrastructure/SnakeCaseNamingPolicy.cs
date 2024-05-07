using System.Text.Json;
using System.Text.RegularExpressions;

namespace Test.API.Infrastructure
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            Regex.Replace(name, "(?<!^)[A-Z]", "_$0").ToLower();
    }
}
