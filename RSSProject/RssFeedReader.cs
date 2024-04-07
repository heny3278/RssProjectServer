using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RSSProject
{
    public class RssFeedReader
    {
        public  static List<NewsTopic> GetNewsTopics()
        {
            // הגדרת כתובת URL של RSS Feed
            string url = "https://www.recruiter.com/feed/career.xml";

            // אחזור נתוני RSS Feed
            WebClient client = new WebClient();
            string xmlData = client.DownloadString(url);

            // ניתוח נתוני XML
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);

            // חילוץ כותרות וכתובות URL של נושאים
            List<NewsTopic> topics = new List<NewsTopic>();
            XmlNodeList nodes = doc.SelectNodes("//item");
            foreach (XmlNode node in nodes)
            {
                NewsTopic topic = new NewsTopic();
                topic.Title = node.SelectSingleNode("title").InnerText;
                topic.Url = node.SelectSingleNode("link").InnerText;
              //  topic.Img = node.SelectSingleNode("img").InnerText;
                topic.Body = node.SelectSingleNode("description").InnerText;

                var regex = new Regex("<img\\s+(?:[^>]*?\\s+)?src\\s*=\\s*(?:'([^']+)'|\\\"([^\\\"]+)\\\")(?:[^>]*?)>", RegexOptions.IgnoreCase);
                

                // Match the IMG tag in the body
                var match = regex.Match(topic.Body);

                // Get the image URL and alt text
                topic.Img= match.Groups[2].Value;





                topics.Add(topic);
            }

            return topics;
        }
    }
}
