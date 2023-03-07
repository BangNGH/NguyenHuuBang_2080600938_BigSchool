namespace NguyenHuuBang_2080600938.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Categories (Id, Name) values (1, 'Development')");
            Sql("Insert into Categories (Id, Name) values (2, 'Business')");
            Sql("Insert into Categories (Id, Name) values (3, 'Marketing')");
        }

        public override void Down()
        {
        }
    }
}
