﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voca.Classes
{
	public class Loader
	{
		private readonly string _filePath;

		private List<Item> _vocabulary;

		public Loader(string relativeFilePath)
		{
			_vocabulary = new List<Item>();
			_filePath = relativeFilePath;
		}

		public int Count => _vocabulary.Count;

		public string FilePath => $"{Environment.CurrentDirectory}\\{_filePath}";

		public async Task AddAsync(IEnumerable<Item> items)
		{
			if (_vocabulary == null)
				await LoadAsync();

			_vocabulary.AddRange(items);

			await SaveAsync();
		}

		public Task<List<Item>> LoadAsync()
		{
			var path = FilePath;

			if (!File.Exists(path))
			{
				File.WriteAllText(path, string.Empty, Encoding.UTF8);
				_vocabulary = new List<Item>();
				return Task.FromResult(new List<Item>());
			}

			var lines = File.ReadAllLines(path);

			_vocabulary = lines.Select(x => ParseSafe(x)).Where(x => !string.IsNullOrEmpty(x.Item1 + x.Item2)).ToList();

			return Task.FromResult(new List<Item>(_vocabulary));
		}

		public async Task RemoveAsync(IEnumerable<Item> items)
		{
			if (_vocabulary == null)
				await LoadAsync();

			foreach (var item in items)
			{
				_vocabulary.Remove(item);
			}

			await SaveAsync();
		}

		public async Task UpdateAsync(IEnumerable<Item> items)
		{
			if (items == null)
				_vocabulary = new List<Item>();
			else
				_vocabulary = new List<Item>(items);

			await SaveAsync();
		}

		private Item ParseSafe(string line)
		{
			try
			{
				var split = line.Split(':', ' ', '=', ',');
				return new Item(split[0], split[1]);
			}
			catch
			{
				return new Item(string.Empty, string.Empty);
			}
		}

		private Task SaveAsync()
		{
			var data = _vocabulary.Select(x => $"{x.Item1},{x.Item2}");

			File.WriteAllLines(FilePath, data, Encoding.UTF8);

			return Task.CompletedTask;
		}
	}
}
