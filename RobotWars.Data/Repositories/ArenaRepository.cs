using System.Collections.Generic;
using RobotWars.Data.Models;

namespace RobotWars.Data.Repositories
{
    public class ArenaRepository : IArenaRepository
    {
        private readonly List<Arena> _arenaRepository = new List<Arena>();
        
        public int Add(Arena model)
        {
            _arenaRepository.Add(model);
            return model.id;

        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Arena> Get()
        {
            return _arenaRepository;
        }

        public Arena GetBy(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Arena model)
        {
            throw new System.NotImplementedException();
        }
    }
}