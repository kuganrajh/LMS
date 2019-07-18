namespace WLV.LMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.String(nullable: false, maxLength: 20),
                        Title = c.String(nullable: false),
                        Publisher = c.String(),
                        PublishedDate = c.String(),
                        Description = c.String(),
                        PageCount = c.Int(nullable: false),
                        InfoLink = c.String(),
                        ImageLink = c.String(),
                        BookCurrentCount = c.Int(nullable: false),
                        BookTotalCount = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ISBN, unique: true);
            
            CreateTable(
                "dbo.BorrowBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        ReserveBookId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .ForeignKey("dbo.ReserveBooks", t => t.ReserveBookId)
                .Index(t => t.BookId)
                .Index(t => t.MemberId)
                .Index(t => t.ReserveBookId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefNumber = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        SSID = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false, maxLength: 20),
                        StreetAddressFirst = c.String(nullable: false, maxLength: 150),
                        StreetAddressSecond = c.String(nullable: false, maxLength: 150),
                        City = c.String(nullable: false, maxLength: 150),
                        State = c.String(nullable: false, maxLength: 150),
                        ZipCode = c.String(nullable: false, maxLength: 20),
                        Country = c.String(nullable: false, maxLength: 100),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.RefNumber, unique: true)
                .Index(t => t.MobileNumber, unique: true)
                .Index(t => t.UserId, unique: true);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ReserveBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarrowDate = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.ReturnBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowBookId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BorrowBooks", t => t.BorrowBookId)
                .Index(t => t.BorrowBookId, unique: true);
            
            CreateTable(
                "dbo.LatePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoOfDateDelays = c.Int(nullable: false),
                        PaymentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReturnBookId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReturnBooks", t => t.ReturnBookId)
                .Index(t => t.ReturnBookId, unique: true);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SystemOptions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Value = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.CategoryBooks",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Book_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CategoryBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ReturnBooks", "BorrowBookId", "dbo.BorrowBooks");
            DropForeignKey("dbo.LatePayments", "ReturnBookId", "dbo.ReturnBooks");
            DropForeignKey("dbo.ReserveBooks", "MemberId", "dbo.Members");
            DropForeignKey("dbo.BorrowBooks", "ReserveBookId", "dbo.ReserveBooks");
            DropForeignKey("dbo.ReserveBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Members", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowBooks", "MemberId", "dbo.Members");
            DropForeignKey("dbo.BorrowBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.CategoryBooks", new[] { "Book_Id" });
            DropIndex("dbo.CategoryBooks", new[] { "Category_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LatePayments", new[] { "ReturnBookId" });
            DropIndex("dbo.ReturnBooks", new[] { "BorrowBookId" });
            DropIndex("dbo.ReserveBooks", new[] { "BookId" });
            DropIndex("dbo.ReserveBooks", new[] { "MemberId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Members", new[] { "UserId" });
            DropIndex("dbo.Members", new[] { "MobileNumber" });
            DropIndex("dbo.Members", new[] { "RefNumber" });
            DropIndex("dbo.BorrowBooks", new[] { "ReserveBookId" });
            DropIndex("dbo.BorrowBooks", new[] { "MemberId" });
            DropIndex("dbo.BorrowBooks", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "ISBN" });
            DropTable("dbo.CategoryBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.SystemOptions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.LatePayments");
            DropTable("dbo.ReturnBooks");
            DropTable("dbo.ReserveBooks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Members");
            DropTable("dbo.BorrowBooks");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
