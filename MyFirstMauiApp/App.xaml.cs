namespace MyFirstMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
             Routing.RegisterRoute("blogdetails", typeof(BlogDetailsPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell()); 
        }
    }
}