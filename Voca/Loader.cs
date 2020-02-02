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

		private Dictionary<string, string> _vocabulary;

		public Loader(string relativeFilePath)
		{
			_vocabulary = new Dictionary<string, string>();
			_filePath = relativeFilePath;
		}

		public int Count => _vocabulary.Count;

		public string FilePath => $"{Environment.CurrentDirectory}\\{_filePath}";

		public async Task AddAsync(string key, string value)
		{
			if (_vocabulary == null)
				await LoadAsync();

			_vocabulary[key] = value;

			await SaveAsync();
		}

		public async Task<IReadOnlyDictionary<string, string>> LoadAsync()
		{
			var path = FilePath;

			if (!File.Exists(path))
			{
				await File.WriteAllTextAsync(path, string.Empty, Encoding.UTF8);
				_vocabulary = new Dictionary<string, string>();
				return new Dictionary<string, string>();
			}

			var lines = await File.ReadAllLinesAsync(path);

			var result = lines.Select(x => ParseSafe(x))
							  .Where(x => !string.IsNullOrEmpty(x.Key))
							  .ToDictionary(x => x.Key, y => y.Value);

			_vocabulary = new Dictionary<string, string>(result);

			return result;
		}

		public async Task RemoveAsync(string key)
		{
			if (_vocabulary == null)
				await LoadAsync();

			if (_vocabulary.ContainsKey(key))
				_vocabulary.Remove(key);

			await SaveAsync();
		}

		private KeyValuePair<string, string> ParseSafe(string line)
		{
			try
			{
				var split = line.Split(':', ' ', '=');
				return KeyValuePair.Create(split[0], split[1]);
			}
			catch
			{
				return KeyValuePair.Create(string.Empty, string.Empty);
			}
		}

		private async Task SaveAsync()
		{
			var data = _vocabulary.Select(x => $"{x.Key}:{x.Value}");

			await File.WriteAllLinesAsync(FilePath, data, Encoding.UTF8);
		}
	}
}
