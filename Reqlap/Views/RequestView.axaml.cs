using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using ReactiveUI;
using Reqlap.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;

namespace Reqlap.Views
{
    public partial class RequestView : UserControl
    {
        public RequestView()
        {
            InitializeComponent();

            var requestMethods = new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Put,
                HttpMethod.Patch, HttpMethod.Delete, HttpMethod.Head, HttpMethod.Options,
                HttpMethod.Connect, HttpMethod.Trace };
            foreach (var requestMethod in requestMethods)
            {
                HttpMethodComboBox.Items.Add(requestMethod);
            }
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs args)
        {
            if (HttpMethodComboBox.SelectedValue == null)
            {
                ShowError("Method not selected");
                return;
            }

            if (string.IsNullOrWhiteSpace(UriTextBox.Text))
            {
                ShowError("URI is empty");
                return;
            }
            Uri uri;
            try
            {
                uri = new Uri(UriTextBox.Text);
            }
            catch
            {
                ShowError("URI is invalid");
                return;
            }

            ClearError();

            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = (HttpMethodComboBox.SelectedValue as HttpMethod)!
            };

            if (!string.IsNullOrWhiteSpace(RequestBodyTextBox.Text))
            {

                request.Content = new StringContent(RequestBodyTextBox.Text);

                try
                {
                    JsonDocument.Parse(RequestBodyTextBox.Text);
                    request.Headers.Add(HttpRequestHeader.ContentType.ToString(),
                        new[] { MediaTypeNames.Application.Json });
                }
                catch
                {

                    request.Headers.Add(HttpRequestHeader.ContentType.ToString(),
                        new[] { MediaTypeNames.Text.Plain });
                }
            }

            var mainViewModel = (DataContext as MainViewModel)!;
            mainViewModel.Request = request;
        }

        private void ShowError(string message)
        {
            RequestErrorTextBlock.Text = message;
            RequestErrorTextBlock.Height = 20;
        }

        private void ClearError()
        {
            RequestErrorTextBlock.Text = string.Empty;
            RequestErrorTextBlock.Height = 0;
        }
    }
}
