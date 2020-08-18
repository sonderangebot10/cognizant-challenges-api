namespace Conginzant.Domain.Challenges
{
    /// <summary>
    /// The challenge.
    /// </summary>
    public class Challenge
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string InputParam { get; set; }

        public string OutputParam { get; set; }

        public Challenge(string id, string taskName, string description, string inputParam, string outputParam)
        {
            Id = id;
            Name = taskName;
            Description = description;
            InputParam = inputParam;
            OutputParam = outputParam;
        }
    }
}
