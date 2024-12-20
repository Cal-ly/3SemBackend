namespace _3SemLibrary;
public class ParticipantsRepository
{
    private readonly List<Participant> _participants;
    private int _nextId;

    public ParticipantsRepository()
    {
        _participants = [];
        _nextId = 1;

        // Adding 5 Participant objects to the list
        Add(new Participant { Name = "Alice", Age = 25, Country = "USA" });
        Add(new Participant { Name = "Bob", Age = 30, Country = "Canada" });
        Add(new Participant { Name = "Charlie", Age = 22, Country = "Denmark" });
        Add(new Participant { Name = "Diana", Age = 28, Country = "Australia" });
        Add(new Participant { Name = "Eve", Age = 35, Country = "Germany" });
    }

    public List<Participant> GetAll()
    {
        return _participants;
    }

    public Participant GetById(int id)
    {
        return _participants.FirstOrDefault(p => p.Id == id) ?? throw new ArgumentException("No participant with such Id");
    }

    public void Add(Participant participant)
    {
        participant.Id = _nextId++;
        _participants.Add(participant);
    }

    public void Update(int id, Participant participant)
    {
        var existingParticipant = GetById(id);
        if (existingParticipant != null)
        {
            existingParticipant.Name = participant.Name;
            existingParticipant.Age = participant.Age;
            existingParticipant.Country = participant.Country;
        }
    }

    public void Remove(int id)
    {
        var participant = GetById(id);
        if (participant != null)
        {
            _participants.Remove(participant);
        }
    }
}
