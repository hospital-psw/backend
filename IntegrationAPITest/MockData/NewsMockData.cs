namespace IntegrationAPITest.MockData
{
    using IntegrationLibrary.News;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit.Sdk;

    public static class NewsMockData
    {
        public static News PendingNews
        {
            get
            {
                return new News()
                {
                    Title = "Akcija prikupljanja krvi na stadionu Karadjordje!",
                    Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Status = NewsStatus.PENDING,
                    Image = "base64"
                };
            }
        }

        public static News ArchivedNews
        {
            get
            {
                return new News()
                {
                    Title = "Akcija prikupljanja krvi na stadionu Rajko Mitic!",
                    Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Status = NewsStatus.ARCHIVED,
                    Image = "base64"
                };
            }
        }

        public static News PublishedNews
        {
            get
            {
                return new News()
                {
                    Title = "Akcija prikupljanja krvi na stadionu JNA!",
                    Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Status = NewsStatus.PUBLISHED,
                    Image = "base64"
                };
            }
        }

    }
}
