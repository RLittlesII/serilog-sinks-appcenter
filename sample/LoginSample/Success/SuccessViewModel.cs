using System;
using ReactiveUI;
using Sextant;

namespace LoginSample
{
    public class SuccessViewModel : ViewModelBase
    {
        public override string Id => "Success";

        public SuccessViewModel(IViewStackService viewStackService = null)
            : base(viewStackService)
        {
        }
    }
}
