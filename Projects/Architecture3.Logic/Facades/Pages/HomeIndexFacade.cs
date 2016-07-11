namespace Architecture3.Logic.Facades.Pages
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;
    using Architecture3.Web.Dtos.Pages.Home.Index;

    public class HomeIndexFacade
    {
        private readonly IMediator _mediator;

        public HomeIndexFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ViewModel GetViewModel()
        {
            var data = _mediator.Send<NonEmptyString>();
            return new ViewModel(data);
        }
    }
}
