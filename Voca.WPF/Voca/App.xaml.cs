using System.Windows;

namespace Voca
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public const string DefaultDataPath = "VocaData.csv";

		private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			Application.Current.MainWindow.Height = 400;
			Application.Current.MainWindow.Width = 600;
			Application.Current.MainWindow.MinHeight = 240;
			Application.Current.MainWindow.MinWidth = 360;
		}
	}
}
