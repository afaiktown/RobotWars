using System.Collections.Generic;

namespace RobotWars.Data.Repositories
{
    public interface IRepository<T>
    {
        int Add(T model);
        void Delete(int id);
        IEnumerable<T> Get();
        T GetBy(int id);
        void Update(T model);
    }
}