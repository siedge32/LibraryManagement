namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateRentedBookWuthdrawal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookWithdrawals", "DateRented", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookWithdrawals", "DateRented");
        }
    }
}
