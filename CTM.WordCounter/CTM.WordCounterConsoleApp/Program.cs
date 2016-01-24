using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCounterConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter filename");
            var filename = Console.ReadLine();
            string text;
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    using (var bs = new BufferedStream(fs))
                    {
                        using (var reader = new StreamReader(bs))
                        {
                            text = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Press any key to exit.");
                Console.ReadLine();
                return;
            }

            var calculator = new WordCountCalculator.WordCountCalculator();
            calculator.CalculateAndUpdateWordCount(text);

            foreach(var word in calculator.CountedWords)
            {
                Console.WriteLine($"Word: {word.Word} count: {word.Count} isPrimeNumber: {word.IsPrime.ToString()}");
            }

            Console.WriteLine($"Total words: {calculator.CountedWords.Sum(w => w.Count)}");
            Console.WriteLine($"Total unique words: {calculator.CountedWords.Count()}");
            Console.WriteLine("Word count complete. Press any key to exit program");
            Console.ReadLine();
        }
    }
}
