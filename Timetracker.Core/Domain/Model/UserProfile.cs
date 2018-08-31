namespace Timetracker.Core.Domain.Model
{
	public class UserProfile : Entity<int>
	{
		public string Email { get; set; }

		public string UserName { get; set; }

		public bool IsActive { get; set; }
	}
}
