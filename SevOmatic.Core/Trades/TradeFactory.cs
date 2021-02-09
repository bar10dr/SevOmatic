using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SevOmatic.Core.Trades
{
    public static class TradeFactory
    {
        static string FileHash = string.Empty;

        //Reads and returns the json trade data
        public static (List<IList<object>> Data, bool HasRefreshed) ReadData()
        {
            var json = File.ReadAllText("testdata.json");
            var hashedJson = ComputeSha256Hash(json);
            var data = JsonConvert.DeserializeObject<TradeData>(json);

            if (FileHash != hashedJson)
            {
                FileHash = hashedJson;

                return (PackageData(data), true);
            }

            return (PackageData(data), false);
        }

        //Packages the data in a format that the Google API will understand
        static List<IList<object>> PackageData(TradeData Datas)
        {
            var packagedData = new List<IList<object>>();

            foreach (var data in Datas.Trades)
            {
                var dataList = new List<object>();

                dataList.Add(data.Symbol);
                dataList.Add(data.BuyPrice);

                packagedData.Add(dataList);
            }

            return packagedData;
        }

        //The raw json data is hashed in order to see if the contents has changed since last update
        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
