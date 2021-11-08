namespace Programmania.Services.Interfaces
{
    public interface IPerformanceService
    {
        public System.Collections.Generic.IEnumerable<Models.Reward> GetRewards(Models.User user, System.DateTime from, System.DateTime to);

        public System.Collections.Generic.IEnumerable<Models.Reward> GetRewards(Models.User user, int count, int offset);
    }
}
