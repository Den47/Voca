using System;
using System.Collections.Generic;
using System.Linq;

namespace Voca
{
	public class Tester
	{
		private readonly Random _random;
		private readonly IDictionary<string, string> _vocabulary;
		private readonly List<string> _keys;

		private int _currentIndex = -1;

		public Tester(IDictionary<string, string> vocabulary)
		{
			_random = new Random();

			if (vocabulary == null || vocabulary.Count == 0)
				throw new ArgumentNullException();

			_vocabulary = new Dictionary<string, string>(vocabulary);
			_keys = _vocabulary.Keys.ToList();
		}

		public string CurrentKey { get; private set; }

		public string CurrentTranslate => _vocabulary[CurrentKey].ToLowerInvariant();

		public bool Check(string translate)
		{
			return _vocabulary[CurrentKey].ToLowerInvariant() == translate.ToLowerInvariant();
		}

		public string Next()
		{
			int index;
			do { index = _random.Next(0, _keys.Count); }
			while (_currentIndex == index);

			_currentIndex = index;
			CurrentKey = _keys[index];
			return CurrentKey;
		}
	}
}
