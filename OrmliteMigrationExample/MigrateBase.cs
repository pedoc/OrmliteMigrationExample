using System.Data;

// ReSharper disable once CheckNamespace
namespace ServiceStack.OrmLite
{
    public abstract class MigrateBase
    {
        protected readonly IDbConnection Connection;
        protected MigrateBase(IDbConnection connection)
        {
            this.Connection = connection;
        }

        public virtual int Order { get; } = 0;
        public abstract string Name { get; }
        public virtual string Description { get; } = "";
        public bool Ignore { get; set; } = false;

        public virtual void Up() { }
        public virtual void Down() { }
        public virtual void SeedData() { }
    }

}
