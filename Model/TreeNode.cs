using System.ComponentModel.DataAnnotations;

namespace ValetaxTest.Model
{
    public class TreeNode
    {
        public long Id { get; set; }
        public long TreeId { get; set; }
        public string Name { get; set; }
        public long? ParentNodeId { get; set; }


    }
}
