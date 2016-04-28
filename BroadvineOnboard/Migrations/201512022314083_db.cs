namespace BroadvineOnboard.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountSubType",
                c => new
                    {
                        AccountSubTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AccountSubTypeID);
            
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        AccountTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AccountTypeID);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AreaID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Guid(nullable: false),
                        ChallengeKey1 = c.Guid(nullable: false),
                        ChallengeKey2 = c.Guid(nullable: false),
                        ClientName = c.String(nullable: false),
                        UserFullName = c.String(nullable: false),
                        UserEmailAddress = c.String(nullable: false),
                        UserID = c.Guid(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.COA",
                c => new
                    {
                        COAID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Suffix = c.String(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        AccountTypeID = c.Int(nullable: false),
                        AccountSubTypeID = c.Int(nullable: false),
                        RevenueSegmentID = c.Int(nullable: false),
                        DriverTypeID = c.Int(nullable: false),
                        WagePTEBID = c.Int(nullable: false),
                        StatisticalAccountID = c.Int(),
                        StatisticalAccount_COAID = c.Int(),
                    })
                .PrimaryKey(t => t.COAID)
                .ForeignKey("dbo.AccountSubType", t => t.AccountSubTypeID, cascadeDelete: true)
                .ForeignKey("dbo.AccountType", t => t.AccountTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.DriverType", t => t.DriverTypeID, cascadeDelete: true)
                .ForeignKey("dbo.RevenueSegment", t => t.RevenueSegmentID, cascadeDelete: true)
                .ForeignKey("dbo.COA", t => t.StatisticalAccount_COAID)
                .ForeignKey("dbo.WagePTEB", t => t.WagePTEBID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.AccountTypeID)
                .Index(t => t.AccountSubTypeID)
                .Index(t => t.RevenueSegmentID)
                .Index(t => t.DriverTypeID)
                .Index(t => t.WagePTEBID)
                .Index(t => t.StatisticalAccount_COAID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        DepartmentCode = c.String(nullable: false),
                        DepartmentName = c.String(nullable: false),
                        DepartmentShortName = c.String(nullable: false),
                        SortOrder = c.String(),
                        ProfitAndLoss = c.Boolean(nullable: false),
                        ProfitCenterDistributed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DepartmentIncludeInGOPID = c.Int(nullable: false),
                        DepartmentGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.DepartmentGroup", t => t.DepartmentGroupID, cascadeDelete: true)
                .Index(t => t.DepartmentGroupID);
            
            CreateTable(
                "dbo.DepartmentGroup",
                c => new
                    {
                        DepartmentGroupID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        DepartmentGroupCode = c.String(nullable: false),
                        GroupName = c.String(nullable: false),
                        ShortName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentGroupID);
            
            CreateTable(
                "dbo.DriverType",
                c => new
                    {
                        DriverTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DriverTypeID);
            
            CreateTable(
                "dbo.RevenueSegment",
                c => new
                    {
                        RevenueSegmentID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        NameShort = c.String(nullable: false),
                        RevenueSegmentGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RevenueSegmentID)
                .ForeignKey("dbo.RevenueSegmentGroup", t => t.RevenueSegmentGroupID, cascadeDelete: true)
                .Index(t => t.RevenueSegmentGroupID);
            
            CreateTable(
                "dbo.RevenueSegmentGroup",
                c => new
                    {
                        RevenueSegmentGroupID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RevenueSegmentGroupID);
            
            CreateTable(
                "dbo.WagePTEB",
                c => new
                    {
                        WagePTEBID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.WagePTEBID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        CompanyCode = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        NameShort = c.String(nullable: false),
                        NameLegal = c.String(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        URL = c.String(),
                        IsManagementCompany = c.Boolean(nullable: false),
                        IsOwnerCompany = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        BrandCode = c.String(),
                        SmithTravelCode = c.String(),
                        TotalRooms = c.Int(nullable: false),
                        URL = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        FoodAndBeverage = c.Boolean(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        StatusType = c.Int(nullable: false),
                        RelationshipType = c.Int(nullable: false),
                        MaturityType = c.Int(nullable: false),
                        CalendarType = c.Int(nullable: false),
                        AreaID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.AreaID, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.AreaID)
                .Index(t => t.CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Property", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Property", "AreaID", "dbo.Area");
            DropForeignKey("dbo.COA", "WagePTEBID", "dbo.WagePTEB");
            DropForeignKey("dbo.COA", "StatisticalAccount_COAID", "dbo.COA");
            DropForeignKey("dbo.COA", "RevenueSegmentID", "dbo.RevenueSegment");
            DropForeignKey("dbo.RevenueSegment", "RevenueSegmentGroupID", "dbo.RevenueSegmentGroup");
            DropForeignKey("dbo.COA", "DriverTypeID", "dbo.DriverType");
            DropForeignKey("dbo.COA", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "DepartmentGroupID", "dbo.DepartmentGroup");
            DropForeignKey("dbo.COA", "AccountTypeID", "dbo.AccountType");
            DropForeignKey("dbo.COA", "AccountSubTypeID", "dbo.AccountSubType");
            DropIndex("dbo.Property", new[] { "CompanyID" });
            DropIndex("dbo.Property", new[] { "AreaID" });
            DropIndex("dbo.RevenueSegment", new[] { "RevenueSegmentGroupID" });
            DropIndex("dbo.Department", new[] { "DepartmentGroupID" });
            DropIndex("dbo.COA", new[] { "StatisticalAccount_COAID" });
            DropIndex("dbo.COA", new[] { "WagePTEBID" });
            DropIndex("dbo.COA", new[] { "DriverTypeID" });
            DropIndex("dbo.COA", new[] { "RevenueSegmentID" });
            DropIndex("dbo.COA", new[] { "AccountSubTypeID" });
            DropIndex("dbo.COA", new[] { "AccountTypeID" });
            DropIndex("dbo.COA", new[] { "DepartmentID" });
            DropTable("dbo.Property");
            DropTable("dbo.Company");
            DropTable("dbo.WagePTEB");
            DropTable("dbo.RevenueSegmentGroup");
            DropTable("dbo.RevenueSegment");
            DropTable("dbo.DriverType");
            DropTable("dbo.DepartmentGroup");
            DropTable("dbo.Department");
            DropTable("dbo.COA");
            DropTable("dbo.Client");
            DropTable("dbo.Area");
            DropTable("dbo.AccountType");
            DropTable("dbo.AccountSubType");
        }
    }
}
