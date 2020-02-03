using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Voca
{
	/// <summary>
	/// Interaction logic for VocabularyGridView.xaml
	/// </summary>
	public partial class VocabularyGridView : Page
	{
		private readonly Loader _loader;

		public VocabularyGridView()
		{
			InitializeComponent();

			_loader = new Loader(App.DefaultDataPath);

			Loaded += VocabularyView_Loaded;
		}

		public ObservableCollection<Item> Data { get; set; }

		private async void VocabularyView_Loaded(object sender, RoutedEventArgs e)
		{
			Loaded -= VocabularyView_Loaded;

			var data = await _loader.LoadAsync();
			Data = new ObservableCollection<Item>(data.Select(x => new Item(x.Key, x.Value)));

			Table.DataContext = Data;
			Data.CollectionChanged += Data_CollectionChanged;
			UpdateStatus(data.Count);

			NavigationService.Navigating += NavigationService_Navigating;
		}

		private async void NavigationService_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			NavigationService.Navigating -= NavigationService_Navigating;

			await _loader.UpdateAsync(Data);
		}

		private async void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				var items = e.OldItems.Cast<Item>().Select(x => x.Key);
				await _loader.RemoveAsync(items);
				UpdateStatus(_loader.Count);
			}
		}

		private void AddRowButton_Click(object sender, RoutedEventArgs e)
		{
			Table.Items.Add(new Item());
		}

		private void Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			var count = Data.Count(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value));
			UpdateStatus(count);
		}

		private void UpdateStatus(int count)
		{
			Count.Text = $"{count} item(s). Back to save!";
		}
	}
}
