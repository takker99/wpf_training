using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace Takker.Triggers
{
    /// <summary>
    /// DataContextをDisposeするTriggerActionを表します。
    /// </summary>
    public class DataContextDispose : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Actionを実行します。
        /// </summary>
        /// <param name="parameter">パラメータを表すobject。</param>
        protected override void Invoke(object parameter)
        {
            var disposable = this.AssociatedObject?.DataContext as IDisposable;
            disposable?.Dispose();
        }
    }
}

