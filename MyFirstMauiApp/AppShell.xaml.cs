namespace MyFirstMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes programmatically
            Routing.RegisterRoute("MainPage", typeof(MainPage));           
            //Routing.RegisterRoute("blogdetails", typeof(BlogDetailsPage));           
        }
    }
}
