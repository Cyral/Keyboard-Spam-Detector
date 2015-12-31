using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cyral.KeyboardSpamDetector.Data
{
    internal class DataFinder
    {
        private static void Main(string[] args)
        {
            var text = File.ReadAllText("data.txt");
            var map = new int[26,26];
            var words = 0;
            var characters = 0;
            for (var i = 0; i < text.Length - 1; i++)
            {
                var character = char.ToLowerInvariant(text[i]);
                var next = char.ToLowerInvariant(text[i + 1]);
                if (IsLetter(character) && IsLetter(next))
                {
                    characters++;
                    map[character - 'a', next - 'a']++;
                }
                else if (character == ' '&& IsLetter(next))
                {
                    words++;
                }
            }
            var maxNext = 0;

            var builder = new StringBuilder();
            builder.AppendLine(words + ":" + characters);
            for (var x = 'a'; x <= 'z'; x++)
            {
                for (var y = 'a'; y <= 'z'; y++)
                {
                    var value = map[x - 'a', y - 'a'];
                    builder.AppendLine(x+ "," + y + "," + value);
                    Console.WriteLine(x + "->" + y + " " + value);
                    if (value > maxNext)
                        maxNext = value;
                }
            }
            File.WriteAllText("occurences.txt", builder.ToString());
            Console.WriteLine("Word to Character Ratio: " + words + ":" + characters + " (" + Math.Round(words / (double)characters, 2) + ")");
            Console.WriteLine("Max Next: " + maxNext);
            Console.WriteLine("Done. Parsed " + characters + " characters.");
            Console.ReadLine();
        }

        private static bool IsLetter(char c)
        {
            return c >= 'a' && c <= 'z';
        }
    }
}