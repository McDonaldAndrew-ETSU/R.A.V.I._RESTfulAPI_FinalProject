using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineLearning_RESTfulAPI_FinalProject.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Command> Commands => Set<Command>();
    public DbSet<Sentence> Sentences => Set<Sentence>();
    public DbSet<CommandSentence> CommandSentences => Set<CommandSentence>();
}
