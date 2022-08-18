using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrmliteMigrationExample.Entity;

namespace OrmliteMigrationExample.Migrations
{
    public class AddBookTable_20220818 : MigrateBase
    {
        public AddBookTable_20220818(IDbConnection connection) : base(connection)
        {
        }

        public override int Order => 0;
        public override string Name => "Add Book Table";

        public override void Up()
        {
            Connection.AddTableIfNotExists<Book>();
        }
    }
}
