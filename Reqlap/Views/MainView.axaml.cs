using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Net.Http;

namespace Reqlap.Views;

public partial class MainView : UserControl
{
    private ComboBox _httpMethodComboBox;
    private TextBox _uriTextBox;

    public MainView()
    {
        InitializeComponent();
        _httpMethodComboBox = this.FindControl<ComboBox>("HttpMethodComboBox")!;
        _uriTextBox = this.FindControl<TextBox>("UriTextBox")!;

        var requestMethods = new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Put, HttpMethod.Patch, HttpMethod.Delete };
        foreach (var requestMethod in requestMethods)
        {
            _httpMethodComboBox.Items.Add(requestMethod);
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void HttpMethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Обработчик события изменения выбранного элемента в выпадающем списке
        var selectedRequestMethod = (HttpMethod)_httpMethodComboBox.SelectedItem!;
        _uriTextBox.Text = selectedRequestMethod.Method;
    }
}
