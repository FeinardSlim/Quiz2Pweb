namespace Quiz2Pweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Category");
        }
    }
}
