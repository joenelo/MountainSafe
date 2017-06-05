using System;
using System.Collections.Generic;
using System.Text;

namespace MountainSafe
{
    public class Feed
    {
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public List<object> enclosure { get; set; }
        public List<string> categories { get; set; }
    }

    public class RssFeed
    {
        public string status { get; set; }
        public Feed feed { get; set; }
        public List<Item> items { get; set; }
    }


   // Item avalancheData = new Item (t)
                                            

    
}
