namespace IncidentService.Models
{
	public class PaginationQueryStringOpts
	{
		private int _pageSize = 10;
		private const int _maxPageSize = 100;
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > _maxPageSize) ? _maxPageSize : value;
			}
		}
	}
}
