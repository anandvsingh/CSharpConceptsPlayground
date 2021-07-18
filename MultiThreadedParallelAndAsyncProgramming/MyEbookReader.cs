using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net;

namespace MultiThreadedParallelAndAsyncProgramming
{
    public class MyEbookReader
    {
        private static string theEbook = "";
        public static void GetBook()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) => {
                theEbook = eArgs.Result;
                System.Console.WriteLine("Download Complete");
                GetStats();
            };
            Console.WriteLine($"Downloading");
            wc.DownloadStringAsync(new Uri("https://www.gutenberg.org/files/65849/65849-0.txt"));
        }
        private static void GetStats()
        {
            string[] words = theEbook.Split(new char[] {' ','\u000A',',','.',';',':','-','?','/'},StringSplitOptions.RemoveEmptyEntries);
            string[] tenMostCommonWords = null;
            GetTenMostCommonWords(words);
            string longestWord = string.Empty;
            GetLongestWord(words);
            Parallel.Invoke(()=>{tenMostCommonWords = GetTenMostCommonWords(words);},
                            ()=>{longestWord = GetLongestWord(words);});
            var sb = new StringBuilder();
            sb.AppendLine("The ten most commonWords are");
            foreach (string s in tenMostCommonWords)
            {
                sb.AppendLine(s);
            }
            sb.AppendLine($"The longest word is {longestWord}");
            Console.WriteLine($"{sb.ToString()}");            
        }

        private static string GetLongestWord(string[] words)
        {
            return words.OrderByDescending(w => w.Length)
            .FirstOrDefault();
        }

        private static string[] GetTenMostCommonWords(string[] words)
        {
             var frequencyOrder = from word in words
                where word.Length > 6
                group word by word into g
                orderby g.Count() descending
                select g.Key;

            string[] commonWords = (frequencyOrder.Take(10)).ToArray();
            return commonWords;
        }
    }
}