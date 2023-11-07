namespace NewsPortal.Domain.Interfaces.Entities
{
    public interface ITimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
