using ReactiveUI;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Reactive;
using System.Collections.ObjectModel;

namespace Reqlap.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<HttpMethod> HttpMethods { get; }

    public MainViewModel()
    {
        HttpMethods = [HttpMethod.Get,
            HttpMethod.Post,
            HttpMethod.Put,
            HttpMethod.Patch,
            HttpMethod.Delete,
            HttpMethod.Head,
            HttpMethod.Options,
            HttpMethod.Connect,
            HttpMethod.Trace];
    }
}
