namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;

    public class RenovationRequestDto
    {
        public RenovationType RenovationType { get; set; }
        public List<int> RoomsId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public List<RenovationDetailsDto> RenovationDetails { get; set; }

        public RenovationRequestDto(RenovationType renovationType, List<int> roomsId, DateTime startTime, int duration, List<RenovationDetailsDto> renovationDetails)
        {
            RenovationType = renovationType;
            RoomsId = roomsId;
            StartTime = startTime;
            Duration = duration;
            RenovationDetails = renovationDetails;
        }

        public RenovationRequestDto() { }
    }
}
