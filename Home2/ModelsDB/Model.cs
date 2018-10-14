namespace Home2.ModelsDB
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {
        public Model() : base("name=Model")
        {
            Database.SetInitializer<Model>(new CustomInitializer<Model>());
        }
        public virtual DbSet<User> Users { get; set; }
    }
}