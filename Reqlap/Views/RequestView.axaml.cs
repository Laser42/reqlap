using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Net.Http;

namespace Reqlap.Views
{
    public partial class RequestView : UserControl
    {
        private ComboBox _httpMethodComboBox;
        private TextBox _uriTextBox;
        public RequestView()
        {
            InitializeComponent();

            _httpMethodComboBox = this.FindControl<ComboBox>("HttpMethodComboBox")!;
            _uriTextBox = this.FindControl<TextBox>("UriTextBox")!;

            var requestMethods = new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Put,
            HttpMethod.Patch, HttpMethod.Delete, HttpMethod.Head, HttpMethod.Options,
            HttpMethod.Connect, HttpMethod.Trace };
            foreach (var requestMethod in requestMethods)
            {
                _httpMethodComboBox.Items.Add(requestMethod);
            }
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs args)
        {
            Console.WriteLine("Меня нажали!");
        }
    }
}
