using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Sextant;
using Splat;

namespace LoginSample
{
    public class MainViewModel : ViewModelBase
    {
        public string Username
        {
            set => this.RaiseAndSetIfChanged(ref _username, value);
            get => _username;
        }

        public string Password
        {
            set => this.RaiseAndSetIfChanged(ref _password, value);
            get => _password;
        }

        public DateTime LastLoginAttempt
        {
            set => this.RaiseAndSetIfChanged(ref _lastLoginAttempt, value);
            get => _lastLoginAttempt;
        }

        public string ServerAddress
        {
            set => this.RaiseAndSetIfChanged(ref _serverAddress, value);
            get => _serverAddress;
        }

        public bool IsRunning
        {
            set => this.RaiseAndSetIfChanged(ref _isRunning, value);
            get => _isRunning;
        }

        public List<string> ServerList
        {
            set => this.RaiseAndSetIfChanged(ref _serverList, value);
            get => _serverList;
        }

        public override string Id => "Login";

        public ReactiveCommand<Unit, bool> Login { protected set; get; }
        public ReactiveCommand<Unit, List<string>> LoadServers { protected set; get; }

        private string _username;
        private string _password;
        private string _serverAddress;
        private DateTime _lastLoginAttempt;
        private bool _isRunning;
        private List<string> _serverList;

        private IAuthenticationService _authenticationService;
        private IServerService _serverService;

        public MainViewModel(
                IAuthenticationService authenticationService = null,
                IServerService serverService = null,
                IViewStackService viewStackService = null)
            : base(viewStackService)
        {
            _authenticationService = authenticationService ?? Locator.Current.GetService<IAuthenticationService>();
            _serverService = serverService ?? Locator.Current.GetService<IServerService>();

            var canLogin = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                x => x.ServerAddress,
                (user, pass, server) => !string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass) && !string.IsNullOrWhiteSpace(server));

            Login = ReactiveCommand.CreateFromTask(_ => ExecuteLogin(), canLogin);

            Login
                .SelectMany(_ => ViewStackService.PushPage(new SuccessViewModel()))
                .Subscribe();

            var canLoadServers = this.WhenAnyValue(x => x.IsRunning).Select(x => !x);

            LoadServers = ReactiveCommand.CreateFromTask(_ => _serverService.GetServers(), canLoadServers);

            LoadServers
                .BindTo(this, x => x.ServerList);

            Observable
                .CombineLatest(
                    Login.IsExecuting,
                    LoadServers.IsExecuting,
                    (loginIsRunning, loadServersIsRunning) => loginIsRunning || loadServersIsRunning)
                .BindTo(this, x => x.IsRunning);

            Observable
                .Merge(
                    Login.ThrownExceptions,
                    LoadServers.ThrownExceptions)
                .SelectMany(ex =>
                {
                    this.Log().Warn(ex, "New Test with json date: {@Date} with a list {@List}", DateTime.Now, new List<string> { "a1", "a2", "a3"});
                    return ShowAlert.Handle(new AlertViewModel("Error", ex.Message, "ok"));
                })
                .Subscribe();
        }

        private Task<bool> ExecuteLogin()
        {
            LastLoginAttempt = DateTime.UtcNow;
            return _authenticationService.Authorize(Username, Password);
        }
    }
}
