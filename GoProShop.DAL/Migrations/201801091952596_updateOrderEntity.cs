namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryType", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "DeliveryType");
        }
    }
}
