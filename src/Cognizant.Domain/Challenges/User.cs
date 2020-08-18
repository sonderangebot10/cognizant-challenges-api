namespace Conginzant.Domain.Challenges
{
    /// <summary>
    /// The challenge.
    /// </summary>
    public class User
    {
        public string Name { get; set; }

        public int SuccessSolutions { get; set; }

        public string Tasks { get; set; }

        public User(string name, int successSolutions, string tasks)
        {
            Name = name;
            SuccessSolutions = successSolutions;
            Tasks = tasks;
        }
    }
}
