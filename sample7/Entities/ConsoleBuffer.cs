using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Data;

namespace Sample7.Entities
{
    public class ConsoleBuffer : Takker.Utilities.BindableModelBase
    {
        public ReactivePropertySlim<DataTable> Data { get; set; }

        public void Clear()
            => this.Data.Value.Reset();

        public ConsoleBuffer() 
            => this.Data = new ReactivePropertySlim<DataTable>(new DataTable("NewTable"))
                .AddTo(this._disposable);
    }
}
