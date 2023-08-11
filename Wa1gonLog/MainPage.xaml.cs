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
            Repo.CreateContext("Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio");
        }

    }
}