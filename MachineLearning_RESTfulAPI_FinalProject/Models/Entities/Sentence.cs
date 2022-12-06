using System.ComponentModel.DataAnnotations;

namespace MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

public class Sentence
{
    [Key, StringLength(256)]
    public string Spelling { get; set; } = String.Empty;
    [StringLength(256)]
    public string Meaning { get; set; } = String.Empty;
    public ICollection<CommandSentence> CommandSentences { get; set; } = new List<CommandSentence>();
}
 