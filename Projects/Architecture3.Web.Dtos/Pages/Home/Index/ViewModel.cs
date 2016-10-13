namespace Architecture3.Web.Dtos.Pages.Home.Index
{
    using Architecture3.Common.Web;

    public class ViewModel : IPageModel
    {
        public ViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
