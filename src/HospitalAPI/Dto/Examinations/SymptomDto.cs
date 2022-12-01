namespace HospitalAPI.Dto.Examinations
{
    public class SymptomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SymptomDto() { }

        public SymptomDto(int id, string name)
        {
            Id = id;
            Name = name;
        }   
    }
}
