using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using Serilog;
using Sextant;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Locator.CurrentMutable.Register(() => new AuthenticationService(), typeof(IAuthenticationService));
            Locator.CurrentMutable.Register(() => new ServerService(), typeof(IServerService));

            SextantHelper.RegisterView<MainPage, MainViewModel>();
            SextantHelper.RegisterView<SuccessPage, SuccessViewModel>();

            AppCenter.Start("ios=ff215962-4af4-48ff-948b-a5da6643caa7;",
                typeof(Crashes));

            InitializeLogger();

            MainPage = Initialize();
        }

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.AppCenterCrashes()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            Locator.CurrentMutable.UseSerilogFullLogger(Log.Logger);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static NavigationPage Initialize()
        {
            var bgScheduler = RxApp.TaskpoolScheduler;
            var mScheduler = RxApp.MainThreadScheduler;
            var vLocator = Locator.Current.GetService<IViewLocator>();

            var navigationView = new NavigationView(mScheduler, bgScheduler, vLocator);
            var viewStackService = new ViewStackService(navigationView);

            Locator.CurrentMutable.Register<IViewStackService>(() => viewStackService);
            navigationView.PushPage(new MainViewModel(), null, true, false).Subscribe();

            return navigationView;
        }
    }
}
