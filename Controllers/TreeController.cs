using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ValetaxTest.Model;

namespace ValetaxTest.Controllers;

[ApiController]
public class TreeController : Controller
{
    private readonly ValetaxDBContext _dbContext;

    public TreeController(ValetaxDBContext context)
    {
        _dbContext = context;
    }

    [Route("api.user.tree.get")]
    [HttpPost]
    [ProducesResponseType(typeof(Tree), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrCreateTree([FromQuery][Required] string treeName)
    {
        var tree = await _dbContext.Trees.SingleOrDefaultAsync(tree => tree.Name == treeName);
        if (tree is null)
        {
            tree = new Tree { Name = treeName };
            _dbContext.Trees.Add(tree);
            await _dbContext.SaveChangesAsync();
        }

        return Ok(tree);
    }
}
