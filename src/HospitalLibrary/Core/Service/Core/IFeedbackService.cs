﻿namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFeedbackService
    {
        Feedback Create(NewFeedbackDTO dto);
        IEnumerable<Feedback> GetAll();
        IEnumerable<Feedback> GetAllPublicFeedback();
        IEnumerable<Feedback> GetAllPrivateFeedback();
        IEnumerable<Feedback> GetAllAnonymousFeedback();
        IEnumerable<Feedback> GetAllIdentifiedFeedback();
        bool MakeFeedbackPublic(int id);
    }
}
