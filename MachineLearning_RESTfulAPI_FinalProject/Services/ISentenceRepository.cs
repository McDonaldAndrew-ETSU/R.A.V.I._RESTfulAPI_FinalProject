using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public interface ISentenceRepository
{
    Task<Sentence?> CreateAsync(Sentence sentence);
    Task<ICollection<Sentence>> ReadAllSentencesAsync();
    Task<ICollection<Sentence>> ReadAllAsync();
    Task<Sentence?> ReadAsync(string sentenceSpelling);
    Task UpdateAsync(string spelling, string meaning);
    Task DeleteAsync(string sentenceSpelling);
}