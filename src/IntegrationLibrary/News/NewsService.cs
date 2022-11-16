namespace IntegrationLibrary.News
{
    using IdentityModel;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.News.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class NewsService : INewsService
    {
        private readonly ILogger<News> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public NewsService(ILogger<News> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public News Create(News entity)
        {
            try
            {
                _unitOfWork.NewsRepository.Add(entity);
                _unitOfWork.Save();

                return entity;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Create {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _unitOfWork.NewsRepository.Get(id);
                entity.Deleted = true;
                _unitOfWork.NewsRepository.Update(entity);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Delete {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public News Get(int id)
        {
            try
            {
                return _unitOfWork.NewsRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<News> GetPublished()
        {
            try
            {
                return _unitOfWork.NewsRepository.GetAll().ToList().Where(x => x.Status == NewsStatus.PUBLISHED);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetPublished {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<News> GetArchived()
        {
            try
            {
                return _unitOfWork.NewsRepository.GetAll().ToList().Where(x => x.Status == NewsStatus.ARCHIVED);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetArchived {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<News> GetPending()
        {
            try
            {
                return _unitOfWork.NewsRepository.GetAll().ToList().Where(x => x.Status == NewsStatus.PENDING);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetPending {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<News> GetAll()
        {
            try
            {
                return _unitOfWork.NewsRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public News Update(News entity)
        {
            try
            {
                _unitOfWork.NewsRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Update {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool Publish(int id)
        {
            try
            {
                var news = Get(id);
                if (news == null || news.Status == NewsStatus.PUBLISHED)
                {
                    return false;
                }
                news.Status = NewsStatus.PUBLISHED;
                _unitOfWork.NewsRepository.Update(news);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Update {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool Archive(int id)
        {
            try
            {
                var news = Get(id);
                if (news == null || news.Status == NewsStatus.ARCHIVED)
                {
                    return false;
                }
                news.Status = NewsStatus.ARCHIVED;
                _unitOfWork.NewsRepository.Update(news);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in Update {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public string GetImageExtension(News entity)
        {
            try
            {
                return entity.Image.Split(new String[] { ";" }, StringSplitOptions.None)[0];
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetImageExtension {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public string GetImageData(News entity)
        {
            try
            {
                return entity.Image.Split(new String[] { ";" }, StringSplitOptions.None)[1];
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in GetImageData {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void SaveImageToDisk(News entity)
        {
            try
            {
                string extension = GetImageExtension(entity);
                string imageData = GetImageData(entity);
                
                string path = Directory.GetParent(Environment.CurrentDirectory).FullName;
                path = Path.Combine(new string[] { path, "MQImages", $"RecievedImage{entity.Id}.{extension}"});

                byte[] imageBytes = Convert.FromBase64String(imageData);

                using var writer = new BinaryWriter(File.OpenWrite(path));
                writer.Write(imageBytes);

                writer.Flush();
                writer.Dispose();
                writer.Close();

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NewsService in SaveImageToDisk {e.Message} in {e.StackTrace}");
            }
        }
    }
}
