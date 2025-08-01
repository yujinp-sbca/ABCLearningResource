using ABC.Learning.Resource.Domain.Enum;


namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SetBookPenaltyRequestDTO
    {
        public Guid TransactionId { get; set; } = Guid.Empty; // The ID of the book transaction for which the penalty is being set
        public Guid ReportedBy { get; set; } = Guid.Empty; // The ID of the user reporting the lost book
        public BookPenaltyType PenaltyType { get; set; }
        public decimal PenaltyAmount { get; set; } // The amount of the penalty to be set for the lost book
    }
}
