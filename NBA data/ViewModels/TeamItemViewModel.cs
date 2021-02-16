namespace NBA_data.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NBA_data.Common.Models;
    using NBA_data.Views;
    using System.Windows.Input;

    public class TeamItemViewModel : Team
    {
        #region Commmands
        public ICommand TeamDetailsCommand
        {
            get
            {
                return new RelayCommand(TeamDetails);
            }
        }

        private async void TeamDetails()
        {
            MainViewModel.GetInstance().TeamDetails = new TeamDetailsViewModel(this);
            await App.Navigator.PushAsync(new TeamDetailsPage());
        }
        #endregion
    }
}
