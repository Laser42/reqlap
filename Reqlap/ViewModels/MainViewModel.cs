using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<HttpMethod> HttpMethods { get; }

    public ReactiveCommand<HttpRequestMessage, HttpResponseMessage?> SendRequestCommand { get; }

    public string ResponseContent { get; private set; }

    public HttpStatusCode ResponseStatusCode { get; private set; }

    public bool IsRequestInProgress { get; private set; }

    private readonly HttpClient _httpClient;

    public MainViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;

        HttpMethods = [HttpMethod.Get,
            HttpMethod.Post,
            HttpMethod.Put,
            HttpMethod.Patch,
            HttpMethod.Delete,
            HttpMethod.Head,
            HttpMethod.Options,
            HttpMethod.Connect,
            HttpMethod.Trace];

        SendRequestCommand = ReactiveCommand.CreateFromTask<HttpRequestMessage, HttpResponseMessage?>(
            async request =>
        {
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
        SendRequestCommand.Subscribe( async result =>
        {
            if (result != null)
            {
                // Обрабатываем успешный результат
                ResponseStatusCode = result.StatusCode;
                ResponseContent = await result.Content.ReadAsStringAsync();
                
                // Сообщаем UI об изменении
                this.RaisePropertyChanged(nameof(ResponseStatusCode));
                this.RaisePropertyChanged(nameof(ResponseContent));
            }
            else
            {
                // Обрабатываем ошибку
                ResponseStatusCode = 0;
                
                // Сообщаем UI об изменении
                this.RaisePropertyChanged(nameof(ResponseStatusCode));
                this.RaisePropertyChanged(nameof(ResponseContent));
            }
        });

        // Обработка изменения состояния команды (необязательно)
        SendRequestCommand.IsExecuting.Subscribe(isExecuting =>
        {
            IsRequestInProgress = isExecuting;
            this.RaisePropertyChanged(nameof(IsRequestInProgress));
        });
    }

}
