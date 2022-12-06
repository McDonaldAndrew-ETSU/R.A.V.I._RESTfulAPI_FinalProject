using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;

namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public interface ICommandRepository
{
    Task<Command?> CreateAsync(Command command);
    Task<ICollection<Command>> ReadAllCommandsAsync();
    Task<ICollection<Command>> ReadAllAsync();
    Task<Command?> ReadAsync(string commandAction); 
    Task UpdateAsync(string action, string information, string commandType);
    Task DeleteAsync(string action);
}