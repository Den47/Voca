using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Voca
{
	/// <summary>
	/// Interaction logic for StartView.xaml
	/// </summary>
	public partial class StartView : Page
	{
		public StartView()
		{
			InitializeComponent();

			Loaded += StartView_Loaded;
		}

		private async void StartView_Loaded(object sender, RoutedEventArgs e)
		{
			Loaded -= StartView_Loaded;

			var data = await (new Loader(App.DefaultDataPath)).LoadAsync();

			TestButton.IsEnabled = data.Count > 0;
		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GetNavigationService(this).Navigate(new Uri("TestView.xaml", UriKind.RelativeOrAbsolute));
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GetNavigationService(this).Navigate(new Uri("VocabularyView.xaml", UriKind.RelativeOrAbsolute));
		}

		private void VocabularyButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GetNavigationService(this).Navigate(new Uri("VocabularyGridView.xaml", UriKind.RelativeOrAbsolute));
		}
	}
}
