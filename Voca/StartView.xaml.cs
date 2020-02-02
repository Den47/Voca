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
		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GetNavigationService(this).Navigate(new Uri("TestView.xaml", UriKind.RelativeOrAbsolute));
		}

		private void VocabularyButton_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GetNavigationService(this).Navigate(new Uri("VocabularyView.xaml", UriKind.RelativeOrAbsolute));
		}
	}
}
