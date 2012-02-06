using System;
using System.Collections.Generic;
using System.Text;
using Lymbix.ToneAPI;
using Lymbix.ToneAPI.Models;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ToneAPI toneapi = new ToneAPI("YOUR API KEY");
            ArticleInfo response = toneapi.TonalizeDetailed("This is a beautiful world, don't you think?");
            Console.WriteLine(String.Format("Contentment: {0}", response.ContentmentGratitude));
        }
    }
}
