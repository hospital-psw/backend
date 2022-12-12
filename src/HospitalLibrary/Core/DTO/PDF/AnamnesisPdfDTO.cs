namespace HospitalLibrary.Core.DTO.PDF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisPdfDTO
    {
        public int AnamnesisId { get; set; }
        public Boolean AreSymptomsSelected { get; set; }
        public Boolean AreRecepiesSelected { get; set; }
        public Boolean IsDescriptionSelected { get; set; }

        public AnamnesisPdfDTO() { }

        public AnamnesisPdfDTO(int anamnesisId, bool areSymptomsSelected, bool areRecepiesSelected, bool isDescriptionSelected)
        {
            AnamnesisId = anamnesisId;
            AreSymptomsSelected = areSymptomsSelected;
            AreRecepiesSelected = areRecepiesSelected;
            IsDescriptionSelected = isDescriptionSelected;
        }
    }
}
