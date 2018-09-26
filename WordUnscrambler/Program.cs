using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    class Program
    {
        private const string wordListFileName = "wordlist.txt";

        private static readonly FileReader _fileReader = new FileReader();

        private static readonly  WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {
                Console.WriteLine("Enter the option - F for File , M for Manual");

                var option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case "F":
                        Console.Write("Enter scrambled words filename: ");
                        ExecuteScrambledWordsInFileScenario();
                            break;
                    case "M":
                        Console.Write("Enter scrambled words manually: ");
                        ExceuteScrambledWordsManualEntryScenario();
                            break;
                        default:
                            Console.WriteLine("Option was not recognized");
                            break;
                }

                var continueDecision = string.Empty;
                do
                {
                    Console.WriteLine("Do you want to continue Y -> Yes , N -> No");
                    continueDecision = (Console.ReadLine() ?? String.Empty);
                    continueWordUnscramble =
                    continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);

                } while (!continueDecision.Equals("Y",StringComparison.OrdinalIgnoreCase) 
                         && !continueDecision.Equals("N",StringComparison.OrdinalIgnoreCase));

            } while (continueWordUnscramble);
        }

        private static void ExceuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? String.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWord(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var fileName = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(fileName);
            DisplayMatchedUnscrambledWord(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWord(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);
            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Match Found for {0} : {1}", matchedWord.ScrambledWord,matchedWord.Word); 
                }
            }
            else
            {
                Console.WriteLine("No Matches Have Been Found");
            }
        }
    }
}
