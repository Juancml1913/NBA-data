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

namespace NBA_data.ViewModels
{
    public class TeamsViewModel : BaseViewModel
    {
        #region Attributes
        private string filter;

        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<TeamItemViewModel> teams;
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

        public List<Team> MyTeams { get; set; }

        public ObservableCollection<TeamItemViewModel> Teams
        {
            get { return this.teams; }
            set { this.SetValue(ref this.teams, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public TeamsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadTeams();
        }
        #endregion

        #region Singleton
        private static TeamsViewModel instance;

        public static TeamsViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Methods
        private async void LoadTeams()
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
            var action = Application.Current.Resources["UrlTeams"].ToString();
            var response = await this.apiService.GetList<Team>(url, action);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var obj = (ApiResponse<Team>)response.Result;
            this.MyTeams = obj.Data;
            this.RefreshList();
            this.IsRefreshing = false;
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myList = this.MyTeams.Select(p => new TeamItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    FullName = p.FullName,
                    Abbreviation = p.Abbreviation,
                    City = p.City,
                    Conference = p.Conference,
                    Division = p.Division
                });

                this.Teams = new ObservableCollection<TeamItemViewModel>(
                    myList.OrderBy(p => p.Name));
            }
            else
            {
                var myList = this.MyTeams.Select(p => new TeamItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    FullName = p.FullName,
                    Abbreviation = p.Abbreviation,
                    City = p.City,
                    Conference = p.Conference,
                    Division = p.Division
                }).Where(p => p.FullName.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Teams = new ObservableCollection<TeamItemViewModel>(
                    myList.OrderBy(p => p.Name));
            }
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
                return new RelayCommand(LoadTeams);
            }
        }
        #endregion
    }
}
