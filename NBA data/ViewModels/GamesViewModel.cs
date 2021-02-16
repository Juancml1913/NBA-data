namespace NBA_data.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NBA_data.Common.Models;
    using NBA_data.Service;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class GamesViewModel : BaseViewModel
    {
        #region Attributes
        private int page=1;

        private string filter;

        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<GameItemViewModel> games;

        private Meta meta;
        #endregion

        #region Properties

        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }

        public List<Game> MyGames{ get; set; }
        public Meta Meta
        {
            get { return this.meta; }
            set { this.SetValue(ref this.meta, value); }
        }

        public ObservableCollection<GameItemViewModel> Games
        {
            get { return this.games; }
            set { this.SetValue(ref this.games, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }



        #endregion

        #region Constructors
        public GamesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadGames();
        }
        #endregion

        #region Singleton
        private static GamesViewModel instance;

        public static GamesViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Methods
        private async void LoadGames()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("No internet", connection.Message, "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var action = Application.Current.Resources["UrlGames"].ToString();
            action = $"{action}?page={this.page}";
            var response = await this.apiService.GetList<Game>(url, action);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var obj = (ApiResponse<Game>)response.Result;            
            this.MyGames = obj.Data;
            this.Meta = obj.Meta;
            this.RefreshList();
            this.IsRefreshing = false;
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myList = this.MyGames.Select(p => new GameItemViewModel
                {
                    Id = p.Id,
                    Date = p.Date,
                    HomeTeam = p.HomeTeam,
                    HomeTeamScore = p.HomeTeamScore,
                    Period = p.Period,
                    PostSeason = p.PostSeason,
                    Season = p.Season,
                    Status = p.Status,
                    Time = p.Time,
                    VisitorTeam = p.VisitorTeam,
                    VisitorTeamScore = p.VisitorTeamScore
                });

                this.Games = new ObservableCollection<GameItemViewModel>(
                    myList.OrderByDescending(p => p.Date));
            }
            else
            {
                var myList = this.MyGames.Select(p => new GameItemViewModel
                {
                    Id = p.Id,
                    Date = p.Date,
                    HomeTeam = p.HomeTeam,
                    HomeTeamScore = p.HomeTeamScore,
                    Period = p.Period,
                    PostSeason = p.PostSeason,
                    Season = p.Season,
                    Status = p.Status,
                    Time = p.Time,
                    VisitorTeam = p.VisitorTeam,
                    VisitorTeamScore = p.VisitorTeamScore
                }).Where(p => p.HomeTeam.FullName.ToLower().Contains(this.Filter.ToLower()) || p.VisitorTeam.FullName.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Games = new ObservableCollection<GameItemViewModel>(
                    myList.OrderByDescending(p => p.Date));
            }
        }

        public void LeftPage() {
            this.page = (int)(Meta.CurrentPage - 1);
            if (page<=0) {
                this.page = (int)Meta.TotalPages;
            }
            this.LoadGames();
        }

        public void RightPage()
        {
            this.page = (int)(Meta.CurrentPage + 1);
            if (page > Meta.TotalPages)
            {
                this.page = 1;
            }
            this.LoadGames();
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadGames);
            }
        }

        public ICommand LeftPageCommand
        {
            get
            {
                return new RelayCommand(LeftPage);
            }
        }

        public ICommand RightPageCommand
        {
            get
            {
                return new RelayCommand(RightPage);
            }
        }        
        #endregion

    }
}
