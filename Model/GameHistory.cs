using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Newtonsoft.Json;

namespace TotallyFairRace.Model
{
    public static class GameHistory
    {
        public static List<Game> Games { get; set; }

        //static GameHistory() => Games = new List<Game>();
        static GameHistory()
        {
            Games = new List<Game>();
            Games.Add(new Game(30, new[]
            {
                new IndividualStat(RandomGen2.Next()%5, Color.FromArgb(255,(byte)(RandomGen2.Next()%255), (byte)(byte)(RandomGen2.Next()%255),(byte)(RandomGen2.Next()%255)), "Kiki",40, 40 ),
                new IndividualStat(RandomGen2.Next() % 5, Color.FromArgb(255,(byte)(RandomGen2.Next()%255), (byte)(RandomGen2.Next()%255),(byte)(RandomGen2.Next()%255)), "Kiki",33, 54 )
            }.ToList()));
        }

        public static async Task SerializeNow()
        {
            string output = JsonConvert.SerializeObject(Games.ToArray(), Formatting.Indented);
            StorageFolder storageFolder =
                ApplicationData.Current.LocalFolder;
            StorageFile storageFile =
                await storageFolder.CreateFileAsync("game_history.json",
                    CreationCollisionOption.ReplaceExisting);

            //Write data to the file
            await FileIO.WriteTextAsync(storageFile, output);
        }

        public static async Task<bool> DeSerializeNow()
        {
            StorageFolder storageFolder =
                ApplicationData.Current.LocalFolder;
            StorageFile storageFile = null;
            try
            {
                storageFile = await storageFolder.GetFileAsync("game_history.json");
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException)
            {
                return false;
            }
            string gamesHistoryJson = await FileIO.ReadTextAsync(storageFile);
            if (gamesHistoryJson == null) return false;
            var games = JsonConvert.DeserializeObject<Game[]>(gamesHistoryJson);
            Games = new List<Game>(games.ToList());
            return true;
        }
    }

    [JsonObject]
    public class Game
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; }
        [JsonProperty(PropertyName = "distance")]
        public decimal Distance { get; }
        [JsonProperty(PropertyName = "durance")]
        public double Durance { get; }
        public List<IndividualStat> Stats { get; set; }

        public Game(decimal distance, List<IndividualStat> stats)
        {
            Distance = distance;
            Stats = stats;
            Date = DateTime.UtcNow;
            Durance = stats.Max(e => e.Durance);
        }
    }

    [JsonObject]
    public class IndividualStat
    {
        [JsonProperty(PropertyName = "place")]
        public int Place { get; }
        [JsonProperty(PropertyName = "color")]
        public Color CarColor { get; }
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; }
        [JsonProperty(PropertyName = "points")]
        public int Points { get; }
        [JsonProperty(PropertyName = "durance")]
        public double Durance { get; }

        public IndividualStat(int place, Color carColor, string nickname, int points, double durance)
        {
            Place = place;
            CarColor = carColor;
            Nickname = nickname;
            Points = points;
            Durance = durance;
        }
    }
}
