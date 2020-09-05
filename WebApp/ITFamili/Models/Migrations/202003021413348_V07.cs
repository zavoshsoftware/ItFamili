namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Magzines", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Magzines", "CommentCount", c => c.Int(nullable: false));
            AddColumn("dbo.Magzines", "Summery", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Magzines", "Summery");
            DropColumn("dbo.Magzines", "CommentCount");
            DropColumn("dbo.Magzines", "LikeCount");
        }
    }
}
