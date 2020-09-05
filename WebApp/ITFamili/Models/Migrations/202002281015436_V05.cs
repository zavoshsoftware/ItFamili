namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupportRequests", "Message", c => c.String());
            DropColumn("dbo.SupportRequests", "Request");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupportRequests", "Request", c => c.String());
            DropColumn("dbo.SupportRequests", "Message");
        }
    }
}
