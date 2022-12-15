﻿namespace HospitalAPI.EmailServices
{
    using HospitalAPI.Configuration;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Util;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private ProjectConfiguration _configuration;
        private ILogger<EmailService> _logger;

        public EmailService(ProjectConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Send(Appointment appointment)
        {
            try
            {
                string body = "<p>Dear, " + appointment.Patient.FirstName + " " + appointment.Patient.LastName +
                              "</p><p> We inform you that your appointment set at " + DateHelper.DateToString(appointment.Date, "dd/MM/yyyy") +
                              "has been canceled by doctor " + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName + " </p>" +
                              "<p>Sorry if we upset your daily plans, you can always contact us, or schedule a new appointment</p>" +
                              "<p>Best Regards, PSW Hospital </p>";
                MailMessage mailMessage = CreateEmailMessage(body, "Appointment cancelation");
                mailMessage.To.Add(appointment.Patient.Email);

                await SendEmailMessage(mailMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in EmailService in Send {e.Message} in {e.StackTrace}");
            }
        }

        private MailMessage CreateEmailMessage(string body, string subject = "New Message")
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration.EmailSettings.FromEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                return message;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in HospitalAPI EmailService in CreateEmailMessage {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public async Task SendActivationEmail(ApplicationUser identityUser, string url)
        {
            try
            {
                string body = $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>";
                MailMessage mailMessage = CreateActivationEmail(body, "Confirm your email", identityUser.Email);

                await SendEmailMessage(mailMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in EmailService in Send {e.Message} in {e.StackTrace}");
            }
        }

        private MailMessage CreateActivationEmail(string body, string subject, string patientEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration.EmailSettings.FromEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.To.Add(patientEmail);

                return message;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in HospitalAPI EmailService in CreateActivationEmail {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private async Task SendEmailMessage(MailMessage message)
        {
            using SmtpClient client = new(_configuration.EmailSettings.ServerAddress, _configuration.EmailSettings.Port)
            {
                Credentials = new NetworkCredential(_configuration.EmailSettings.Username, _configuration.EmailSettings.Password),
                EnableSsl = _configuration.EmailSettings.EnableSsl,
            };

            await client.SendMailAsync(message);
        }

        //ForgotPasswordMail
        public async Task SendPasswordResetEmail(string email, string callback) 
        {
            try
            {
                string body = "<p>Please click on this link to reset your password:</p>" + callback;
                var message = CreateEmailMessage(body, "Reset Password Token");
                message.To.Add(email);
                
                await SendEmailMessage(message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in HospitalAPI EmailService in CreateActivationEmail {e.Message} in {e.StackTrace}");
            }
        
        }
    }
}
