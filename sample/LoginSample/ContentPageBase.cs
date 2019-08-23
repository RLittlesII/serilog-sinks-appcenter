using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace LoginSample
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>
    where TViewModel : ViewModelBase
    {
        public ContentPageBase()
        {
            this.WhenActivated(disposable =>
            {
                ViewModel
                    .ShowAlert
                    .RegisterHandler(interaction =>
                    {
                        AlertViewModel input = interaction.Input;
                        DisplayAlert(input.Title, input.Description, input.ButtonText);
                        interaction.SetOutput(Unit.Default);
                    })
                    .DisposeWith(disposable);
            });
        }
    }
}
