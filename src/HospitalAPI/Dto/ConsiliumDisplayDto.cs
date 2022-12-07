namespace HospitalAPI.Dto
{
    using System;

    public class ConsiliumDisplayDto
    {
        public DateTime DateTime { get; set; }
        public string Topic { get; set; }
        public int Duration { get; set; }

        public ConsiliumDisplayDto(DateTime dateTime, string topic, int duration)
        {
            DateTime = dateTime;
            Topic = topic;
            Duration = duration;
        }

        public ConsiliumDisplayDto()
        {
        }
    }

}
