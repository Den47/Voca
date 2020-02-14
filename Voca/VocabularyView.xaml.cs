using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Voca
{
	/// <summary>
	/// Interaction logic for VocabularyView.xaml
	/// </summary>
	public partial class VocabularyView : Page
	{
		private readonly Loader _loader;

		public VocabularyView()
		{
			InitializeComponent();

			_loader = new Loader(App.DefaultDataPath);

			Loaded += VocabularyView_Loaded;
		}

		private async void VocabularyView_Loaded(object sender, RoutedEventArgs e)
		{
			Loaded -= VocabularyView_Loaded;

			var data = await _loader.LoadAsync();

			Count.Text = $"{data.Count} item(s)";
		}

		private async void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(SourceInput.Text))
				return;
			if (string.IsNullOrEmpty(TranslateInput.Text))
				return;

			var item = new Item(SourceInput.Text.Trim().ToLowerInvariant(), TranslateInput.Text.Trim().ToLowerInvariant());
			await _loader.AddAsync(new List<Item> { item });

			SourceInput.Text = string.Empty;
			TranslateInput.Text = string.Empty;
			Count.Text = $"{_loader.Count} item(s)";

			SourceInput.Focus();
		}

		private void FileButton_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Environment.CurrentDirectory);
		}

		private void SourceInput_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TranslateInput.Focus();
		}

		private void TranslateInput_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				SubmitButton_Click(null, null);
		}
	}
}
