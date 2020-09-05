namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivationCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                        UsingDate = c.DateTime(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Password = c.String(maxLength: 150),
                        CellNum = c.String(nullable: false, maxLength: 20),
                        FullName = c.String(nullable: false, maxLength: 250),
                        Code = c.Int(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Token = c.String(),
                        RemainCredit = c.Decimal(precision: 18, scale: 2),
                        OsType = c.String(maxLength: 50),
                        VersionNumber = c.String(),
                        IsVip = c.Boolean(nullable: false),
                        Email = c.String(),
                        Rate = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ParentId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProvinceId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        IsCenter = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                        UrlParam = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Summery = c.String(),
                        Body = c.String(),
                        FileUrl = c.String(),
                        PageTitleTag = c.String(),
                        PageMetaDescription = c.String(),
                        ContentTypeId = c.Guid(nullable: false),
                        IsInSlider = c.Boolean(nullable: false),
                        IsInHome = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentTypes", t => t.ContentTypeId, cascadeDelete: true)
                .Index(t => t.ContentTypeId);
            
            CreateTable(
                "dbo.ContentTypes",
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
            
            CreateTable(
                "dbo.Magzines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                        PublishDate = c.DateTime(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        FileUrl = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 500),
                        UrlParam = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VersionHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VersionNumber = c.String(),
                        Os = c.String(),
                        IsNeccessary = c.Boolean(nullable: false),
                        LatestStableVersion = c.String(),
                        IsBeta = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "ContentTypeId", "dbo.ContentTypes");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "ParentId", "dbo.Users");
            DropForeignKey("dbo.ForgetPasswordRequests", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivationCodes", "UserId", "dbo.Users");
            DropIndex("dbo.Contents", new[] { "ContentTypeId" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.ForgetPasswordRequests", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ParentId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ActivationCodes", new[] { "UserId" });
            DropTable("dbo.VersionHistories");
            DropTable("dbo.Magzines");
            DropTable("dbo.ContentTypes");
            DropTable("dbo.Contents");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Roles");
            DropTable("dbo.ForgetPasswordRequests");
            DropTable("dbo.Users");
            DropTable("dbo.ActivationCodes");
        }
    }
}
