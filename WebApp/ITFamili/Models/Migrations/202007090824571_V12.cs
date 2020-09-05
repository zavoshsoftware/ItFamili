namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                        UrlParam = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contents", "ContentGroupId", c => c.Guid());
            CreateIndex("dbo.Contents", "ContentGroupId");
            AddForeignKey("dbo.Contents", "ContentGroupId", "dbo.ContentGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "ContentGroupId", "dbo.ContentGroups");
            DropIndex("dbo.Contents", new[] { "ContentGroupId" });
            DropColumn("dbo.Contents", "ContentGroupId");
            DropTable("dbo.ContentGroups");
        }
    }
}
