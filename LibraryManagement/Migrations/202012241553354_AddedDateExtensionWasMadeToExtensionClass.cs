namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateExtensionWasMadeToExtensionClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Extensions", "DateExtensionWasMade", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Extensions", "DateExtensionWasMade");
        }
    }
}
