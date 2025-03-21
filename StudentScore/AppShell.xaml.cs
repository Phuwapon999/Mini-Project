using StudentScore.Page;

namespace StudentScore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("home", typeof(HomePage));
	}
}
