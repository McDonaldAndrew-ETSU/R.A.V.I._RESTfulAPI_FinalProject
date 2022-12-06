using System.ComponentModel.DataAnnotations;

namespace MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

public class Command
{
    [Key, StringLength(50)]
    public string Action { get; set; } = String.Empty;
    [StringLength(512)]
    public string Information { get; set; } = String.Empty;
    [StringLength(20)]
    public string CommandType { get; set; } = String.Empty;
    public ICollection<CommandSentence> CommandSentences { get; set; } = new List<CommandSentence>();  
}
