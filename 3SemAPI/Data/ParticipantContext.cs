using _3SemLibrary;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace _3SemAPI.Data;

public class ParticipantContext : DbContext
{
    public DbSet<Participant> Participants { get; set; }

    public ParticipantContext(DbContextOptions<ParticipantContext> options) : base(options)
    {
    }
}