using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voca
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

		public async Task<List<Item>> LoadAsync()
		{
			var path = FilePath;

			if (!File.Exists(path))
			{
				await File.WriteAllTextAsync(path, string.Empty, Encoding.UTF8);
				_vocabulary = new List<Item>();
				return new List<Item>();
			}

			var lines = await File.ReadAllLinesAsync(path);

			_vocabulary = lines.Select(x => ParseSafe(x)).Where(x => !string.IsNullOrEmpty(x.Item1 + x.Item2)).ToList();

			return new List<Item>(_vocabulary);
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

		private async Task SaveAsync()
		{
			var data = _vocabulary.Select(x => $"{x.Item1},{x.Item2}");

			await File.WriteAllLinesAsync(FilePath, data, Encoding.UTF8);
		}
	}
}
