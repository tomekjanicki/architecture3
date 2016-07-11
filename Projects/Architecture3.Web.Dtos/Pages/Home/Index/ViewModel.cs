namespace Architecture3.Web.Dtos.Pages.Home.Index
{
    public class ViewModel : IPageModel
    {
        public ViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
