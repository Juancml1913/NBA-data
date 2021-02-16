using GalaSoft.MvvmLight.Command;
using NBA_data.Common.Models;
using NBA_data.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NBA_data.ViewModels
{
    public class GameItemViewModel : Game
    {
        #region Commmands
        public ICommand GameDetailsCommand
        {
            get
            {
                return new RelayCommand(GameDetails);
            }
        }

        private async void GameDetails()
        {
            MainViewModel.GetInstance().GameDetails = new GameDetailsViewModel(this);
            await App.Navigator.PushAsync(new GameDetailsPage());
        }
        #endregion
    }
}
