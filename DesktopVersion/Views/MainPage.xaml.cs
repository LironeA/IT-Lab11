using DesktopVersion.Models.ObservableModels;
using DesktopVersion.ViewModels;
using System.Collections.ObjectModel;

namespace DesktopVersion.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;

        public MainPage(MainPageViewModel vm)
        {
            _vm = vm;
            _vm.ConstructHeaderAction = ConstructHeaderGrid;
            this.BindingContext = _vm;
            InitializeComponent();
        }

        public void ConstructHeaderGrid(ObservableCollection<ColumnOM> columns)
        {
            HeaderGrid.Children.Clear();
            HeaderGrid.ColumnDefinitions.Clear();

            foreach(var column in columns)
            {
                HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                var label = new Label()
                {
                    Text = column.Name,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black,
                };

                Grid.SetColumn(label, HeaderGrid.Children.Count);
                HeaderGrid.Children.Add(label);
            }

        }
    }

}
