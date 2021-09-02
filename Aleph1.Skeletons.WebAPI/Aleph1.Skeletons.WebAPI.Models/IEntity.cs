namespace Aleph1.Skeletons.WebAPI.Models
{
	/// <summary>Identify readable entities from the generic repository</summary>
	public interface IReadableEntity { }

	/// <summary>Identify readable and writable entities from the generic repository</summary>
	public interface IWritableEntity : IReadableEntity { }
}
