namespace ValetaxTest.Model
{
    public class MJournal
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public Guid EventId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
