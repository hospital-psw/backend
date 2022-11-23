namespace IntegrationAPI.DTO.News
{
    using IntegrationLibrary.News;
    using System;

    public class UserNewsDTO
    {
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
