using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public class DbSentenceRepository : ISentenceRepository
{
    private readonly ApplicationDbContext _db;
    public DbSentenceRepository(ApplicationDbContext db)
    {
        _db = db;
    }



    public async Task<Sentence?> CreateAsync(Sentence sentence)
    {
        var sentenceAlreadyExists = await ReadAsync(sentence.Spelling);
        if (sentenceAlreadyExists != null)
        {
            return null;
        }

        await _db.Sentences.AddAsync(sentence);
        await _db.SaveChangesAsync();

        return sentence;
    }


    public async Task<ICollection<Sentence>> ReadAllSentencesAsync()
    {
        var sentences = await ReadAllAsync();

        foreach (var sentence in sentences)
        {
            sentence.CommandSentences.Clear();
        }

        return sentences;
    }

    public async Task<ICollection<Sentence>> ReadAllAsync()
    {
        return await _db.Sentences
            .Include(s => s.CommandSentences)
                .ThenInclude(cs => cs.Command)
            .ToListAsync();
    }

    public async Task<Sentence?> ReadAsync(string sentenceSpelling)
    {
        return await _db.Sentences
            .Include(s => s.CommandSentences)
                .ThenInclude(cs => cs.Command)
            .FirstOrDefaultAsync(s => s.Spelling == sentenceSpelling);
    }


    public async Task UpdateAsync(string spelling, string meaning)
    {
        var sentenceToUpdate = await ReadAsync(spelling);
        if (sentenceToUpdate != null)
        {
            sentenceToUpdate.Meaning = meaning;

            await _db.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(string sentenceSpelling)
    {
        var sentenceToDelete = await ReadAsync(sentenceSpelling);

        var commandSentenceExists = _db.CommandSentences.FirstOrDefault(cs => cs.SentenceSpelling == sentenceSpelling);
        if (commandSentenceExists == null)
        {
            _db.Sentences.Remove(sentenceToDelete!);
            await _db.SaveChangesAsync();
        }
    }



}
