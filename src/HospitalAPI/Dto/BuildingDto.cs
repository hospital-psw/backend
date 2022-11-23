namespace HospitalAPI.Dto
{

    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public BuildingDto() { }

        public BuildingDto(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
