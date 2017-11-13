using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace dCareProject.Models {
    public class jsonGather {
        string ipaddr = @"http://192.168.1.113:2003/";
        public string getCardID {
            get {
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Stream mystream = objWebClient.OpenRead( ipaddr + "get_cid"); //cid = CardID

                //JSON
                StreamReader sr = new StreamReader(mystream);
                string sJson = sr.ReadToEnd();
                sr.Close();

                JObject objJson = JObject.Parse(sJson);

                //getWeight
                var objToken = objJson.SelectToken("cardid");

                string cardid = objToken.ToString();
                cardid = cardid.Replace("[\r\n  \"", "").Replace("\"\r\n]", "");


                return cardid;
            }
        }


        public string getWeight {
            get {
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Stream mystream = objWebClient.OpenRead(ipaddr + "get_weight");

                //JSON
                StreamReader sr = new StreamReader(mystream);
                string sJson = sr.ReadToEnd();
                sr.Close();

                JObject objJson = JObject.Parse(sJson);

                //getWeight
                var objToken = objJson.SelectToken("weight");
                string weight = objToken.ToString();
                weight = weight.Replace("[\r\n  ", "").Replace("\r\n]", "");

                return weight;
            }
        }

        public string getTemp {
            get {
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Stream mystream = objWebClient.OpenRead(ipaddr + "get_temp");

                //JSON
                StreamReader sr = new StreamReader(mystream);
                string sJson = sr.ReadToEnd();
                sr.Close();

                JObject objJson = JObject.Parse(sJson);

                //getWeight
                var objToken = objJson.SelectToken("temp");
                string weight = objToken.ToString();
                weight = weight.Replace("[\r\n  ", "").Replace("\r\n]", "");

                return weight;
            }
        }

        public string getS2PO {
            get {
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Stream mystream = objWebClient.OpenRead(ipaddr + "get_s2po");

                //JSON
                StreamReader sr = new StreamReader(mystream);
                string sJson = sr.ReadToEnd();
                sr.Close();

                JObject objJson = JObject.Parse(sJson);

                //getWeight
                var objToken = objJson.SelectToken("s2po");
                string weight = objToken.ToString();
                weight = weight.Replace("[\r\n  ", "").Replace("\r\n]", "");

                return weight;
            }
        }

        public string getHeartRate {
            get {
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Stream mystream = objWebClient.OpenRead(ipaddr + "get_heartrate");

                //JSON
                StreamReader sr = new StreamReader(mystream);
                string sJson = sr.ReadToEnd();
                sr.Close();

                JObject objJson = JObject.Parse(sJson);

                //getWeight
                var objToken = objJson.SelectToken("heartrate");
                string weight = objToken.ToString();
                weight = weight.Replace("[\r\n  ", "").Replace("\r\n]", "");

                return weight;
            }
        }

    }
}