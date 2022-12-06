using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public class DbCommandRepository : ICommandRepository
{
    private readonly ApplicationDbContext _db;

    public DbCommandRepository(ApplicationDbContext db)
    {
        _db = db;
    }



    public async Task<Command?> CreateAsync(Command command)
    {
        var cmdAlreadyExists = await ReadAsync(command.Action);
        if(cmdAlreadyExists != null)
        {
            return null;
        }

        await _db.Commands.AddAsync(command);
        await _db.SaveChangesAsync();

        return command;
    }


    public async Task<ICollection<Command>> ReadAllCommandsAsync()
    {
        var commands = await ReadAllAsync();

        foreach (var command in commands)
        {
            command.CommandSentences.Clear();
        }

        return commands;
    }

    public async Task<ICollection<Command>> ReadAllAsync()
    {
        return await _db.Commands
            .Include(c => c.CommandSentences)
                .ThenInclude(cs => cs.Sentence)
            .ToListAsync();
    }

    public async Task<Command?> ReadAsync(string commandAction)
    {
        return await _db.Commands
            .Include(c => c.CommandSentences)
                .ThenInclude(cs => cs.Sentence)
            .FirstOrDefaultAsync(c => c.Action == commandAction);
    }


    public async Task UpdateAsync(string action, string information, string commandType)
    {
        var commandToUpdate = await ReadAsync(action);
        if (commandToUpdate != null)
        {
            commandToUpdate.Information = information;
            commandToUpdate.CommandType = commandType;

            await _db.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(string action)
    {
        var commandToDelete = await ReadAsync(action);

        var commandSentenceExists = _db.CommandSentences.FirstOrDefault(cs => cs.CommandAction == action);
        if(commandSentenceExists == null)
        {
            _db.Commands.Remove(commandToDelete!);
            await _db.SaveChangesAsync();
        }
    }


}