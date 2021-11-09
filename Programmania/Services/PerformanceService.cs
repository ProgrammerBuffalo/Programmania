using Programmania.DAL;
using Programmania.Models;
using Programmania.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Programmania.Services
{
    public class PerformanceService : IPerformanceService
    {
        private ProgrammaniaDBContext dBContext;

        public PerformanceService(ProgrammaniaDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IEnumerable<Reward> GetRewards(User user, DateTime from, DateTime to)
        {
            var rewards = dBContext.Rewards
                .OrderByDescending(r => r.CreatedAt)
                .Where(r => r.User.Id == user.Id && from <= r.CreatedAt && to >= r.CreatedAt);
            return rewards;
        }

        public IEnumerable<Reward> GetRewards(User user, int count, int offset)
        {
            var rewards = dBContext.Rewards
                .OrderByDescending(r => r.CreatedAt)
                .Where(r => r.User.Id == user.Id).Skip(offset).Take(count);
            return rewards;
        }
    }
}
