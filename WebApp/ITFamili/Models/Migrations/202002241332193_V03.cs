namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contents", "IsInHome", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contents", "IsInHome", c => c.String());
        }
    }
}
