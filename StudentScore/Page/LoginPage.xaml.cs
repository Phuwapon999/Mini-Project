using StudentScore.ViewModel;

namespace StudentScore.Page;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
	private void OnLoginClicked(object sender, EventArgs e)
    {
        // Call the command that is bound in the ViewModel
    	var viewModel = (LoginViewModel)BindingContext;
    	viewModel.LoginCommand.Execute(null);
    }
}
