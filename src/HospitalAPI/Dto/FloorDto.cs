namespace HospitalAPI.Dto
{

    public class FloorDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Purpose { get; set; }
        public BuildingDto Building { get; set; }

    }
}
