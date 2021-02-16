namespace NBA_data.ViewModels
{
    using NBA_data.Common.Models;
    using NBA_data.Service;
    using Xamarin.Forms;

    public class PlayerDetailsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private string imageSource;
        private bool isRunning;
        public Player Player { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public string ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }

        public PlayerDetailsViewModel(Player Player)
        {
            this.apiService = new ApiService();
            this.Player = Player;
            this.LoadImage();
        }

        private async void LoadImage()
        {
            this.IsRunning = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("No internet", connection.Message, "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI2"].ToString();
            var action = Application.Current.Resources["UrlImage"].ToString();
            action = $"{action}?pageNumber=1&pageSize=1&autoCorrect=false&q={this.Player.FirstName}{' '}{this.Player.LastName}";//{' '}{this.Player.Team.FullName}";
            var response = await this.apiService.GetImage(url, action);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var obj = (Common.Models.Image)response.Result;
            this.ImageSource = obj.Value[0].Url;
            this.IsRunning = false;
        }
    }
}
