using _3SemAPI.Data;
using _3SemLibrary;
using _3SemAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

namespace _3SemTests;

[TestClass]
public sealed class ParticipantRepositoryDBTest
{
    private ParticipantContext _context = null!;
    private ParticipantsRepositoryDB _repository = null!;
    private int _newestKey = 1;

    [TestInitialize]
    public void TestInit()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<ParticipantRepositoryDBTest>()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<ParticipantContext>()
            .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
            .Options;

        _context = new ParticipantContext(options);
        _repository = new ParticipantsRepositoryDB(_context);
    }

    [TestMethod]
    public async Task AddAsync_ShouldAddParticipant()
    {
        // Arrange
        if (await _context.Participants.AnyAsync())
        {
            _newestKey = await _context.Participants.MaxAsync(p => p.Id);
            _newestKey++;
        }
        
        var participant = new Participant { Id = _newestKey, Name = "John Doe", Age = 30, Country = "USA" };

        // Act
        await _repository.AddAsync(participant);
        var result = await _context.Participants.FindAsync(_newestKey);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(participant.Name, result.Name);
        Assert.AreEqual(participant.Age, result.Age);
        Assert.AreEqual(participant.Country, result.Country);
    }

    [TestMethod]
    public async Task RemoveAsync_ShouldRemoveParticipant()
    {
        // Arrange
        if (await _context.Participants.AnyAsync())
        {
            _newestKey = await _context.Participants.MaxAsync(p => p.Id);
        }

        // Act
        await _repository.RemoveAsync(_newestKey);
        var result = await _context.Participants.FindAsync(_newestKey);

        // Assert
        Assert.IsNull(result);
    }
}
