namespace NBA_data.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NBA_data.Common.Models;
    using NBA_data.Views;
    using System.Windows.Input;

    public class PlayerItemViewModel : Player
    {
        #region Commmands
        public ICommand PlayerDetailsCommand
        {
            get
            {
                return new RelayCommand(PlayerDetails);
            }
        }

        private async void PlayerDetails()
        {           
            MainViewModel.GetInstance().PlayerDetails = new PlayerDetailsViewModel(this);
            await App.Navigator.PushAsync(new PlayerDetailsPage());
        }
        #endregion
    }
}
