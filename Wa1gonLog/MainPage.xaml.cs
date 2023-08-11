using HamDevLib;

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
            //CollectionView.ItemsSourceProperty = Repo.GetAllQsos();

        }

    }
}