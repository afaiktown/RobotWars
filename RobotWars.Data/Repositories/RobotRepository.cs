using System.Collections.Generic;
using System.Linq;
using RobotWars.Data.Models;

namespace RobotWars.Data.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        private readonly List<Robot> _robotRepository = new List<Robot>();
        
        public int Add(Robot model)
        {
            _robotRepository.Add(model);
            return 0;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Robot> Get()
        {
            return _robotRepository;
        }

        public Robot GetBy(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Robot model)
        {
            throw new System.NotImplementedException();
        }

        public Robot getLastUsed()
        {
            return _robotRepository.First(r => r.lastUsed);
        }

        public void setLastUsed(Robot robot)
        {
            Robot oldR = getLastUsed();
            oldR.lastUsed = false;

            Robot newR = _robotRepository.Find(r => r.id == robot.id);
            newR.lastUsed = true;

        }
    }
}