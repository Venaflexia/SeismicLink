namespace SeismicLink;

public partial class App : Application
{
	public static string SelectedCountryNameSh = "SelectedCountryNameSh";
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
