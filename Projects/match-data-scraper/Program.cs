using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json; 

await FetchAndParseDataAsync("https://tracklock.gg/players/94516027"); 

async Task FetchAndParseDataAsync(string url)
{
    // todo;
    using HttpClient client = new(); 
    try
    {
        var response = await client.GetStringAsync(url);
        //Console.WriteLine(response);
        // \{[^{}]*\"is_win\\"[^{}]*\}
        //string pattern = @"\{[^{}]*\"is_win\\"[^{}]*\}";
        string pattern =   @"\{[^{}]*\""is_win\\\""[^{}]*\}";
        Regex regex = new Regex(pattern, RegexOptions.Multiline);

        MatchCollection matches = regex.Matches(response);
        foreach(Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
        Console.WriteLine($"Found {matches.Count} matches");
    } catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
