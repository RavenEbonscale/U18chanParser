using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace U18chanParser
{
    
    public struct U18Info
    {
        public string comment { get;set; }
        public string url {get;set; }
    }
    
    
    
    public class U18Parser : IU18Parser
    {
        const string pattern = "https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*).jpg|.png|.gif$";
        


        private HtmlWeb web;


        public U18Parser()
        {
            this.web = new HtmlWeb();
        }

        public List<U18Info> U18Parse(List<string> urls)
        {
            
            List<U18Info> u18Infos = new List<U18Info>();
            foreach (string url in urls)
            {
                var doc = web.Load(url);


                //Get the main area of the Html webpage
                List<HtmlNode> htmlNodes = doc.DocumentNode.Descendants("form").Where(n => n.GetAttributeValue("id", " ").Equals("DeleteForm")).ToList();


                //Breaks it down into sub posts
                var U18Posts = htmlNodes[0].Descendants("table").Where(x => x.GetAttributeValue("class", " ").Contains("ReplyBoxTable")).ToList();


                //parse indivduial post for comment and image :3
                foreach (var U18Post in U18Posts)
                {
                    U18Info temp = new U18Info();


                    //comments :3
                    var text = U18Post.Descendants("span").Where(n => n.GetAttributeValue("name", " ").Contains("post_")).ToList();

                    if (text.Count > 0)
                    {
                        temp.comment = text[0].InnerText;
                    }

                    var image = U18Post.Descendants("a").Where(x => x.GetAttributeValue("href", "").Contains("https:")).ToList();
                    {
                        if (image.Count > 0)
                        {
                            temp.url = image[0].GetAttributeValue("href", "");
                        }

                    }

                    if (temp.comment != null || temp.url != null)
                        u18Infos.Add(temp);



                }
            } 

            return u18Infos;
        
        
        
        }

        public List<U18Info> U18SimpleParse(List<string> urls)
        {
            List<U18Info> result = new List<U18Info>();
            foreach (string url in urls)
            {
                
                HtmlDocument doc = web.Load(url);



                var nodes = doc.DocumentNode.Descendants(0).Where(n => n.HasClass("FileDetails"));
                foreach (var node in nodes)
                {
                    U18Info temp = new();
                    Match m = Regex.Match(node.InnerHtml, pattern);


                    //I'm to lazy to figure out a better method to get rid of random extensions
                    if (m.Success & m.Value.Length > 4)
                    {
                        temp.url = m.Value;
                    }
                    result.Add(temp);

                }
            }
            return result;
        }
    }
}
