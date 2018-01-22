namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsViewed", c => c.Boolean(nullable: false));
            AddColumn("dbo.StoredProducts", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.StoredProducts", "Endings");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoredProducts", "Endings", c => c.Int());
            DropColumn("dbo.StoredProducts", "Quantity");
            DropColumn("dbo.Orders", "IsViewed");
        }
    }
}
