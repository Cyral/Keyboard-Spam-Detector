using System;
using System.Collections.Generic;
using System.Linq;
using Cyral.KeyboardSpamDetector.Properties;

namespace Cyral.KeyboardSpamDetector
{
    public class SpamDetector
    {
        private readonly double avgWordsToCharacters;
        private readonly int[,] map = new int[26, 26];

        public SpamDetector()
        {
            var lines = Resources.occurences.Split('\n');
            var ratio = lines[0].Split(':');
            avgWordsToCharacters = double.Parse(ratio[0]) / double.Parse(ratio[1]);
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(',');
                    var character = parts[0][0];
                    var next = parts[1][0];
                    var times = int.Parse(parts[2]);
                    map[character - 'a', next - 'a'] = times;
                }
            }
        }

        public bool IsSpam(string str)
        {
            var correct = 0d;
            str = str.ToLower();
            var words = 1;
            var chars = 0;
            var letters = new List<char>();
            for (var i = 0; i < str.Length - 1; i++)
            {
                var character = str[i];
                var next = str[i + 1];
                if (character == ' ' && IsLetter(next))
                    words++;
            }
            var wordMultiplier = Math.Min(words / 6d, 1.1); // Don't punish short phrases as much.
            for (var i = 0; i < str.Length - 1; i++)
            {
                var character = str[i];
                var next = str[i + 1];
                if (IsLetter(character) && IsLetter(next))
                {
                    chars++;
                    letters.Add(character);
                    if (OccurencesAfter(character, next) > AverageOccurencesOf(character) * wordMultiplier)
                    {
                        correct++;
                    }
                    else
                    {
                        correct -= .25;
                    }
                }
            }

            var entropy = letters.Distinct().Count() / Math.Min(chars / 10d, 1); // Don't punish short phrases for low entropy
            var correctOrderingRatio = (correct / chars);
            var goodEntropy = entropy / 20d;
            var wordToCharacterRatio = Math.Min(avgWordsToCharacters, words / (double) chars) / avgWordsToCharacters;
            return (correctOrderingRatio * .9) + (goodEntropy * .1) + (wordToCharacterRatio * .1) < .7;
        }

        private double AverageOccurencesOf(char character)
        {
            var occurences = new int[26];
            for (var x = 0; x < 26; x++)
            {
                occurences[x] = map[character - 'a', x];
            }
            return occurences.Average();
        }

        private static bool IsLetter(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private int OccurencesAfter(char character, char after)
        {
            return map[character - 'a', after - 'a'];
        }
    }
}