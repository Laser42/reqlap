using ReactiveUI;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Reactive;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    private HttpClient _httpClient = new HttpClient();

    private string _uri;
    public string Uri
    {
        get => _uri;
        set => this.RaiseAndSetIfChanged(ref _uri, value);
    }

    private string _requestBody;
    public string RequestBody
    {
        get => _requestBody;
        set => this.RaiseAndSetIfChanged(ref _requestBody, value);
    }

    private string _response;
    public string Response
    {
        get => _response;
        set => this.RaiseAndSetIfChanged(ref _response, value);
    }

    public ReactiveCommand<Unit, Unit> SendRequestCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    public MainViewModel()
    {
        SendRequestCommand = ReactiveCommand.CreateFromTask(SendRequestAsync);
        ClearCommand = ReactiveCommand.Create(Clear);
    }

    private async Task SendRequestAsync()
    {
        try
        {
            var content = new StringContent(RequestBody, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = null;
            // Отправляем запрос в зависимости от выбранного HTTP метода
            // Для примера используем POST
            responseMessage = await _httpClient.PostAsync(Uri, content);

            Response = await responseMessage.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Response = $"An error occurred: {ex.Message}";
        }
    }

    private void Clear()
    {
        Uri = string.Empty;
        RequestBody = string.Empty;
        Response = string.Empty;
    }
}
