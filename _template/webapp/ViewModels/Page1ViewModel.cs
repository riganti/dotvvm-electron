using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace WebApp.ViewModels
{
    public class Page1ViewModel : DotvvmViewModelBase
    {
        public string Title { get; set; }

        public List<string> Items { get; set; } = new List<string>{ "First", "Second", "Third" };

        public string Item { get; set; }
        public Page1ViewModel()
        {
            Title = "Hello from DotVVM: Page1ViewModel!";
        }

        public void AddItem()
        {
            Items.Add(Item);
        }
    }
}
