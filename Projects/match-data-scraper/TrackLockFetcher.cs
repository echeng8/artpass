
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using artpass.Models;

namespace match_data_scraper
{
    /// <summary>
    /// Fetches match data from TrackLock.gg profile url
    /// This is intended to be a placeholder before we switch to the Deadlock API.
    /// @TODO expand this beyond the last 30 matches?? 
    /// @TODO time of day is not included in this data.
    /// </summary>
    public class TrackLockFetcher
    {

        /// <summary>
        /// Intermediary deadlock match class before converting to DeadlockMatch
        /// </summary>
        public class DeadlockMatchJSON
        {
            [JsonProperty("elo_after")]
            public int EloAfter { get; set; }

            [JsonProperty("is_win")]
            public bool IsWin { get; set; }

            [JsonProperty("match_id")]
            public string MatchId { get; set; } // Changed to string as per JSON

            [JsonProperty("start_time_day")]
            public string StartTimeDay { get; set; }

            // Override ToString method
            public override string ToString()
            {
                return $"EloAfter: {EloAfter}, IsWin: {IsWin}, MatchId: {MatchId}, StartTimeDay: {StartTimeDay}";
            }
        }

        public static async Task<List<DeadlockMatch>> FetchAndParseDataAsync(string url)
        {
            // todo;

            var deadlockMatches = new List<DeadlockMatch>();
            using HttpClient client = new(); 
            try
            {
                var response = await client.GetStringAsync(url);
                string pattern =   @"\{[^{}]*\""is_win\\\""[^{}]*\}";
                Regex regex = new Regex(pattern, RegexOptions.Multiline);

                MatchCollection matches = regex.Matches(response);
                foreach(Match match in matches)
                {
                    deadlockMatches.Add(JsonToDeadlockMatch(match.Value));
                }

                return deadlockMatches;
            } catch (Exception e)
            {
                return deadlockMatches;
            }
        }

        private static DeadlockMatch JsonToDeadlockMatch(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new Exception("Empty JSON string");
            }

            // remove slashes 
            jsonString = jsonString.Replace("\\", "");

            DeadlockMatchJSON match = JsonConvert.DeserializeObject<DeadlockMatchJSON>(jsonString);
            if (match == null)
            {
                throw new Exception("Failed to parse JSON to DeadlockMatch");
            }

            // convert json match to DeadlockMatch

            //  clean timestamp
            var timestampString = match.StartTimeDay.ToString().Replace("$D","");
            DateTime startTimeDay = DateTime.Parse(timestampString);

            return new DeadlockMatch(
                int.Parse(match.MatchId),
                match.IsWin,
                startTimeDay
                );
        }
    }
}
