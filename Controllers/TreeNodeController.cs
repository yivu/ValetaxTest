using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ValetaxTest.Model;

namespace ValetaxTest.Controllers
{
    public class TreeNodeController : Controller
    {
        private readonly ValetaxDBContext _dbContext;
        public TreeNodeController(ValetaxDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [Route("api.user.tree.node.create")]
        [HttpPost]
        [ProducesResponseType<TreeNode>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateTreeNode([FromQuery][Required] string treeName, [FromQuery] long parentNodeId = 0,
            [FromQuery][Required] string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                return UnprocessableEntity("Invalid nodeName.");
            }
            var tree = await _dbContext.Trees.SingleOrDefaultAsync(tree => tree.Name == treeName);
            if (tree == null) 
            {
                return UnprocessableEntity("Tree with such a name does not exist.");
            }

            if(parentNodeId == 0 && await _dbContext.TreeNodes.SingleOrDefaultAsync(node => node.ParentNodeId == 0 && node.TreeId == tree.Id) != null)
            {
                return UnprocessableEntity("Cannot add second root to the tree.");
            }

            var node = await _dbContext.TreeNodes.SingleOrDefaultAsync(node => node.TreeId == tree.Id && node.ParentNodeId == parentNodeId && node.Name == nodeName);
            if (node != null)
            {
                return UnprocessableEntity("Cannot add sibling with same name");
            }

            node = new TreeNode()
            {
                Name = nodeName,
                ParentNodeId = parentNodeId,
                TreeId = tree.Id,
            };
            
            _dbContext.TreeNodes.Add(node);
            await _dbContext.SaveChangesAsync();
            return Ok(node);
        }

        [Route("api.user.tree.node.delete")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTreeNode([FromQuery][Required] long nodeId)
        {
            var node = await _dbContext.TreeNodes.FindAsync(nodeId);
            if (node == null)
            {
                return NotFound();
            }

            _dbContext.TreeNodes.Remove(node);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [Route("api.user.tree.node.rename")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RenameTreeNode([FromQuery][Required] long nodeId, [FromQuery][Required] string newNodeName)
        {
            if (string.IsNullOrEmpty(newNodeName))
            {
                return UnprocessableEntity("Invalid name");
            }
            var node = await _dbContext.TreeNodes.FindAsync(nodeId);
            if (node == null)
            {
                return NotFound();
            }

            node.Name = newNodeName;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }   

    }
}
