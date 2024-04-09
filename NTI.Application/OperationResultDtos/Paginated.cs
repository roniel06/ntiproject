using System;
namespace NTI.Application.OperationResultDtos
{
    /// <summary>
    /// This class represents a pagination of a T entity or Dto.
    /// </summary>
    /// <typeparam name="T">The Entity or Dto</typeparam>
	public class Paginated<T>
	{
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T>? Items { get; set; }
    }
}

