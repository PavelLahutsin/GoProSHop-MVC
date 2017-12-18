namespace GoProShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedbacks");
        }
    }
}
