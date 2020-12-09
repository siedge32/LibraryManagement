namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Adress = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookPublications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                        CoverMaterial = c.String(),
                        Book_Id = c.Int(),
                        PublishingHouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.BookStocks", t => t.Id)
                .ForeignKey("dbo.PublishingHouses", t => t.PublishingHouse_Id)
                .Index(t => t.Id)
                .Index(t => t.Book_Id)
                .Index(t => t.PublishingHouse_Id);
            
            CreateTable(
                "dbo.BookStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfBooks = c.Int(nullable: false),
                        NumberOfBooksForLecture = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookWithdrawals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateToReturn = c.DateTime(nullable: false),
                        Librarian_Id = c.Int(),
                        Reader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Librarians", t => t.Librarian_Id)
                .ForeignKey("dbo.Readers", t => t.Reader_Id)
                .Index(t => t.Librarian_Id)
                .Index(t => t.Reader_Id);
            
            CreateTable(
                "dbo.Extensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateToReturn = c.DateTime(nullable: false),
                        BookWithdrawal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookWithdrawals", t => t.BookWithdrawal_Id)
                .Index(t => t.BookWithdrawal_Id);
            
            CreateTable(
                "dbo.Librarians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublishingHouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentField_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fields", t => t.ParentField_Id)
                .Index(t => t.ParentField_Id);
            
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
                "dbo.BookWithdrawalBookPublications",
                c => new
                    {
                        BookWithdrawal_Id = c.Int(nullable: false),
                        BookPublication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookWithdrawal_Id, t.BookPublication_Id })
                .ForeignKey("dbo.BookWithdrawals", t => t.BookWithdrawal_Id, cascadeDelete: true)
                .ForeignKey("dbo.BookPublications", t => t.BookPublication_Id, cascadeDelete: true)
                .Index(t => t.BookWithdrawal_Id)
                .Index(t => t.BookPublication_Id);
            
            CreateTable(
                "dbo.FieldBooks",
                c => new
                    {
                        Field_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Field_Id, t.Book_Id })
                .ForeignKey("dbo.Fields", t => t.Field_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Field_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fields", "ParentField_Id", "dbo.Fields");
            DropForeignKey("dbo.FieldBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.FieldBooks", "Field_Id", "dbo.Fields");
            DropForeignKey("dbo.BookPublications", "PublishingHouse_Id", "dbo.PublishingHouses");
            DropForeignKey("dbo.BookWithdrawals", "Reader_Id", "dbo.Readers");
            DropForeignKey("dbo.BookWithdrawals", "Librarian_Id", "dbo.Librarians");
            DropForeignKey("dbo.Extensions", "BookWithdrawal_Id", "dbo.BookWithdrawals");
            DropForeignKey("dbo.BookWithdrawalBookPublications", "BookPublication_Id", "dbo.BookPublications");
            DropForeignKey("dbo.BookWithdrawalBookPublications", "BookWithdrawal_Id", "dbo.BookWithdrawals");
            DropForeignKey("dbo.BookPublications", "Id", "dbo.BookStocks");
            DropForeignKey("dbo.BookPublications", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.FieldBooks", new[] { "Book_Id" });
            DropIndex("dbo.FieldBooks", new[] { "Field_Id" });
            DropIndex("dbo.BookWithdrawalBookPublications", new[] { "BookPublication_Id" });
            DropIndex("dbo.BookWithdrawalBookPublications", new[] { "BookWithdrawal_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.Fields", new[] { "ParentField_Id" });
            DropIndex("dbo.Extensions", new[] { "BookWithdrawal_Id" });
            DropIndex("dbo.BookWithdrawals", new[] { "Reader_Id" });
            DropIndex("dbo.BookWithdrawals", new[] { "Librarian_Id" });
            DropIndex("dbo.BookPublications", new[] { "PublishingHouse_Id" });
            DropIndex("dbo.BookPublications", new[] { "Book_Id" });
            DropIndex("dbo.BookPublications", new[] { "Id" });
            DropTable("dbo.FieldBooks");
            DropTable("dbo.BookWithdrawalBookPublications");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Fields");
            DropTable("dbo.PublishingHouses");
            DropTable("dbo.Readers");
            DropTable("dbo.Librarians");
            DropTable("dbo.Extensions");
            DropTable("dbo.BookWithdrawals");
            DropTable("dbo.BookStocks");
            DropTable("dbo.BookPublications");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
