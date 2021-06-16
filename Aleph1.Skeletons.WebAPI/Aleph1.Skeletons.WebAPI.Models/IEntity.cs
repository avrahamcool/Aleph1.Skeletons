namespace Aleph1.Skeletons.WebAPI.Models
{
	/// <summary>identify the Entities that are available for Read from the generic repository</summary>
	public interface IReadableEntity { }

	/// <summary>identify the Entities that are available for Read+Write from the generic repository</summary>
	public interface IWritableEntity : IReadableEntity { }
}
