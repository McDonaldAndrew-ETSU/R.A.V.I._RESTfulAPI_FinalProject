using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public class DbCommandSentenceRepository : ICommandSentenceRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ICommandRepository _commandRepo;
    private readonly ISentenceRepository _sentenceRepo;

    public DbCommandSentenceRepository(ApplicationDbContext db, ICommandRepository commandRepo, ISentenceRepository sentenceRepo)
    {
        _db = db;
        _commandRepo = commandRepo;
        _sentenceRepo = sentenceRepo;
    }

    public async Task<CommandSentence?> CreateAsync(string commandAction, string sentenceSpelling)
    {
        var command = await _commandRepo.ReadAsync(commandAction);
        if (command == null)
        {
            //The Command was not found
            return null;
        }

        var commandSentenceAlreadyExists = command.CommandSentences.FirstOrDefault(cs => cs.SentenceSpelling == sentenceSpelling);
        if (commandSentenceAlreadyExists != null)
        {
            //The Command already has a CommandSentence for the SentenceSpelling used
            return null;
        }

        var sentence = await _sentenceRepo.ReadAsync(sentenceSpelling);
        if (sentence == null)
        {
            //The Sentence was not found
            return null;
        }

        var newCommandSentence = new CommandSentence
        {
            Command = command,
            Sentence = sentence
        };

        await _db.CommandSentences.AddAsync(newCommandSentence);

        await _db.SaveChangesAsync();

        return newCommandSentence;
    }


    public async Task<ICollection<CommandSentence>> ReadAllCommandSentencesAsync()
    {
        var commandSentences = await ReadAllAsync();

        foreach (var commandSentence in commandSentences)
        {
            commandSentence.Command?.CommandSentences.Clear();
            commandSentence.Sentence?.CommandSentences.Clear();
        }

        return commandSentences;
    }

    public async Task<ICollection<CommandSentence>> ReadAllAsync()
    {
        return await _db.CommandSentences //can use this without the Includes in the "ReadAllCommandSentencesAsync"
            .Include(sc => sc.Command)
            .Include(sc => sc.Sentence)
            .ToListAsync();
    }

    public async Task<CommandSentence?> ReadAsync(int commandSentenceId)
    {
        return await _db.CommandSentences
            .Include(cs => cs.Command)
            .Include(cs => cs.Sentence)
            .FirstOrDefaultAsync(sc => sc.Id == commandSentenceId);
    }


    public async Task UpdateAsync(int id, string commandAction, string sentenceSpelling)
    {
        var commandSentenceToUpdate = await ReadAsync(id);

        var commandToUpdateCSWith = await _commandRepo.ReadAsync(commandAction);

        var sentenceToUpdateCSWith = await _sentenceRepo.ReadAsync(sentenceSpelling);


        var seeIfACommandSentenceAlreadyExists = commandToUpdateCSWith!.CommandSentences.FirstOrDefault(cs => cs.SentenceSpelling == sentenceSpelling);
        if (seeIfACommandSentenceAlreadyExists == null )
        {
            commandSentenceToUpdate!.CommandAction = commandToUpdateCSWith.Action;
            commandSentenceToUpdate!.SentenceSpelling = sentenceToUpdateCSWith!.Spelling;

            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(CommandSentence commandSentence)
    {
        _db.CommandSentences.Remove(commandSentence);
        
        await _db.SaveChangesAsync();
    }


}

