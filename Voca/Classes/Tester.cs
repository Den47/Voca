using System;
using System.Collections.Generic;
using System.Linq;

namespace Voca
{
	public class Tester
	{
		private readonly Random _random;
		private readonly List<Item> _vocabulary;

		private string _current;

		public Tester(List<Item> vocabulary)
		{
			_random = new Random();

			if (vocabulary == null || vocabulary.Count == 0)
				throw new ArgumentNullException();

			_vocabulary = new List<Item>(vocabulary);
		}

		public bool Direction { get; set; } = true;

		private Item CurrentItem { get; set; }

		public bool Check(string translate)
		{
			return GetTranslate()?.ToLowerInvariant() == translate.ToLowerInvariant();
		}

		public string Next()
		{
			if (_vocabulary.Count == 1)
			{
				CurrentItem = _vocabulary.First();
				return GetCurrent();
			}

			Item current;
			do { current = _vocabulary[_random.Next(0, _vocabulary.Count)]; }
			while (_current == current.Item1);

			_current = current.Item1;
			CurrentItem = current;
			return GetCurrent();
		}

		public string GetCurrent()
		{
			if (CurrentItem == null)
				return null;

			return Direction ? CurrentItem.Item1 : CurrentItem.Item2;
		}

		public string GetTranslate()
		{
			if (CurrentItem == null)
				return null;

			return Direction ? CurrentItem.Item2 : CurrentItem.Item1;
		}
	}
}
