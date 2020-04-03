using Prism.Mvvm;

namespace Sample1.ViewModels
{
    public class MainWindow : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get => this._title;
            set => SetProperty(ref this._title, value);
        }

        public MainWindow() { }
    }
}
