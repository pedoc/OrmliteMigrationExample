using System.Data;
using OrmliteMigrationExample.Entity;
using ServiceStack.OrmLite;

namespace OrmliteMigrationExample.Migrations
{
	public class BookRemoveToDeleteField_20220818 : MigrateBase
	{
		public BookRemoveToDeleteField_20220818(IDbConnection connection) : base(connection)
		{
		}

		public override int Order => 3;
		public override string Name => "Remove ToDelete Filed";

		public override void Down()
		{
			const string columnName = "ToDelete";
			var modelDefinition = typeof(Book).GetModelMetadata();
			if (Connection.ColumnExists(columnName, modelDefinition.Name))
			{
				Connection.DropColumn<Book>(columnName);
			}
		}
	}
}