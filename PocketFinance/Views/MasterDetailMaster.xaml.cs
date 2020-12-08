using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFinance.Views
{
    /*[XamlCompilation(XamlCompilationOptions.Compile)]*/
    public partial class MasterDetailMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailMenuItem> MenuItems { get; set; }

            public MasterDetailMasterViewModel()
            {
                //MenuItems = new ObservableCollection<MasterDetailMenuItem>(new[]
                //{
                //    new MasterDetailMenuItem { Id = 0, Title = "Page 1" },
                //    new MasterDetailMenuItem { Id = 1, Title = "Page 2" },
                //    new MasterDetailMenuItem { Id = 2, Title = "Page 3" },
                //    new MasterDetailMenuItem { Id = 3, Title = "Page 4" },
                //    new MasterDetailMenuItem { Id = 4, Title = "Page 5" },
                //});
                MenuItems = new ObservableCollection<MasterDetailMenuItem>();
                // add all the different pages available to the list, but also
                // change the Type of the pages to their respective types
                MenuItems.Add(new MasterDetailMenuItem
                {
                    Id = 0,
                    Title = "Dashboard",
                    TargetType = typeof(MainPage)
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
