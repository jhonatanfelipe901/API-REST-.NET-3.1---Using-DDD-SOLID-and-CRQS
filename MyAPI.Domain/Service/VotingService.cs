using MyAPI.Domain.Entities;
using MyAPI.Domain.Repository;
using MyAPI.Domain.Service.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyAPI.Domain.Service
{
    public class VotingService : ServiceBase<Voting>, IVotingService
    {
        private readonly IVotingRepository _votingRepository;


        public VotingService(IVotingRepository votingRepository) : base(votingRepository)
        {
            _votingRepository = votingRepository;
        }

        public IEnumerable<string> Create(Voting voting, string image64, string imageExtension)
        {
            string imageFolder = GetRootImageFolder().Replace("\\", "/") + "/";

            var imageFolderWithName = imageFolder + voting.Description + "." + imageExtension;

            var errors = AddImage(image64, imageFolder, imageFolderWithName);

            if (errors == null || errors.Count() == 0)
            {
                voting.ImagePath = imageFolderWithName;
                _votingRepository.Create(voting);
            }

            return errors;
        }

        public IEnumerable<string> AddImage(string image64, string imageFolder, string imageFolderWithName)
        {
            var errors = new List<string>();

            if(!AddImageBase64(imageFolder, imageFolderWithName, image64))
                errors.Add("sdsdsdsd"); 

            return errors;
        }

        private bool AddImageBase64(string imageFolder, string imagePath, string image64)
        {
            try
            {
                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                File.WriteAllBytes(imagePath, Convert.FromBase64String(image64));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private string GetRootImageFolder()
        {
            var path = "C:\\inetpub\\wwwroot\\Homologacao\\MyApi";

            return Path.Combine(path);
            //return Path.Combine(Configuration.GetSection("DestinationImage:FrontAppPath").Value);
        }
    }
}
