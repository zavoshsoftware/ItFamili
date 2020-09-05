namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Magzines", "SummerySite", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Magzines", "Summery", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Magzines", "Summery", c => c.String());
            DropColumn("dbo.Magzines", "SummerySite");
        }
    }
}
