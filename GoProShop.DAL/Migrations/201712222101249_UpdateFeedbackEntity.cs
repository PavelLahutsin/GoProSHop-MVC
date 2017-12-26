namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFeedbackEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Feedbacks", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "IsApproved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Feedbacks", "Status");
        }
    }
}
