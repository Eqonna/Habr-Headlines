using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Habr_headlines
{
    class Program
    {
        static string GetHTMLPage(string link)
        {
            string HtmlText;
            HttpWebRequest habrHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(link);
            HttpWebResponse habrHttpWebResponse = (HttpWebResponse)habrHttpWebRequest.GetResponse();
            StreamReader strm = new StreamReader(habrHttpWebResponse.GetResponseStream());
            HtmlText = strm.ReadToEnd();
            return HtmlText;
        }
        static void Pars(string HtmlText)
        {
            string patternHeadline = @"(<h2 class=""post__title\"">)(.*)\n(.*)";
            MatchCollection matchesHeadlines = Regex.Matches(HtmlText, patternHeadline, RegexOptions.IgnoreCase);
            for(int i = 0; i < 20; i++)
            {
                string a = matchesHeadlines[i].ToString();
                a = a.Remove(a.Length - 4, 4);
                int ind = a.IndexOf('>');
                ind++;
                ind = a.IndexOf('>', ind);
                ind++;
                a = a.Remove(0, ind);
                Console.WriteLine(a);
            }
        }

        static void Main(string[] args)
        {
            string link = "https://habr.com/ru/all/page";
            string page = Console.ReadLine();
            link = link + page;

            Pars(GetHTMLPage(link));
        }
    }
}
