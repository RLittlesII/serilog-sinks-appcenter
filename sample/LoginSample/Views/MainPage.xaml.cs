using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace LoginSample
{
    public partial class MainPage : ContentPageBase<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, 
                    vm => vm.LastLoginAttempt, 
                    v => v.Subtitle.Text, 
                    x =>
                    {
                        if (x == default(DateTime))
                            return "No attempts";

                        return x.ToString("R");
                    });

                Server
                    .Events()
                    .Focused
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.LoadServers)
                    .DisposeWith(disposables);
            });
        }
    }
}