using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.ItemTemplateSelectors
{
    public class DataRowItemTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var datarow = item as DataRowOM;

            var dataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = 50f });
                for(int i = 0; i < datarow.Values.Count; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    var border = new Border() { Stroke = Colors.Gray, StrokeThickness = 1d, BackgroundColor = Colors.Transparent };
                    grid.Children.Add(border);
                    Grid.SetColumn(border, i);
                    var value = new Label { Text = datarow.Values[i].Value.ToString() };
                    value.HorizontalOptions = LayoutOptions.Fill;
                    value.VerticalOptions = LayoutOptions.Fill;
                    value.HorizontalTextAlignment = TextAlignment.Center;
                    value.VerticalTextAlignment = TextAlignment.Center;
                    value.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = datarow.EditValueCommand,
                        CommandParameter = datarow.Values[i]
                    });

                    grid.Children.Add(value);
                    Grid.SetColumn(value, i);
                }
                return grid;
            });

            return dataTemplate;
        }
    }
}
