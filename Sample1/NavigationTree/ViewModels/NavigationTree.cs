using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationTree.ViewModels
{
    public class NavigationTree : BindableBase
    {
        public NavigationTree(Sample1.Model.AppData appData)
        {
            // DI container からAppDataを受け取る
            this._appData = appData;
        }

        private Sample1.Model.AppData _appData = null;
    }
}
