namespace HospitalAPI.Dto
{
    using System;

    public class SearchCriteriaDto
    {
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public string RoomNumber { get; set; }
        public string RoomPurpose { get; set; }
        public DateTime WorkingHoursStart { get; set; }
        public DateTime WorkingHoursEnd { get; set; }

        public SearchCriteriaDto(int buildingId, int floorNumber, string roomNumber, string roomPurpose, DateTime workingHoursStart, DateTime workingHoursEnd)
        {
            this.BuildingId = buildingId;
            this.FloorNumber = floorNumber;
            this.RoomNumber = roomNumber;
            this.RoomPurpose = roomPurpose;
            this.WorkingHoursStart = workingHoursStart;
            this.WorkingHoursEnd = workingHoursEnd;
        }

    }
}
