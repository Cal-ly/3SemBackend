using Microsoft.EntityFrameworkCore;
using _3SemLibrary;
using _3SemAPI.Data;

namespace _3SemAPI.Repositories;

public class ParticipantsRepositoryDB
{
    private readonly ParticipantContext _context;

    public ParticipantsRepositoryDB(ParticipantContext context)
    {
        _context = context;
    }

    public async Task<List<Participant>> GetAllAsync()
    {
        return await _context.Participants.ToListAsync();
    }

    public async Task<Participant> GetByIdAsync(int id)
    {
        return await _context.Participants.FindAsync(id) ?? throw new ArgumentException("No participant with such Id");
    }

    public async Task AddAsync(Participant participant)
    {
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Participant participant)
    {
        var existingParticipant = await GetByIdAsync(id);
        if (existingParticipant != null)
        {
            existingParticipant.Name = participant.Name;
            existingParticipant.Age = participant.Age;
            existingParticipant.Country = participant.Country;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(int id)
    {
        var participant = await GetByIdAsync(id);
        if (participant != null)
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        }
    }
}
