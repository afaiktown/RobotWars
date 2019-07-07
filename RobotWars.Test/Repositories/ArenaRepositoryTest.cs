using System.Linq;
using RobotWars.Data.Models;
using RobotWars.Data.Repositories;
using Xunit;

namespace RobotWars.Test.Repositories
{
    public class ArenaRepositoryTest
    {
        public ArenaRepositoryTest()
        {
            _arenaRepository = new ArenaRepository();
        }

        private readonly ArenaRepository _arenaRepository;

        [Fact]
        public void Add_NewArena_WithActiveStatus()
        {
            // arange
            var arena = new Arena(5, 5);

            // act
            _arenaRepository.Add(arena);

            // assert
            var createdArena = _arenaRepository.Get().Single();
            Assert.Equal(true, createdArena.lastUsed);
        }

        [Fact]
        public void Add_NewArena_WithIncorrectParameters()
        {
            // arange
            var arena = new Arena(-5, -5);

            // act
            _arenaRepository.Add(arena);

            // assert
            var createdArena = _arenaRepository.Get().Single();
            Assert.Equal(0, createdArena.arenaX);
            Assert.Equal(0, createdArena.arenaY);
        }
    }
}