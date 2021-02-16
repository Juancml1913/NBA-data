using System.Collections.ObjectModel;

namespace NBA_data.ViewModels
{
    public class MainViewModel
    {
        public GamesViewModel Games { get; set; }
        public TeamsViewModel Teams { get; set; }
        public PlayersViewModel Players { get; set; }
        public GameDetailsViewModel GameDetails { get; set; }
        public TeamDetailsViewModel TeamDetails { get; set; }
        public PlayerDetailsViewModel PlayerDetails { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
        }

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }
        #endregion

        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "teamIcon",
                PageName = "TeamsPage",
                Title = "Teams",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "playerIcon",
                PageName = "PlayersPage",
                Title = "Players",
            });
        }
    }
}
