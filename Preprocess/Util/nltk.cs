using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NltkNet;

namespace Preprocess.Util
{
    class nltk
    {
        public static void Init()
        {
            /*Nltk.Init(new List<string>
            {
                @"C:\Program Files\IronPython 2.7\Lib",                 // Path to IronPython standard libraries
                @"C:\ProgramData\Anaconda3\Lib\site-packages",   // Path to IronPython third-party libraries
            });*/
            Nltk.Init(new List<string>
            {
                HttpContext.Current.Server.MapPath("~/packages/IronPython 2.7/Lib"),  
                HttpContext.Current.Server.MapPath("~/packages/site-packages"),
            });
        }

        public static List<string> WordSeg(String text)
        {

            List<string> result = new List<string>();

            var list = Nltk.Tokenize.WordTokenize(text).AsNet;

            foreach (var item in list)
                result.Add(item);
            return result;
        }

        public static List<string> SentenceSeg(String text)
        {

            List<string> result = new List<string>();

            var list = Nltk.Tokenize.SentTokenize(text).AsNet;

            foreach (var item in list)
                result.Add(item);
            return result;
        }

    }
}