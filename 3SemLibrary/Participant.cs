namespace _3SemLibrary;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Country { get; set; } = null!;

    public void ValidateName()
    {
        if (Name == null)
        {
            throw new ArgumentNullException("Name must not be null.");
        }
        if (Name.Length < 2)
        {
            throw new ArgumentException("Name must be at least two characters long.");
        }

    }

    public void ValidateAge()
    {
        if (Age < 12)
        {
            throw new ArgumentException("Age must be a positive number.");
        }
    }

    public void ValidateCountry()
    {
        if (Country == null)
        {
            throw new ArgumentNullException("Name must not be null.");
        }
        if (Country.Length < 3)
        {
            throw new ArgumentException("Country must be at least three characters long.");
        }
    }

    public void ValidateParticipant()
    {
        ValidateName();
        ValidateAge();
        ValidateCountry();
    }
    public override string ToString()
    {
        return $"Participant [Id={Id}, Name={Name}, Age={Age}, Country={Country}]";
    }

}
