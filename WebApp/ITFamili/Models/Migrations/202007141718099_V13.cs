namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArAssets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InputImageUrl = c.String(),
                        InputSize = c.String(),
                        OutPutType = c.String(),
                        OutputFileUrl = c.String(),
                        MagzineId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magzines", t => t.MagzineId, cascadeDelete: true)
                .Index(t => t.MagzineId);
            
            AddColumn("dbo.Magzines", "Version", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArAssets", "MagzineId", "dbo.Magzines");
            DropIndex("dbo.ArAssets", new[] { "MagzineId" });
            DropColumn("dbo.Magzines", "Version");
            DropTable("dbo.ArAssets");
        }
    }
}
