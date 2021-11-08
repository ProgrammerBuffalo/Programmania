using Programmania.DAL;
using Programmania.Models;
using Programmania.Services.Interfaces;
using System;
using System.Collections.Generic;

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
            LinkedList<Reward> rewards = new LinkedList<Reward>();
            //dBContext.Rewards.Select();
            return null;
        }

        public IEnumerable<Reward> GetRewards(User user, int count, int offset)
        {
            LinkedList<Reward> rewards = new LinkedList<Reward>();
            //dBContext.Rewards.Select();
            return null;
        }
    }
}
