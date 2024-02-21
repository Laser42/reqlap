using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;

namespace Reqlap.Views
{
    public partial class RequestView : UserControl
    {
        private readonly ComboBox _httpMethodComboBox;
        private readonly TextBox _uriTextBox;
        private readonly TextBox _requestBodyTextBox;
        private readonly Button _sendButton;
        private readonly TextBox _errorTextBox;
        public RequestView()
        {
            InitializeComponent();

            _httpMethodComboBox = this.FindControl<ComboBox>("HttpMethodComboBox")!;
            _uriTextBox = this.FindControl<TextBox>("UriTextBox")!;
            _requestBodyTextBox = this.FindControl<TextBox>("RequestBodyTextBox")!;
            _sendButton = this.FindControl<Button>("SendButton")!;
            _errorTextBox = this.FindControl<TextBox>("ErrorTextBox")!;

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
            if (_httpMethodComboBox.SelectedValue == null)
            {
                _errorTextBox.Text = "Метод не указан";
                return;
            }

            if (string.IsNullOrWhiteSpace(_uriTextBox.Text))
            {
                _errorTextBox.Text = "URI не указан";
                return;
            }

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_uriTextBox.Text),
                Method = _httpMethodComboBox.SelectedValue as HttpMethod
            };

            if (!string.IsNullOrWhiteSpace(_requestBodyTextBox.Text))
            {

                request.Content = new StringContent(_requestBodyTextBox.Text);

                try
                {
                    JsonDocument.Parse(_requestBodyTextBox.Text);
                    request.Headers.Add(HttpRequestHeader.ContentType.ToString(),
                        new[] { MediaTypeNames.Application.Json });
                }
                catch
                {

                    request.Headers.Add(HttpRequestHeader.ContentType.ToString(),
                        new[] { MediaTypeNames.Text.Plain });
                }
            }
        }
    }
}
