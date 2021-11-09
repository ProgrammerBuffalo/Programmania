using Programmania.ViewModels;
using System.Collections.Generic;

namespace Programmania.Services.Interfaces
{
    public interface IStaticService
    {
        public UserCourseVM[] GetCourses(Models.User user, IFileService fileService);

        public List<OfferedChallengeVM> GetOfferedChallenges(Models.User user, IFileService fileService);

        public List<PossibleChallengeVM> GetPossibleChallenges(IFileService fileService, int count);
    }
}
