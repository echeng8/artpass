using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace match_data_scraper
{
    /// <summary>
    /// Fetches match data from TrackLock.gg profile url
    /// This is intended to be a placeholder before we switch to the Deadlock API.
    /// </summary>
    public class TrackLockFetcher
    {
        public static async Task FetchAndParseDataAsync(string url)
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
    }
}
