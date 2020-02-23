using System;
using System.Collections.Generic;
using System.Linq;

namespace Voca
{
	public class Tester
	{
		private readonly Random _random;
		private readonly List<Item> _vocabulary;

		private List<Item> _testList;
		private string _current;
		private bool _fullTest;

		public Tester(List<Item> vocabulary)
		{
			_random = new Random();

			if (vocabulary == null || vocabulary.Count == 0)
				throw new ArgumentNullException();

			_vocabulary = new List<Item>(vocabulary);

			FullTest = true;
		}

		public int Left => List.Count();

		public bool Direction { get; set; } = true;

		public bool FullTest
		{
			get => _fullTest;
			set
			{
				_fullTest = value;

				if (value)
				{
					List = _testList = new List<Item>(_vocabulary);
				}
				else
				{
					List = _vocabulary;
				}
			}
		}

		private Item CurrentItem { get; set; }

		private List<Item> List { get; set; }

		public bool Check(string translate)
		{
			return GetTranslate()?.ToLowerInvariant() == translate.ToLowerInvariant();
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

		public string Next()
		{
			if (FullTest)
				_testList.Remove(CurrentItem);

			if (List.Count == 0 && FullTest && _vocabulary.Count > 0)
			{
				_testList.AddRange(_vocabulary);
			}

			if (List.Count == 1 && !FullTest)
			{
				CurrentItem = List.First();
				return GetCurrent();
			}

			Item current;
			do { current = List[_random.Next(0, List.Count)]; }
			while (_current == current.Item1);

			_current = current.Item1;
			CurrentItem = current;

			return GetCurrent();
		}
	}
}
