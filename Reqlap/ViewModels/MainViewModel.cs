using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ReactiveCommand<HttpRequestMessage, HttpResponseMessage?> SendRequestCommand { get; }

    public string ResponseContent
    {
        get { return _responseContent; }
        set { this.RaiseAndSetIfChanged(ref _responseContent, value); }
    }

    public string ResponseStatusCode
    {
        get { return _responseStatusCode; }
        set { this.RaiseAndSetIfChanged(ref _responseStatusCode, value); }
    }

    public bool IsRequestInProgress
    {
        get { return _isRequestInProgress; }
        set { this.RaiseAndSetIfChanged(ref _isRequestInProgress, value); }
    }

    private readonly HttpClient _httpClient;
    private string _responseContent = string.Empty;
    private string _responseStatusCode = string.Empty;
    private bool _isRequestInProgress = false;

    public MainViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;

        SendRequestCommand = ReactiveCommand.CreateFromTask<HttpRequestMessage, HttpResponseMessage?>(
            async request =>
        {
            ResponseStatusCode = "Sending...";
            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                ResponseContent = ex.Message;
                return null;
            }
        });

        // Подписка на результат выполнения команды (необязательно)
        /*SendRequestCommand.Subscribe( async result =>
        {
            if (result != null)
            {
                ResponseStatusCode = $"{(int)result.StatusCode} {result.StatusCode}";
                ResponseContent = await result.Content.ReadAsStringAsync();
            }
            else
            {
                ResponseStatusCode = "Unknown error";
            }
        });*/

        // Обработка изменения состояния команды (необязательно)
        SendRequestCommand.IsExecuting.Subscribe(isExecuting =>
        {
            IsRequestInProgress = isExecuting;
        });
    }
}
