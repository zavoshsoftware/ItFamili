namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Contents", "CommentCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "CommentCount");
            DropColumn("dbo.Contents", "LikeCount");
        }
    }
}
