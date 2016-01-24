using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCounterAlternateConsoleApp
{
    // Reads file one line at a time instead of reading whole file in 1 go.
    // The benefit of this is a much small memory footprint but may potentially be slower.
    // This approach also opens the possibility of doing some processing in parallel or even asynchronously.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter filename");
            var filename = Console.ReadLine();
            var calculator = new WordCountCalculator.WordCountCalculator();

            string text = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                using (var bs = new BufferedStream(fs))
                using (var reader = new StreamReader(bs))
                {
                    while (text != null)
                    {
                        text = reader.ReadLine();
                        calculator.CalculateAndUpdateWordCount(text);
                    }

                    reader.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Press any key to exit.");
                Console.ReadLine();
                return;
            }

            foreach (var word in calculator.CountedWords)
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
