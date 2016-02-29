using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.Net.NetworkInformation;

namespace ConsoleApplication1
{
    class Program
    {

        //Karthi Here
        static void Main(string[] args)
        {
            LocalPing();

            ReadValueFromXmldocument();
            CreateXmlDocument();

            String ipstr = "#1111 👎 10%";
            String uniqueid = String.Empty;

            String[] words = ipstr.Split(' ');
            String discountPercent = String.Empty; 
            int discountType = 0;
            String thumbsymbolhexavalue = String.Empty;

            foreach (string word in words)
            {
                String afterSpace = String.Empty;

                if (word.Contains('#'))
                {
                    if (word.Contains("👍") && word.Trim().StartsWith("👍"))
                    {
                        thumbsymbolhexavalue = Regex.Match(word, @"\s(.*)").Groups[0].Value;

                        //Get Unique id by avoiding '#'.
                        uniqueid = word.Replace("👍", "").Trim().Remove(0, 1); //Remove '#' symbol from here.
                    }
                    else if (word.Contains("👍") && word.Trim().EndsWith("👍"))
                    {
                        thumbsymbolhexavalue = Regex.Match(word, @"\s(.*)").Groups[1].Value;
                        uniqueid = word.Replace("👍", "").Trim().Remove(0, 1);
                    }
                    else
                    {
                        uniqueid = word.Replace("👍", "").Trim().Remove(0, 1); 
                    }
                }
                else if (word.Contains('%'))
                {
                    if (word.Contains("👍") && word.Trim().StartsWith("👍"))
                    {
                        thumbsymbolhexavalue = Regex.Match(word, @"\s(.*)").Groups[0].Value;
                    }
                    else if (word.Contains("👍") && word.Trim().EndsWith("👍"))
                    {
                        thumbsymbolhexavalue = Regex.Match(word, @"\s(.*)").Groups[1].Value;
                    }

                    discountPercent = word.Contains("👍") ? word.Replace("👍", "") : word;
                    discountType = 10;
                }
                else
                {
                    thumbsymbolhexavalue = word.Trim();
                }
            }

            discountPercent = discountPercent.Trim().Replace("%", "").Trim();
            thumbsymbolhexavalue = ConvertStringToHex(thumbsymbolhexavalue);
          

            //String uniqueid = String.Empty;
            //String thumbsymbolhexavalue = String.Empty;
            //String discountPercent = String.Empty;

            //string[] words = ipstr.Split(' ');
            //foreach (string word in words)
            //{
            //    //30% 👍
            //    if (word.Contains("👍"))
            //    {
            //        var afterSpace = Regex.Match(word, @"\s(.*)").Groups[1].Value;

            //        thumbsymbolhexavalue = ConvertStringToHex(afterSpace);
            //    }

            //    if (word.Contains('#'))
            //    {
            //        //Get Unique id by avoiding '#'.
            //        uniqueid = word.Trim().Remove(0, 1); //Remove '#' symbol from here.
            //    }
            //    else if (word.Contains('%'))
            //    {
            //        //Get discount Percentage.
            //        discountPercent = word.Trim();
            //    }
            //    else
            //    {
            //        //Get status(Approved/Rejected).
            //        thumbsymbolhexavalue = word.Trim();
            //    }
            //}



            String splcharstr = "DR.SANKARANARAYANAN .M MBBS .DPM . M.D";
            splcharstr = Regex.Replace(splcharstr, @"[^0-9a-zA-Z]+", " ");


            WebClient _webclient = new WebClient();
            XmlDocument xmlDocument = new XmlDocument();


            ////http://192.168.1.10:8080/Api/AndroidDevice?data=true&deviceid=10
            //String webApiEndPointAddress = "http://192.168.1.10:8080/Api/AndroidDevice?data=true&deviceid=1";
            //String response = _webclient.DownloadString(webApiEndPointAddress);


            //if (!String.IsNullOrEmpty(response))
            //{
            //    xmlDocument = new XmlDocument();
            //    xmlDocument.LoadXml(response.ToString());
            //}


            xmlDocument.Load(@"C:\Users\Innowave\Desktop\Testtt.xml");

            if (xmlDocument != null && xmlDocument.SelectNodes("PatientInfo/*").Count > 0)
            {
                //if(!String.IsNullOrEmpty(response) || xmlDocument.SelectNodes("PatientInfo/*").Count > 0) { 

                XmlNodeList xmlNodeList = xmlDocument.SelectNodes("*");

                String patname = xmlNodeList[0]["PatientName"].InnerText;

                foreach (XmlNode node in xmlNodeList)
                {
                    string id = node.InnerText; 
                }
            }




            //String input = "#1111Y";
            //var afterSpace = Regex.Match(input, @"\s(.*)").Groups[1].Value;




            //DateTime dob = DateTime.MinValue;
            //String age = GetAgeFromDateofBirth("1958-10-25 00:00:00", ref dob); //yyyy-mm-dd HH:mm:ss

            //int maxval = Int32.MaxValue;
            //int minval = Int32.MinValue;

            //int i = Convert.ToInt32(DateTime.Today.ToString("yymmHHMMss"));

            //String _sourcedirpath = @"E:\DataStore\StudyFolder";
            //String _destdirpath = @"E:\DataStore\FileStore";

            //String filename = @"E:\DataStore\StudyFolder\2015\1\1\New folder\1.3.6.1.4.1.25403.260208109885019.3136.20150925105451.72.dcm";

            //String getfilepath = filename.Substring(_sourcedirpath.Length + 1);

            //String rootpath = Path.GetDirectoryName(filename);
        }

