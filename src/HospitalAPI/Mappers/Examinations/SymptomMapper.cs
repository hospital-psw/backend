namespace HospitalAPI.Mappers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalLibrary.Core.Model.Examinations;
    using System.Collections.Generic;

    public class SymptomMapper
    {

        public static SymptomDto EntityToEntityDto(Symptom symptom)
        {
            return new SymptomDto(symptom.Id, symptom.Name);
        }

        public static List<SymptomDto> EntityListToEntityDtoList(List<Symptom> sypmtoms)
        {
            List<SymptomDto> symptomList = new List<SymptomDto>();
            sypmtoms.ForEach(x => symptomList.Add(EntityToEntityDto(x)));
            return symptomList;
        }
    }
}
