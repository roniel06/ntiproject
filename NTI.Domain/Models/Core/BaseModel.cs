using System.ComponentModel.DataAnnotations;

namespace NTI.Domain.Models.Core
{
	public abstract class BaseModel
	{
		[Key]
		public int Id { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
        public string? ModificatedBy { get; set; }
        public DateTime? ModificatedAt { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}

