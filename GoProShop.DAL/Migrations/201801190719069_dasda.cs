namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dasda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoredProducts", "Endings", c => c.Int());
            DropColumn("dbo.StoredProducts", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoredProducts", "Quantity", c => c.Int());
            DropColumn("dbo.StoredProducts", "Endings");
        }
    }
}
