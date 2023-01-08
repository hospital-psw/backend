namespace HospitalAPI.Dto.Examinations.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DescriptionCreatedDto : ExaminationEventDto
    {
        [Required]
        public string Description { get; set; }

        public DescriptionCreatedDto()
        {

        }

        public DescriptionCreatedDto(int aggregateId, ExaminationEventType eventType, DateTime timeStamp, string description)
            : base(aggregateId, eventType, timeStamp)
        {
            Description = description;
        }
    }
}