        public static void LocalPing()
        {
            String ipAddress = "192.168.1.10";
            ipAddress = ipAddress.Split(':')[0];

           Ping ping = new Ping();
           PingReply pingresult = ping.Send(ipAddress);
            if (pingresult.Status.ToString() == "Success")
            {

            }
        }


        private static String GetAgeFromDateofBirth(String dobstr, ref DateTime birthdate)
        {
            if (String.IsNullOrEmpty(dobstr)) { return String.Empty; }
            int age = 0;

            try
            {
                String datetimeformatlist = "dd/MM/yyyy HH:mm:ss;dd/MM/yyyy HH:mm:ss tt;M/d/yyyy HH:mm:ss tt;M/dd/yyyy HH:mm:ss tt;MM/d/yyyy HH:mm:ss tt;MM/dd/yyyy HH:mm:ss tt;dd-MM-yyyy;dd-MM-YYYY;yyyyMMddHHmmss;yyyyMMdd;HH:mm:ss;yyyy-MM-dd HH:mm:ss";
                //String datetimeformatlist = "yyyy-MM-dd HH:mm:ss";

                String[] formats = datetimeformatlist.Split(';');
                //DateTime birthdate = DateTime.ParseExact(dobstr, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime.TryParseExact(dobstr, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthdate);

                if (birthdate == DateTime.MinValue)
                {
                    //Platform.Log(LogLevel.Warn, "Date of birth get the minimum value, so we can't get the age from DOB");
                    return String.Empty;
                }
                age = DateTime.Now.Year - birthdate.Year;

                if (age == 0)
                {
                    int months = 0;
                    months = DateTime.Now.Month - birthdate.Month;
                    if (months == 0)
                    {
                        int days = DateTime.Now.Day - birthdate.Day;
                        return String.Format("{0:000}{1}", days, "D");
                    }
                    return String.Format("{0:000}{1}", months, "M");
                }
            }
            catch (Exception ex)
            {
                //Platform.Log(LogLevel.Error, ex, String.Format("{0} , {1} : {2}./n DOB parameter will be {3}", "EasySoftMwlMppsBackend", "GetAgeFromDateofBirth", ex, dobstr));
            }

            return String.Format("{0:000}{1}", age, "Y");
        }

        public static string ConvertStringToHex(String input)
        {
            Byte[] stringBytes = System.Text.Encoding.Unicode.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        private static void CreateXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("AndriodAppSettings");
            xmlDoc.AppendChild(rootNode);

            XmlNode userNode = xmlDoc.CreateElement("IpAddress");
            userNode.InnerText = "127.0.0.1";
            rootNode.AppendChild(userNode);

            xmlDoc.Save("test-doc.xml");
        }

        private static void ReadValueFromXmldocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("test-doc.xml");

            XmlNodeList xnList = xmlDoc.SelectNodes("/AndriodAppSettings");

            String ipAddress = xnList[0]["IpAddress"].InnerText;
        }
    }
}





