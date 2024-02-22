using Avalonia.Controls;
using Avalonia.Interactivity;
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
                //_errorTextBox.Text = "Метод не указан";
                return;
            }

            if (string.IsNullOrWhiteSpace(UriTextBox.Text))
            {
                //_errorTextBox.Text = "URI не указан";
                return;
            }

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(UriTextBox.Text),
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
    }
}
