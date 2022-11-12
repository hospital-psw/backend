namespace IntegrationLibrary.News
{
    using IntegrationLibrary.Core;

    public class News : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public NewsStatus Status { get; set; }

    }
}
