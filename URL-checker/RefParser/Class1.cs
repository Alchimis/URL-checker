using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;
using System.Net;
using System.Threading.Tasks;

namespace URL_checker.LinkParser
{
    internal class LinkParser
    {
        public static bool isAbsoluteLink(string link, string host) 
        {
            Regex reg=new Regex($"^[Hh][Tt][Tt][Pp][Ss]?://{host}");
            return reg.IsMatch(link);
        }
        Regex regRelative { set; get; } = new Regex(@"^(\.\.|\/)");//(\.\.)|(/)");
        public bool isNLink(string link)
        {
            return regRelative.IsMatch(link);
        }
        public bool isTellephone(string link) 
        {
            return telReg.IsMatch(link);
        }
        Regex telReg { set; get; } = new Regex(@"[Tt][Ee][Ll]://");
        Regex someREg { set; get; } = new Regex(@"(.*)(/[^/]*)$");
        public bool isMail(string link) 
        {
            return regMail.IsMatch(link) ;
        }
        Regex regMail { set; get; } = new Regex(@"[Mm][Aa][Ii][Ll][Tt][Oo]:");
        public HashSet<string> listOfAllRefs { set; get; } = new HashSet<string>();
        public HashSet<string> listOfUnverifiedLinks { set; get; } = new HashSet<string>();
        public HashSet<string> listOfUncheckedLinks { set; get; } = new HashSet<string>();
        public HashSet<string> listOfCheckedLinks { set; get; } = new HashSet<string>();
        public HashSet<string> listOfawLiks { set; get; } = new HashSet<string>();

        public HashSet<(string, HttpStatusCode?)> listOfBrokenLinks { set; get; } = new HashSet<(string, HttpStatusCode?)>();
        public HashSet<(string, HttpStatusCode?)> listOfVerifiedLinks { set; get; } = new HashSet<(string, HttpStatusCode?)>();
        public string Path { set; get; }
        public LinkParser() { }
        public string Domain { get; set; } = "";
        /*public HashSet<string> getDomenLinksFromHtlm(string htmlPath)
        {
            Uri uri;
            try
            {
                uri = new Uri(htmlPath);
            }
            catch { return new HashSet<string>(); }
            WebClient webClient = new WebClient();
            Domain = "http://" + uri.Host;
            var webHtml = new HtmlWeb();

            HtmlDocument htmlDocument;
            try { htmlDocument = webHtml.Load(uri); } catch { return new HashSet<string>(); }
            var nodesWithHref = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            Regex regex = new Regex($"^[Hh][Tt][Tt][Pp][Ss]?://{uri.Host}");
            Regex reg = new Regex($"^[Hh][Tt][Tt][Pp][Ss]?://");
            


            Regex rg = new Regex($"^../");
            if (nodesWithHref != null)
                foreach (var node in nodesWithHref)
                {
                    var href = node.Attributes["href"].Value;
                    if (!listOfawLiks.Contains(href))
                    {
                        listOfawLiks.Add(href);
                        bool k = true;

                        if (rg.IsMatch(href))
                        {
                            if (Domain[Domain.Length - 1] != '/' && href[0] != '/') { href = Domain + '/' + href.Substring(3); }
                            else
                            {
                                href = Domain + href.Substring(3);
                            }
                            k = false;
                 */
        /*           if (htmlPath[htmlPath.Length - 1] != '/' && href[0] != '/') { href = htmlPath + '/' + href.Substring(3); }
                            else
                            {
                                href = htmlPath + href.Substring(3);
                            }
                            k = false;*/
        /*
                        }
                        if (!reg.IsMatch(href))
                        {
                            if (Domain[Domain.Length - 1] != '/' && href[0] != '/')
                            {
                                href = Domain + '/' + href;
                            }
                            else
                            {
                                href = Domain + href;
                            }
                            */
        /*if (htmlPath[htmlPath.Length - 1] != '/' && href[0] != '/')
                            {
                                href = htmlPath + '/' + href;
                            }
                            else
                            {
                                href = htmlPath + href;
                            }*/
        /*
                        }

                        if ((href[0] == '/') || regex.IsMatch(href))
                        {
                            listOfUnverifiedLinks.Add(href);
                        }
                        listOfAllRefs.Add(href);
                    }
                }
            Path = htmlPath;
            return listOfUnverifiedLinks;
        }
*/
        Regex yaNe = new Regex(@"^/");
        Regex soneNewReg = new Regex(@"/$");
        public bool otherCheck(string href, string domain) 
        {
            return yaNe.IsMatch(href) || soneNewReg.IsMatch(domain);
        }
        public string otherFunc(string href, string domain) 
        {
            if (otherCheck(href, domain)) { return domain + href; }
            return href + "/" + domain;
        } 
        public HashSet<string> getDomenLinks(string htmlPath)
        {
            Uri uri;
            try
            {
                uri = new Uri(htmlPath);
            }
            catch { return new HashSet<string>(); }
            WebClient webClient = new WebClient();
            Regex reg = new Regex($"^[Hh][Tt][Tt][Pp][Ss]?://");
            Regex httpsreg = new Regex($"^[Hh][Tt][Tt][Pp][Ss]://");
            Regex httpreg = new Regex($"^[Hh][Tt][Tt][Pp]://");
            var t = uri.Host;
            if (httpreg.IsMatch(htmlPath)) {
                Domain = "http://" + t;
            }
            if (httpsreg.IsMatch(htmlPath)) 
            {
                Domain = "https://" + uri.Host;
            }
            
            var webHtml = new HtmlWeb();

            HtmlDocument htmlDocument;
            try { htmlDocument = webHtml.Load(uri); } catch { return new HashSet<string>(); }
            var nodesWithHref = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            Regex regex = new Regex($"^[Hh][Tt][Tt][Pp][Ss]?://{uri.Host}");
            
            Regex rg = new Regex($@"^\.\.");
            if (nodesWithHref != null)
                foreach (var node in nodesWithHref)
                {
                    var href = node.Attributes["href"].Value;
                    if (!listOfawLiks.Contains(href))
                    {
                        listOfawLiks.Add(href);
                        bool k = true;
                        int a = 0;
                        if (telReg.IsMatch(href) || regMail.IsMatch(href))//
                        {
                           continue;
                        }
                        else
                        {
                            if (someREg.IsMatch(href) && !reg.IsMatch(href) && !rg.IsMatch(href))
                            {//&& !rg.IsMatch(href)
                                href = otherFunc(href,Domain);
                            }
                            if (regRelative.IsMatch(href))
                            {
                                Regex jjk = new Regex(@"(/[^/]*)$");
                                Match b = jjk.Match(htmlPath);
                                string g = htmlPath.Substring(0, htmlPath.Length - b.Groups[1].Value.Length);
                                if (htmlPath == Domain)
                                {
                                    href = otherFunc(href,Domain);
                                    
                                }
                                else {
                                    href = otherFunc(href,g);
                                }

                            }
                            if (!reg.IsMatch(href))
                            {
                                Regex jjk = new Regex(@"(/[^/]*)$");
                                string g = "";
                                Match b;
                                if (htmlPath != Domain)
                                {
                                    b = jjk.Match(htmlPath);
                                    g = htmlPath.Substring(0, htmlPath.Length - b.Groups[1].Value.Length);
                                }
                                else {
                                    g = htmlPath;
                                }
                                    href = otherFunc(href, g);
                            }
                            if (regex.IsMatch(href))
                            {
                                listOfUnverifiedLinks.Add(href);
                            }
                            listOfAllRefs.Add(href);
                        }
                    }
                }
            Path = htmlPath;
            return listOfUnverifiedLinks;
        }


