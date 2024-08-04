using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Builders.Interfaces;

namespace TarkovTracker.Logic.Builders
{
	/// <inheritdoc cref="IQueryBuilder"/>
	[Service(typeof(IQueryBuilder), Lifetime = ServiceLifetime.Transient)]
	public class QueryBuilder : IQueryBuilder
	{
		private readonly ILogger<QueryBuilder> _logger;

		private List<string>? _query;
		private List<string> Query => _query ??= [];

		public QueryBuilder(ILogger<QueryBuilder> logger)
		{
			_logger = logger;
		}

		public IQueryBuilder Create()
		{
			return this;
		}

		public IQueryBuilder Add(string lexeme)
		{
			if (string.IsNullOrEmpty(lexeme))
			{
				_logger.LogWarning($"{nameof(Add)}: Attempted to add to query, but lexeme provided was null or empty.");
				return this;
			}

			Query.Add(lexeme);
			return this;
		}

		public IQueryBuilder Add(List<string> lexemes)
		{
			if (lexemes == null || lexemes.Count == 0)
			{
				_logger.LogWarning($"{nameof(Add)}: Attempted to add to query, but no lexemes were provided.");
				return this;
			}

			if (lexemes.Count == 1)
			{
				Add(lexemes.First());
				return this;
			}

			var parentLexeme = lexemes.First();
			var childLexemes = lexemes.Where(lexeme => lexeme != lexemes.First());
			Query.Add($"{parentLexeme}{{{string.Join(" ", childLexemes)}}}");
			return this;
		}

		public string Build()
		{
			return $"{{{string.Join(Environment.NewLine, Query)}}}";
		}
	}
}
