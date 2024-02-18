using ReactiveUI;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Reactive;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Reactive.Linq;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;

    public ObservableCollection<HttpMethod> HttpMethods { get; }

    public ReactiveCommand<HttpRequestMessage,HttpResponseMessage> SendRequestCommand { get; } 
     
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

        SendRequestCommand = ReactiveCommand.CreateFromTask<HttpRequestMessage>(async (request) =>
        {
            await _httpClient.SendAsync(request);
        });
    }

}