        public void ValidateLinks()
        {
            foreach (string node in listOfUnverifiedLinks)
            {
                var href = node;
                HttpStatusCode? statusCode = getLinkStatusCode(href);
                if (statusCode != null && (int)statusCode >= 400 && (int)statusCode <= 599)//httpStatusCodes.Contains(statusCode)  statusCode != HttpStatusCode.OK
                {
                    listOfBrokenLinks.Add(((string)href, statusCode));
                }
                else
                {
                    listOfVerifiedLinks.Add(((string)href, statusCode));
                }

            }
        }
        public void LinksToFiles(string validLinksFile, string invalidLinksFile)
        {

            var corectLinksFile = new StreamWriter(validLinksFile);
            var errorLinksFile = new StreamWriter(invalidLinksFile);
            foreach ((string, HttpStatusCode?) link in listOfVerifiedLinks)
            {
                corectLinksFile.WriteLine(link.Item1 + " " + link.Item2.ToString());
            }
            corectLinksFile.WriteLine($"count {listOfVerifiedLinks.Count}; {DateTime.Now}");
            corectLinksFile.Close();
            foreach ((string, HttpStatusCode?) link in listOfBrokenLinks)
            {
                errorLinksFile.WriteLine(link.Item1 + " " + link.Item2.ToString());
            }
            errorLinksFile.WriteLine($"count {listOfBrokenLinks.Count}; {DateTime.Now}");
            errorLinksFile.Close();

        }
        public static HttpStatusCode? getLinkStatusCode(string url)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            try
            {
                WebRequest request = WebRequest.Create(url);

                HttpWebResponse response
                    =
                    (HttpWebResponse)request.GetResponse();
                var statusCode = response.StatusCode;
                response.Close();
                return statusCode;
            }
            catch (WebException e)
            {
                if (e.Response == null)
                    return null;
                var errorResponse = e.Response as HttpWebResponse;
                return errorResponse.StatusCode;
            }
            catch
            {
                return HttpStatusCode.NotFound;
            }

        }
        public static void func(ref HashSet<string> chekedLink, ref HashSet<string> unchekedLinks, ref LinkParser linkParser)
        {
            if (chekedLink == null) { return; }
            if (chekedLink.Count == 0) { return; }
            string link = "";
            foreach (string k in chekedLink)
            {
                link = k;
                break;
            }
            if (link == null) { return; }
            linkParser.getDomenLinks(link);//linkParser.getDomenLinksFromHtlm(link)  linkParser.getDomenLinks(link)
            foreach (string l in linkParser.listOfUnverifiedLinks)
            {
                if (!unchekedLinks.Contains(l))
                {
                    chekedLink.Add(l);
                }
            };
            unchekedLinks.Add(link);
            chekedLink.Remove(link);
        }
    }

}
