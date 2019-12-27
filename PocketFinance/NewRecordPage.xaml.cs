using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewRecordPage : ContentPage
    {

        public ContentPage parentPage;
        NewRecordPageViewModel viewModel;

        public NewRecordPage(ContentPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            viewModel = new NewRecordPageViewModel(this, book);
            BindingContext = viewModel;
        }
    }
}
