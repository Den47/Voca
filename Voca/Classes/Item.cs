﻿namespace Voca
{
	public class Item
	{
		public Item()
		{
		}

		public Item(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public string Key { get; set; }

		public string Value { get; set; }
	}
}
