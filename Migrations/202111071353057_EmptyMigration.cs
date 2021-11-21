namespace EmpResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "MobileNo", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "TechnicalSkills", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "ProjectDetails", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "RelevantExperience", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "RelevantExperience", c => c.String());
            AlterColumn("dbo.Employees", "ProjectDetails", c => c.String());
            AlterColumn("dbo.Employees", "TechnicalSkills", c => c.String());
            AlterColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "MobileNo", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
        }
    }
}
