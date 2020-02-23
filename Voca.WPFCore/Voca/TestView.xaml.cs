using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Voca
{
	/// <summary>
	/// Interaction logic for TestView.xaml
	/// </summary>
	public partial class TestView : Page
	{
		private Tester _tester;

		public TestView()
		{
			InitializeComponent();

			Loaded += TestView_Loaded;
		}

		private async void TestView_Loaded(object sender, RoutedEventArgs e)
		{
			Loaded -= TestView_Loaded;

			var loader = new Loader(App.DefaultDataPath);
			var data = await loader.LoadAsync();
			_tester = new Tester(data);
			UpdateModeText();

			Next();
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			Next();
		}

		private void TranslateInput_KeyUp(object sender, KeyEventArgs e)
		{
			var result = _tester.Check(TranslateInput.Text);
			if (result)
			{
				if (e.Key == Key.Enter)
				{
					Next();
					return;
				}

				TranslateInput.Background = new SolidColorBrush(Color.FromRgb(0xD5, 0xFF, 0xD8));
			}
			else
			{
				if (TranslateInput.Text == string.Empty)
					TranslateInput.Background = new SolidColorBrush(Colors.White);
				else
					TranslateInput.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xDC, 0xD5));
			}
		}

		private void Next()
		{
			var left = _tester.Left;
			var next = _tester.Next();

			if (_tester.FullTest && left == 1)
			{
				UpdateStatus(0);
				MessageBox.Show("Test Completed!");
			}

			TranslateInput.Background = new SolidColorBrush(Colors.White);

			SourceButton.Content = next;
			SourceButton.IsChecked = false;

			UpdateStatus(_tester.Left);

			TranslateInput.Text = string.Empty;
			TranslateInput.Focus();
		}

		private void SourceButton_Checked(object sender, RoutedEventArgs e)
		{
			SourceButton.Content = $"{_tester.GetCurrent()}/{_tester.GetTranslate()}";
		}

		private void SourceButton_Unchecked(object sender, RoutedEventArgs e)
		{
			SourceButton.Content = $"{_tester.GetCurrent()}";
		}

		private void SwapButton_Click(object sender, RoutedEventArgs e)
		{
			_tester.Direction = !_tester.Direction;
			Next();
		}

		private void ModeButton_Click(object sender, RoutedEventArgs e)
		{
			_tester.FullTest = !_tester.FullTest;
			UpdateModeText();
			UpdateStatus(_tester.Left);
			TranslateInput.Focus();
		}

		private void UpdateModeText()
		{
			ModeButton.Content = _tester.FullTest ? "Start Random Test" : "Start Full Test";
		}

		private void UpdateStatus(int count)
		{
			Count.Text = $"{count} item(s)";
		}
	}
}
