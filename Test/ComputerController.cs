using Test.Models;

namespace Test;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/computer")]
public class ComputerController : ControllerBase
{
    private readonly IComputerService _computerService;
    
    public ComputerController(IComputerService computerService)
    {
        _computerService = computerService;
    }
    
    [HttpPost ("/addCpu")]
    public IActionResult AddCpu([FromBody] Cpu cpu)
    {
        var newId = _computerService.AddCpu(cpu);
        return CreatedAtAction("", new { id = newId }, cpu);

    }
    
    [HttpPost ("/addVideocard")]
    public IActionResult AddVideocard([FromBody] Videocard videocard)
    {
        var newId = _computerService.AddVideocard(videocard);
        return CreatedAtAction("", new { id = newId }, videocard);

    }
    
    [HttpPost ("/addComputer")]
    public IActionResult AddComputer([FromBody] string cpu, string videocard, string name)
    {
        var newId = _computerService.AddComputer(cpu, videocard, name);
        if (newId == 0)
            return BadRequest();
        return CreatedAtAction("", new { id = newId }, cpu);

    }
    
    [HttpDelete ("/deleteComputer")]
    public IActionResult DeleteComputer([FromBody] int id)
    {
        if(_computerService.DeleteComputer(id))
            return Ok();
        return BadRequest();
    }
    
}