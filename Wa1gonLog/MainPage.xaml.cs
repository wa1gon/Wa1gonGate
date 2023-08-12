using HamDevLib;
using System.Diagnostics;

namespace Wa1gonLog
{
    public partial class MainPage : ContentPage
    {
        private IQSORepo Repo { get; set; }
        public MainPage(IQSORepo repo)
        {
            InitializeComponent();
            Repo = repo;
            //todo: replace with setting manager to get connection string
            Repo.CreateContext("Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //collectionView.ItemsSourceProperty = Repo.GetAllQsos();

        }
        public async void OnAddQsoClicked(object sender, EventArgs e)
        {
            Debug.Write("--> add button clicked");
        }
        public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.Write("--> selection change");
        }

    }
}