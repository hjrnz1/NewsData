namespace PriceJob.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Btc_Usd",
                c => new
                    {
                        index = c.Int(nullable: false, identity: true),
                        high = c.Single(nullable: false),
                        low = c.Single(nullable: false),
                        avg = c.Single(nullable: false),
                        vol = c.Single(nullable: false),
                        vol_cur = c.Single(nullable: false),
                        last = c.Single(nullable: false),
                        buy = c.Single(nullable: false),
                        sell = c.Single(nullable: false),
                        updated = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.index);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Btc_Usd");
        }
    }
}
