namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedFeedbackEntity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "ProductId", c => c.Int());
            CreateIndex("dbo.Feedbacks", "ProductId");
            AddForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "ProductId", "dbo.Products");
            DropIndex("dbo.Feedbacks", new[] { "ProductId" });
            DropColumn("dbo.Feedbacks", "ProductId");
        }
    }
}
