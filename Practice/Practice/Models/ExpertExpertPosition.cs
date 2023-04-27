namespace Practice.Models
{
    public class ExpertExpertPosition : BaseEntity
    {
        public int ExpertId { get; set; }
        public int ExpertPositionId { get; set; }
        public Expert Expert { get; set; }
        public ExpertPosition ExpertPosition { get; set; }
    }
}
