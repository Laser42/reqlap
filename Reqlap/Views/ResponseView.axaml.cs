using Avalonia.Controls;

namespace Reqlap.Views
{
    public partial class ResponseView : UserControl
    {
        private readonly TextBox _requestBodyTextBox;
        public ResponseView()
        {
            InitializeComponent();

            _requestBodyTextBox = this.FindControl<TextBox>("RequestBodyTextBox")!;
        }
    }
}
