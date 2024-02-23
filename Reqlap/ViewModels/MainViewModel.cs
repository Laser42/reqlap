using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    public bool IsRequestInProgress
    {
        get { return _isRequestInProgress; }
        set { this.RaiseAndSetIfChanged(ref _isRequestInProgress, value); }
    }

    public HttpRequestMessage? Request
    {
        get { return _request; }
        set { this.RaiseAndSetIfChanged(ref _request, value); }
    }

    public string ResponseStatus
    {
        get { return _responseStatus; }
        set { this.RaiseAndSetIfChanged(ref _responseStatus, value); }
    }

    public string ResponseBody
    {
        get { return _responseBody; }
        set { this.RaiseAndSetIfChanged(ref _responseBody, value); }
    }

    public string BackgroundImagePath { get; }

    private readonly HttpClient _httpClient;
    private readonly ThemeVariant _actualThemeVariant;

    private bool _isRequestInProgress = false;
    private HttpRequestMessage? _request;
    private string _responseStatus;
    private string _responseBody;

    public MainViewModel(HttpClient httpClient, ThemeVariant actualThemeVariant)
    {
        _httpClient = httpClient;
        _actualThemeVariant = actualThemeVariant;
        BackgroundImagePath = $"/Assets/bg720{_actualThemeVariant.Key.ToString()!.ToLowerInvariant}.jpg";
        BackgroundImagePath = $"/Assets/bg720dark.jpg";

        this.WhenAnyValue(x => x.Request)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(SendRequestAsync!);
    }

    private async void SendRequestAsync(HttpRequestMessage request)
    {
        if (request == null)
            return;

        IsRequestInProgress = true;
        ResponseBody = string.Empty;
        ResponseStatus = "In progress...";

        try
        {
            var response = await _httpClient.SendAsync(request);
            ResponseBody = await response.Content.ReadAsStringAsync();
            ResponseStatus = $"{(int)response.StatusCode} {response.StatusCode}";
        }
        catch (Exception ex)
        {
            ResponseBody = ex.Message;
            ResponseStatus = "Error";
        }

        IsRequestInProgress = false;
    }
}
