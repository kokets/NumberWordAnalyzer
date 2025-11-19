namespace NumberWordAnalyzer.Domain.Logic
{
	public class NumberWordsCounter
	{

		private static readonly string[] NumberOrder = new[]
		{
			"zero", "one", "two", "three", "four",
			"five", "six", "seven", "eight", "nine", "ten"
		};

		// Precompute letter counts for each number word
		private static readonly Dictionary<string, Dictionary<char, int>> WordLetters =
			NumberOrder.ToDictionary(w => w, w => CountLetters(w));

		/// <summary>
		/// Processes input string and returns dictionary with counts of each number word found.
		/// </summary>
		public async Task<Dictionary<string, int>> NumberWordProcesser(string? input)
		{
			// Initialize result with zeros for all words
			var result = NumberOrder.ToDictionary(w => w, _ => 0);

			if (string.IsNullOrWhiteSpace(input))
				return result;

			var letters = CountLetters(input);

			// Words with their unique identifying letters (null if none)
			var wordsWithUnique = new List<(string word, char? uniqueLetter)>
			{
				("zero", 'z'), ("two", 'w'), ("four", 'u'), ("six", 'x'), ("eight", 'g'),
				("three", 'h'), ("five", 'f'), ("seven", 's'), ("one", 'o'), ("nine", 'i'), ("ten", null)
			};

			bool progress;

			// Repeat until no more words can be found
			do
			{
				progress = false;

				foreach (var (word, unique) in wordsWithUnique)
				{
					// Skip if unique letter required and not present
					if (unique != null && letters.GetValueOrDefault(unique.Value, 0) == 0)
						continue;

					int maxFit = FitWord(letters, word);

					if (maxFit > 0)
					{
						result[word] += maxFit;
						SubtractLetters(letters, word, maxFit);
						progress = true;
					}
				}

			} while (progress);

			return result;
		}

		// Helper: subtract letters for the word times times
		private static void SubtractLetters(Dictionary<char, int> letters, string word, int times)
		{
			foreach (var (ch, need) in WordLetters[word])
			{
				letters[ch] = letters.GetValueOrDefault(ch, 0) - need * times;
				if (letters[ch] < 0) letters[ch] = 0;
			}
		}

		// Helper: find max times word can fit in available letters
		private static int FitWord(Dictionary<char, int> letters, string word)
		{
			int max = int.MaxValue;

			foreach (var (ch, need) in WordLetters[word])
			{
				int available = letters.GetValueOrDefault(ch, 0);
				max = Math.Min(max, available / need);
			}

			return max == int.MaxValue ? 0 : max;
		}

		// Helper: count letters in a string (case insensitive)
		private static Dictionary<char, int> CountLetters(string s)
		{
			var dict = new Dictionary<char, int>();

			foreach (char c in s.ToLower())
			{
				if (!char.IsLetter(c))
					continue;

				dict[c] = dict.GetValueOrDefault(c, 0) + 1;
			}

			return dict;
		}

	}
}
