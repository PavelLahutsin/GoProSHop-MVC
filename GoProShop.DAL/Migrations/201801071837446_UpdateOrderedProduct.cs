namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderedProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderedProducts", "StoreId", "dbo.Stores");
            DropIndex("dbo.OrderedProducts", new[] { "StoreId" });
            AlterColumn("dbo.OrderedProducts", "StoreId", c => c.Int());
            CreateIndex("dbo.OrderedProducts", "StoreId");
            AddForeignKey("dbo.OrderedProducts", "StoreId", "dbo.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedProducts", "StoreId", "dbo.Stores");
            DropIndex("dbo.OrderedProducts", new[] { "StoreId" });
            AlterColumn("dbo.OrderedProducts", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderedProducts", "StoreId");
            AddForeignKey("dbo.OrderedProducts", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
    }
}
