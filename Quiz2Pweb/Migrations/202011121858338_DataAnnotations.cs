namespace Quiz2Pweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.Articles", "Category", c => c.String(maxLength: 30));
            AlterColumn("dbo.Articles", "Body", c => c.String(maxLength: 2500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Body", c => c.String());
            AlterColumn("dbo.Articles", "Category", c => c.String());
            AlterColumn("dbo.Articles", "Title", c => c.String());
        }
    }
}
