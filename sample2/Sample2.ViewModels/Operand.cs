using System;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample2.ViewModels
{
    public class Operand : BindableBase
    {
        [Required, Range(-10000,10000)]
        public ReactiveProperty<string> Text { get; }

        public Operand()
        {
            // SetValidateAttribute:
            //
            // エラー検出を可能にする。検出条件式は
            // System.ComponentModel.DataAnnotations属性で指定する
            this.Text = new ReactiveProperty<string>("2").
                SetValidateAttribute(() => this.Text);

        }
    }
}
