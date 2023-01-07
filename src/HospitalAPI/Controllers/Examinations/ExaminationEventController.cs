namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalAPI.Mappers.Examinations;
    using HospitalAPI.Mappers.Examinations.Events;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ExaminationEventController : BaseController<DomainEvent>
    {
        private readonly IExaminationEventService _examinationEventService;

        public ExaminationEventController(IExaminationEventService examinationEventService)
        {
            _examinationEventService = examinationEventService;
        }

        [HttpPost("start")]
        public IActionResult StartExamination(ExaminationStartedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.StartExamination(ExaminationStartedMapper.DtoToEntity(dto))));
        }

        [HttpPost("manage-symptoms")]
        public IActionResult AddSymptom(SymptomsChangedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.ManageSymptoms(SymptomChangedMapper.DtoToEntity(dto))));  
        }

        [HttpPost("add-prescription")]
        public IActionResult AddPrescription(PrescriptionCreatedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.CreatePrescription(PrescriptionCreatedMapper.DtoToEntity(dto))));
        }

        [HttpPost("add-description")]
        public IActionResult AddDescription(DescriptionCreatedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.CreateDescription(DescriptionCreatedMapper.DtoToEntity(dto))));
        }

        [HttpPost("remove-prescription")]
        public IActionResult RemovePrescription(PrescriptionRemovedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.RemovePrescription(PrescriptionRemovedMapper.DtoToEntity(dto))));
        }

        [HttpPost("finish")]
        public IActionResult FinishExamination(ExaminationFinishedDto dto)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_examinationEventService.FinishExamination(ExaminationFinishedMapper.DtoToEntity(dto))));
        }

        [HttpPost("execute-event")]
        public IActionResult ExecuteEvent(ExaminationEventDto dto)
        {
            Anamnesis anamnesis = _examinationEventService.ExecuteEvent(ExaminationEventMapper.DtoToEntity(dto));
            return Ok(AnamnesisMapper.EntityToEntityDto(anamnesis));
        }
    }
}
