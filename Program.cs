using System.Text;
using System.Text.RegularExpressions;

namespace home_TextAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            #region ReadingFile

            FileStream fs = new FileStream("1.txt", FileMode.OpenOrCreate, FileAccess.Read);

            byte[] readBytes = new byte[fs.Length];

            fs.Read(readBytes, 0, readBytes.Length);

            string readText = Encoding.UTF8.GetString(readBytes);

            #endregion

            readText = Regex.Replace(readText, @"[^\w\s]", string.Empty);

            Console.WriteLine(readText);

            string[] readTextArray = readText.ToLower().Split(new char[2]{ ' ', '\n' });

            for (int i = 0; i < readTextArray.Length; i++)
            {
                if (readTextArray[i].Length < 4) continue;

                if (words.ContainsKey(readTextArray[i])) words[readTextArray[i]]++;
                else
                {
                    words.Add(readTextArray[i],1);
                }
            }

            #region PrintTop & Sorting

            Console.WriteLine("+----+-----------------+--------+");
            Console.WriteLine("|{0,2}  | {1,-15} | {2,5} |", "№", "слово", "кол-во");
            Console.WriteLine("+----+-----------------+--------+");

            // https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
            var sortedWords = from entry in words orderby entry.Value descending select entry;

            int count = 0;
            foreach (var elem in sortedWords)
            {
                if (String.IsNullOrEmpty(elem.Key)) continue;
                Console.WriteLine("|{0,3:G} | {1,-18} | {2,6:G} |", count++, elem.Key, elem.Value);
                if (count == 51) break;
            }
            Console.WriteLine("+----+-----------------+--------+");

            #endregion

        }
    }
}