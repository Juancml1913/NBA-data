using NBA_data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_data.ViewModels
{
    public class GameDetailsViewModel : BaseViewModel
    {
        public Game Game { get; set; }



        public GameDetailsViewModel(Game Game)
        {
            this.Game = Game;
        }
    }
}
