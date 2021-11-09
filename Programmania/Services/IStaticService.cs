using Programmania.Models;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Services
{
    public interface IStaticService
    {
        public UserCourseVM[] GetCourses(User user, IFileService fileService);

        public List<OfferedChallengeVM> GetOfferedChallenges(User user, IFileService fileService);

        public List<PossibleChallengeVM> GetPossibleChallenges(IFileService fileService, int count);
    }
}
