namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Comment = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ContentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContentLikes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ContentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContentLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContentLikes", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentComments", "ContentId", "dbo.Contents");
            DropIndex("dbo.ContentLikes", new[] { "UserId" });
            DropIndex("dbo.ContentLikes", new[] { "ContentId" });
            DropIndex("dbo.ContentComments", new[] { "UserId" });
            DropIndex("dbo.ContentComments", new[] { "ContentId" });
            DropTable("dbo.ContentLikes");
            DropTable("dbo.ContentComments");
        }
    }
}
