namespace HospitalAPI.Dto
{
    public class RenovationDetailsDto
    {
        public string NewRoomName { get; set; }
        public string NewRoomPurpose { get; set; }

        public RenovationDetailsDto(string newRoomName, string newRoomPurpose)
        {
            NewRoomName = newRoomName;
            NewRoomPurpose = newRoomPurpose;
        }

        public RenovationDetailsDto() { }
    }
}
