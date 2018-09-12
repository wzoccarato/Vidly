using System.Data.Entity.Core.Metadata.Edm;

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MEMBERSHIPTYPES SET Name='Pay as you go' WHERE DurationInMonths=0");
            Sql("UPDATE MEMBERSHIPTYPES SET Name='Montly' WHERE DurationInMonths=1");
            Sql("UPDATE MEMBERSHIPTYPES SET Name='Quarterly' WHERE DurationInMonths=3");
            Sql("UPDATE MEMBERSHIPTYPES SET Name='Yearly' WHERE DurationInMonths=12");
        }
        
        public override void Down()
        {
        }
    }
}
