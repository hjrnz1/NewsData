namespace DataJob.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sentiments",
                c => new
                    {
                        sid = c.Int(nullable: false, identity: true),
                        mixed = c.String(),
                        score = c.Single(nullable: false),
                        type = c.String(),
                        docid = c.String(),
                        timestamp = c.Int(nullable: false),
                        url = c.String(),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.sid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sentiments");
        }
    }
}
