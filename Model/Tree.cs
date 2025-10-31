namespace ValetaxTest.Model
{
    public class Tree
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}
