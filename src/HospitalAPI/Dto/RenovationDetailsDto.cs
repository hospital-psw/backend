namespace HospitalAPI.Dto
{
    public class RenovationDetailsDto
    {
        public string NewRoomName { get; set; }
        public string NewRoomPurpose { get; set; }
        public int NewCapacity { get; set; }

        public RenovationDetailsDto(string newRoomName, string newRoomPurpose, int newCapacity)
        {
            NewRoomName = newRoomName;
            NewRoomPurpose = newRoomPurpose;
            NewCapacity = newCapacity;
        }

        public RenovationDetailsDto() { }
    }
}
