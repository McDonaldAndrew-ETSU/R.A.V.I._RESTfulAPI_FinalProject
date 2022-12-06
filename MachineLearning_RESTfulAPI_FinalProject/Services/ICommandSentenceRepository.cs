using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public interface ICommandSentenceRepository
{
    Task<CommandSentence?> CreateAsync(string commandAction, string sentenceSpelling);
    Task<ICollection<CommandSentence>> ReadAllCommandSentencesAsync(); 
    Task<ICollection<CommandSentence>> ReadAllAsync(); 
    Task<CommandSentence?> ReadAsync(int commandSentenceId); 
    Task UpdateAsync(int id, string commandAction, string sentenceSpelling);
    Task DeleteAsync(CommandSentence commandSentence);
}
