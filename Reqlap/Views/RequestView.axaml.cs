using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Net.Http;
using System.Text;

namespace Reqlap.Views
{
    public partial class RequestView : UserControl
    {
        private readonly ComboBox _httpMethodComboBox;
        private readonly TextBox _uriTextBox;
        private readonly TextBox _requestBodyTextBox;
        private readonly Button _sendButton;
        public RequestView()
        {
            InitializeComponent();

            _httpMethodComboBox = this.FindControl<ComboBox>("HttpMethodComboBox")!;
            _uriTextBox = this.FindControl<TextBox>("UriTextBox")!;
            _requestBodyTextBox = this.FindControl<TextBox>("RequestBodyTextBox")!;
            _sendButton = this.FindControl<Button>("SendButton")!;

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
            _requestBodyTextBox.Clear();
            _requestBodyTextBox.Text = new StringBuilder()
                .AppendLine((_httpMethodComboBox.SelectedItem as HttpMethod)?.Method)
                .AppendLine(_uriTextBox.Text)
                .ToString();
        }
    }
}
