using System;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Splat;

namespace LoginSample
{
    public abstract class ViewModelBase : ReactiveObject, ISupportsActivation, IPageViewModel
    {
        public ViewModelActivator Activator => _viewModelActivator;
        public Interaction<AlertViewModel, Unit> ShowAlert;

        protected readonly ViewModelActivator _viewModelActivator = new ViewModelActivator();

        public abstract string Id
        {
            get;
        }

        protected readonly IViewStackService ViewStackService;

        public ViewModelBase(IViewStackService viewStackService)
        {
            ViewStackService = viewStackService ?? Locator.Current.GetService<IViewStackService>();
            ShowAlert = new Interaction<AlertViewModel, Unit>();
        }
    }
}
