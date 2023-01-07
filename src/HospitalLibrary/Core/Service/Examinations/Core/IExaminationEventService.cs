namespace HospitalLibrary.Core.Service.Examinations.Core
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Events;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExaminationEventService : IBaseService<DomainEvent>
    {
        Anamnesis StartExamination(ExaminationStarted domainEvent);

        Anamnesis ManageSymptoms(SymptomsChanged symptomsChanged);

        Anamnesis CreatePrescription(PrescriptionCreated prescriptionCreated);

        Anamnesis CreateDescription(DescriptionCreated descriptionCreated);

        Anamnesis RemovePrescription(PrescriptionRemoved prescriptionRemoved);

        Anamnesis FinishExamination(ExaminationFinished examinationFinished);

        Anamnesis ExecuteEvent(ExaminationEvent examinationEvent);  


    }
}
