namespace NguyenHuuBang_2080600938.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsCanceledColumnToCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsCanceled");
        }
    }
}
