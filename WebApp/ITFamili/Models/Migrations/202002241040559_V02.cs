namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForgetPasswordRequests", "UserId", "dbo.Users");
            DropIndex("dbo.ForgetPasswordRequests", new[] { "UserId" });
            AddColumn("dbo.Users", "IsMale", c => c.Boolean());
            AlterColumn("dbo.Users", "FullName", c => c.String(maxLength: 250));
            DropColumn("dbo.Users", "IsVip");
            DropColumn("dbo.Users", "Rate");
            DropTable("dbo.ForgetPasswordRequests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ForgetPasswordRequests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.String(maxLength: 200),
                        DeviceModel = c.String(maxLength: 50),
                        OsType = c.String(maxLength: 50),
                        OsVersion = c.String(),
                        UserId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Rate", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "IsVip", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "FullName", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Users", "IsMale");
            CreateIndex("dbo.ForgetPasswordRequests", "UserId");
            AddForeignKey("dbo.ForgetPasswordRequests", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
