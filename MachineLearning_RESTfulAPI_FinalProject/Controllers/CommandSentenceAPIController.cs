using MachineLearning_RESTfulAPI_FinalProject.Models.Entities;
using MachineLearning_RESTfulAPI_FinalProject.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MachineLearning_RESTfulAPI_FinalProject.Controllers;

[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class CommandSentenceAPIController : Controller
{
    private readonly ICommandRepository _commandRepo;
    private readonly ISentenceRepository _sentenceRepo;
    private readonly ICommandSentenceRepository _commandSentenceRepo;

    public CommandSentenceAPIController(ICommandRepository commandRepo, ISentenceRepository sentenceRepo, ICommandSentenceRepository commandSentenceRepo)
    {
        _commandRepo = commandRepo;
        _sentenceRepo = sentenceRepo;
        _commandSentenceRepo = commandSentenceRepo;
    }



    //*****************************************************HttpPost Create methods*****************************************************



    [HttpPost("createcommand")]
    public async Task<IActionResult> PostCommandAsync([FromForm] Command command)
    {
        await _commandRepo.CreateAsync(command);
        return CreatedAtAction("Get", new { action = command.Action }, command);
    }

    [HttpPost("createcommandsentence")]
    public async Task<IActionResult> PostCommandSentenceAsync([FromForm] string commandAction, [FromForm] string sentenceSpelling)
    {
        var newCommandSentence = await _commandSentenceRepo.CreateAsync(commandAction, sentenceSpelling);

        // Remove the circular reference for the JSON
        newCommandSentence?.Command?.CommandSentences.Clear();
        newCommandSentence?.Sentence?.CommandSentences.Clear();

        return CreatedAtAction("Get", new { id = newCommandSentence?.Id }, newCommandSentence);
    }

    [HttpPost("createsentence")]
    public async Task<IActionResult> PostSentenceAsync([FromForm] Sentence sentence)
    {
        await _sentenceRepo.CreateAsync(sentence);
        return CreatedAtAction("Get", new { spelling = sentence.Spelling }, sentence);
    }



    //*****************************************************HttpGet ReadALL methods*****************************************************



    [HttpGet("allcommands")]
    public async Task<IActionResult> GetCommandAsync()
    {
        return Ok(await _commandRepo.ReadAllCommandsAsync());
    }

    [HttpGet("allcommandsentences")]
    public async Task<IActionResult> GetCommandSentenceAsync()
    {
        return Ok(await _commandSentenceRepo.ReadAllCommandSentencesAsync());
    }

    [HttpGet("allsentences")]
    public async Task<IActionResult> GetSentenceAsync()
    {
        return Ok(await _sentenceRepo.ReadAllSentencesAsync());
    }



    //*****************************************************HttpGet Read methods*****************************************************



    [HttpGet("onecommand/{id}")]
    public async Task<IActionResult> GetCommandAsync(string id)
    {
        var command = await _commandRepo.ReadAsync(id);
        if (command == null)
        {
            return NotFound();
        }

        // Remove the circular reference for the JSON
        command?.CommandSentences?.Clear();

        return Ok(command);
    }

    [HttpGet("onecommandsentence/{id}")]
    public async Task<IActionResult> GetCommandSentenceAsync(int id)
    {
        var commandSentence = await _commandSentenceRepo.ReadAsync(id);
        if (commandSentence == null)
        {
            return NotFound();
        }

        // Remove the circular reference for the JSON
        commandSentence?.Command?.CommandSentences.Clear();
        commandSentence?.Sentence?.CommandSentences.Clear();

        return Ok(commandSentence);
    }

    [HttpGet("onesentence/{id}")]
    public async Task<IActionResult> GetSentenceAsync(string id)
    {
        var sentence = await _sentenceRepo.ReadAsync(id);
        if (sentence == null)
        {
            return NotFound();
        }

        sentence?.CommandSentences.Clear();

        return Ok(sentence);
    }



    //*****************************************************HttpPut Update methods*****************************************************



    [HttpPut("updatecommand")]
    public async Task<IActionResult> PutCommandAsync([FromForm]string action, [FromForm]string information, [FromForm]string commandType)
    {
        await _commandRepo.UpdateAsync(action, information, commandType);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpPut("updatecommandsentence")]
    public async Task<IActionResult> PutCommandSentenceAsync([FromForm]int id, [FromForm] string commandAction, [FromForm] string sentenceSpelling)
    {
        await _commandSentenceRepo.UpdateAsync(id, commandAction, sentenceSpelling);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpPut("updatesentence")]
    public async Task<IActionResult> PutSentenceAsync([FromForm]string spelling, [FromForm]string meaning)
    {
        await _sentenceRepo.UpdateAsync(spelling, meaning);
        return NoContent(); // 204 as per HTTP specification
    }



    //*****************************************************HttpDelete Delete methods*****************************************************



    [HttpDelete("deletecommand/{id}")]
    public async Task<IActionResult> DeleteCommandAsync(string id)
    {
        var command = await _commandRepo.ReadAsync(id);
        if (command == null)
        {
            return NotFound();
        }

        await _commandRepo.DeleteAsync(id);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpDelete("deletecommandsentence/{id}")]
    public async Task<IActionResult> DeleteCommandSentencesync(int id)
    {
        var commandSentence = await _commandSentenceRepo.ReadAsync(id);
        if (commandSentence == null)
        {
            return NotFound();
        }

        await _commandSentenceRepo.DeleteAsync(commandSentence);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpDelete("deletesentence/{id}")]
    public async Task<IActionResult> DeleteSentencesync(string id)
    {
        var sentence = await _sentenceRepo.ReadAsync(id);
        if (sentence == null)
        {
            return NotFound();
        }

        await _sentenceRepo.DeleteAsync(id);
        return NoContent(); // 204 as per HTTP specification
    }




}
