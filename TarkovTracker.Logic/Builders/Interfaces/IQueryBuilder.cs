using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Builders.Interfaces
{
	/// <summary>
	/// Creates queries for use in the <see cref="IQueryService"/>.
	/// </summary>
	public interface IQueryBuilder
	{
		/// <summary>
		/// Creates an instance of the query builder.
		/// </summary>
		/// <returns>
		/// A query builder, which is meant to be added
		/// to with the <see cref="Add(List{string})"/>
		/// or <see cref="Add(string)"/> functions.
		/// </returns>
		IQueryBuilder Create();

		/// <summary>
		/// Adds a field to the query.
		/// </summary>
		/// <param name="lexeme">
		/// The field to add to the query.
		/// </param>
		/// <returns>
		/// The query builder, to chain further methods
		/// like <see cref="Build"/>.
		/// </returns>
		IQueryBuilder Add(string lexeme);

		/// <summary>
		/// Adds a field to the query.
		/// </summary>
		/// <param name="lexemes">
		/// The fields to add to the query.
		/// The first string will be the "parent",
		/// and subsequent items will be child fields.
		/// </param>
		/// <returns>
		/// The query builder, to chain further methods
		/// like <see cref="Build"/>.
		/// </returns>
		IQueryBuilder Add(List<string> lexemes);

		/// <summary>
		/// Returns the query after being added to.
		/// </summary>
		/// <returns>
		/// A query.
		/// </returns>
		string Build();
	}
}
