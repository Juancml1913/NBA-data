namespace NBA_data.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using NBA_data.Common.Models;
    using NBA_data.Service;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class PlayersViewModel : BaseViewModel
    {
        #region Attributes
        private int page;

        private string filter;

        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<PlayerItemViewModel> players;

        private Meta meta;
        #endregion

        #region Properties
        public Meta Meta
        {
            get { return this.meta; }
            set { this.SetValue(ref this.meta, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }

        public List<Player> MyPlayers { get; set; }

        public ObservableCollection<PlayerItemViewModel> Players
        {
            get { return this.players; }
            set { this.SetValue(ref this.players, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public PlayersViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.IsRefreshing = true;
            this.LoadPlayers();
        }
        #endregion

        #region Singleton
        private static PlayersViewModel instance;

        public static PlayersViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Methods
        private async void LoadPlayers()
        {
            

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("No internet", connection.Message, "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var action = Application.Current.Resources["UrlPlayers"].ToString();
            action = $"{action}?page={this.page}";
            var response = await this.apiService.GetList<Player>(url, action);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            this.IsRefreshing = false;
            var obj = (ApiResponse<Player>)response.Result;
            this.MyPlayers = obj.Data;
            this.Meta = obj.Meta;
            this.RefreshList();
            
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myList = this.MyPlayers.Select(p => new PlayerItemViewModel
                {
                    Id = p.Id,
                    FirstName =p.FirstName,
                    HeightFeet=p.HeightFeet,
                    HeightInches=p.HeightInches,
                    LastName=p.LastName,
                    Position=p.Position,
                    WeightPounds=p.WeightPounds,
                    Team=p.Team
                });

                this.Players = new ObservableCollection<PlayerItemViewModel>(
                    myList.OrderBy(p => p.FirstName));
            }
            else
            {
                var myList = this.MyPlayers.Select(p => new PlayerItemViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    HeightFeet = p.HeightFeet,
                    HeightInches = p.HeightInches,
                    LastName = p.LastName,
                    Position = p.Position,
                    WeightPounds = p.WeightPounds,
                    Team = p.Team
                }).Where(p => p.FirstName.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Players = new ObservableCollection<PlayerItemViewModel>(
                    myList.OrderBy(p => p.FirstName));
            }
        }
        public void LeftPage()
        {
            this.page = (int)(Meta.CurrentPage - 1);
            if (page <= 0)
            {
                this.page = (int)Meta.TotalPages;
            }
            this.LoadPlayers();
        }

        public void RightPage()
        {
            this.page = (int)(Meta.CurrentPage + 1);
            if (page > Meta.TotalPages)
            {
                this.page = 1;
            }
            this.LoadPlayers();
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
                return new RelayCommand(LoadPlayers);
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
