namespace HospitalAPI.Dto
{

    public class FloorDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Purpose { get; set; }
        public BuildingDto Building { get; set; }

        public FloorDto() { }

        public FloorDto(int id, int number, string purpose, BuildingDto building)
        {
            Id = id;
            Number = number;
            Purpose = purpose;
            Building = building;
        }
    }
}
