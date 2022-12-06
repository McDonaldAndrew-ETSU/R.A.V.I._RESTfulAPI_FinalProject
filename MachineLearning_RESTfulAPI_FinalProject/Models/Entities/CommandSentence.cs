namespace MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

public class CommandSentence
{
    public int Id { get; set; }

    public string CommandAction { get; set; } = String.Empty;
    public Command? Command { get; set; }

    public string SentenceSpelling { get; set; } = String.Empty;
    public Sentence? Sentence { get; set; }
}
