using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using match_data_scraper;
using artpass.Models;

var m = await TrackLockFetcher.FetchAndParseDataAsync("https://tracklock.gg/players/94516027"); 
foreach (DeadlockMatch ma in m)
{
    Console.WriteLine(ma);
}

