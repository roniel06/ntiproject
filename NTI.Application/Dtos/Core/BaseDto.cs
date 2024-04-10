
namespace NTI.Application.Dtos.Core
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModificatedAt { get; set; }
    }
}