using System;
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
            return model.id;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Robot> Get()
        {
            return _robotRepository;
        }

        public Robot GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Robot model)
        {
            throw new NotImplementedException();
        }

        public Robot getLastUsed()
        {
            return _robotRepository.First(r => r.lastUsed);
        }
    }
}