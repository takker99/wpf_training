using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample6.Entities
{
    public class ConsoleBuffer : Utilities.BindableModelBase
    {
        public ReactivePropertySlim<string> ConsoleText { get; set; }

        public void Clear() 
            => this.ConsoleText.Value = System.String.Empty;

        public void AppendLineToBuffer(string text)
        {
            var buf = new StringBuilder(this.ConsoleText.Value);
            buf.AppendLine(text);

            this.ConsoleText.Value = buf.ToString();
        }

        public ConsoleBuffer() 
            => this.ConsoleText = new ReactivePropertySlim<string>(System.String.Empty)
                .AddTo(this._disposable);
    }
}
