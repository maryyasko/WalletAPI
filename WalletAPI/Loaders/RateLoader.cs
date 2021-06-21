using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WalletAPI.Models;

namespace WalletAPI.Loaders
{
    public class RateLoader : IRateLoader
    {
        /// <summary>
        /// Публичный API для получения текущего курса валют.
        /// </summary>
        private readonly string URL = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        private string GetData()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            using var reader = new StreamReader(dataStream);
                return reader.ReadToEnd();
        }

        public IEnumerable<CurrencyInformation> GetRateInformation()
        {
            var ratesInformation = new List<CurrencyInformation>();

            var data = GetData();

            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            Envelope rateSerializer;
            using (var sr = new StringReader(data))
                rateSerializer = (Envelope)serializer.Deserialize(sr);

            foreach (var rate in rateSerializer.Cube.Cube1.Cube)
            {
                var rateInformation = new CurrencyInformation
                {
                    Currency = rate.currency,
                    Rate = rate.rate,
                };

                ratesInformation.Add(rateInformation);
            }

            return ratesInformation;
        }
    }
}
