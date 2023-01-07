namespace HospitalAPI.Dto.Examinations.Events
{
    public enum ExaminationEventType
    {
        EXAMINATION_STARTED = 0,
        EXAMINATION_FINISHED = 1,
        SYMPTOMS_CHANGED = 2,
        DESCRIPTION_CREATED = 3,
        PRESCRIPTION_CREATED = 4, 
        PRESCRIPTION_REMOVED = 5, 
        EXAMINATION_LEFT = 6,
        EXAMINATION_PREVIOUS_1 = 7,
        EXAMINATION_PREVIOUS_2 = 8,
        EXAMINATION_PREVIOUS_3 = 9,
    }
}
