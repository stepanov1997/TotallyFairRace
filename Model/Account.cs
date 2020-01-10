using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace TotallyFairRace.Model
{
    [JsonObject]
    public class Account
    {
        // static members
        public static RankCollection RankCollection { get; set; }
        public static Account CurrentAccount { get; set; }

        // non-static members
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }
        [JsonProperty(PropertyName = "money")]
        public double Money { get; set; }
        [JsonProperty(PropertyName = "numberOfGames")]
        public int NumberOfGames { get; set; }
        [JsonProperty(PropertyName = "lastMatch")]
        public DateTime LastMatch { get; set; }
        [JsonProperty(PropertyName = "points")]
        public int Points { get; set; }

        // constructor
        static Account()
        {
            RankCollection = new RankCollection();
            CurrentAccount = null;
        }

        public Account() { }
        private Account(string nickname)
        {
            Nickname = nickname;
            Money = 50;
            NumberOfGames = 0;
            LastMatch = DateTime.Today;
            Points = 0;
        }

        public static async Task<Account> CreateAccount(string nickname)
        {
            CurrentAccount = RankCollection
                                 .Where(e => string.Equals(e.Nickname, nickname,
                                     StringComparison.CurrentCultureIgnoreCase))
                                 .ToList()
                                 .FirstOrDefault()
                             ?? new Account(nickname);
            RankCollection.Add(CurrentAccount);
            RankCollection.Sort(true);
            await SerializeNow();
            return CurrentAccount;
        }

        public static async Task SerializeNow()
        {
            string output = JsonConvert.SerializeObject(RankCollection.ToArray(), Formatting.Indented);
            await Task.Run(async () =>
            {
                StorageFolder storageFolder =
                    ApplicationData.Current.LocalFolder;
                StorageFile storageFile =
                    await storageFolder.CreateFileAsync("accounts.json",
                        CreationCollisionOption.ReplaceExisting);

                //Write data to the file
                await FileIO.WriteTextAsync(storageFile, output);
            });
        }

        public static async Task<bool> DeSerializeNow()
        {
            StorageFolder storageFolder =
                ApplicationData.Current.LocalFolder;
            StorageFile storageFile =
                await storageFolder.GetFileAsync("accounts.json");

            if (storageFile == null) return false;
            string rankListJson = await FileIO.ReadTextAsync(storageFile);
            if (rankListJson == null) return false;
            var accounts = JsonConvert.DeserializeObject<Account[]>(rankListJson);
            RankCollection = new RankCollection(accounts.ToList());
            return RankCollection != null;
        }
    }


}
