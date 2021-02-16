namespace NBA_data.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NBA_data.Views;
    using System.Windows.Input;
    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(GotoAsync);
            }
        }

        private async void GotoAsync()
        {
            if (this.PageName == "TeamsPage")
            {
                MainViewModel.GetInstance().Teams = new TeamsViewModel();
                await App.Navigator.PushAsync(new TeamsPage());
            } else if (this.PageName == "PlayersPage") {
                MainViewModel.GetInstance().Players = new PlayersViewModel();
                await App.Navigator.PushAsync(new PlayersPage());
            }
        }
        #endregion
    }
}
