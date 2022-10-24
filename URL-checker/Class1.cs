using System.Collections.Generic;
using System.Text.RegularExpressions;
using URL_checker.LinkParser;

namespace URL_checker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mk = @"https://www.travelline.ru/";//@"http://links.qatl.ru/about.html" https://koptelnya.ru/
            LinkParser.LinkParser link = new LinkParser.LinkParser();//https://www.travelline.ru/
            var listOfRefs = link.getDomenLinks(mk);//link.getDomenLinksFromHtlm(mk) link.getDomenLinks(mk)
            HashSet<string> listOfUncheckedLinks = new HashSet<string> { mk };
            HashSet<string> listOfCheckedLinks = new HashSet<string>();
            while (true)
            {
                if (listOfUncheckedLinks.Count == 0) break;
                LinkParser.LinkParser.func(ref listOfUncheckedLinks, ref listOfCheckedLinks, ref link);
            }
            link.ValidateLinks();
            link.LinksToFiles(
                @"D:\cpluplus\repos\URL-checker\URL-checker\T.txt",
                @"D:\cpluplus\repos\URL-checker\URL-checker\Te.txt");

        }
    }
}