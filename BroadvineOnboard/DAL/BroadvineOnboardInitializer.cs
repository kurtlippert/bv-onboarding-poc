using System.Collections.Generic;
using BroadvineOnboard.Models;

namespace BroadvineOnboard.DAL
{
    public class BroadvineOnboardInitializer : System.Data.Entity.DropCreateDatabaseAlways<BroadvineOnboardContext> //CreateDatabaseIfNotExists<BroadvineOnboardContext> //
    {
        protected override void Seed(BroadvineOnboardContext context)
        {
            //  Seed the account types 
            var accountTypes = new List<AccountType>
            {
                new AccountType { AccountTypeID = 1, Name = "Asset"},
                new AccountType { AccountTypeID = 2, Name = "Liability"},
                new AccountType { AccountTypeID = 3, Name = "Equity"},
                new AccountType { AccountTypeID = 4, Name = "Retained Earnings"},
                new AccountType { AccountTypeID = 5, Name = "Revenue"},
                new AccountType { AccountTypeID = 6, Name = "Expense"},
                new AccountType { AccountTypeID = 7, Name = "Suspense"},
                new AccountType { AccountTypeID = 8, Name = "Statistical"},
                new AccountType { AccountTypeID = 9, Name = "Unspecified"}
            };
            accountTypes.ForEach(a => context.AccountTypes.Add(a));
            context.SaveChanges();

            //  Seed the account sub types 
            var accountSubTypes = new List<AccountSubType>
            {
                new AccountSubType { AccountSubTypeID = 1, Name = "Current Assets" },
                new AccountSubType { AccountSubTypeID = 2, Name = "Other Assets" },
                new AccountSubType { AccountSubTypeID = 3, Name = "Property and Equipment" },
                new AccountSubType { AccountSubTypeID = 4, Name = "Investments" },
                new AccountSubType { AccountSubTypeID = 5, Name = "Current Liabilities" },
                new AccountSubType { AccountSubTypeID = 6, Name = "Other Liabilities" },
                new AccountSubType { AccountSubTypeID = 7, Name = "Long Term Debt" },
                new AccountSubType { AccountSubTypeID = 8, Name = "Owner's Equity" },
                new AccountSubType { AccountSubTypeID = 9, Name = "Equity" },
                new AccountSubType { AccountSubTypeID = 10, Name = "Retained Earnings" },
                new AccountSubType { AccountSubTypeID = 11, Name = "Revenue" },
                new AccountSubType { AccountSubTypeID = 12, Name = "Mgmt Fees" },
                new AccountSubType { AccountSubTypeID = 13, Name = "Fixed" },
                new AccountSubType { AccountSubTypeID = 14, Name = "Payroll" },
                new AccountSubType { AccountSubTypeID = 15, Name = "Cost of Sales" },
                new AccountSubType { AccountSubTypeID = 16, Name = "Operating" },
                new AccountSubType { AccountSubTypeID = 17, Name = "Other Inc/Expense" },
                new AccountSubType { AccountSubTypeID = 18, Name = "Suspense" },
                new AccountSubType { AccountSubTypeID = 19, Name = "Rooms Rented (Units)" },
                new AccountSubType { AccountSubTypeID = 20, Name = "Rooms Occupied (Units)" },
                new AccountSubType { AccountSubTypeID = 21, Name = "Rooms Available (Units)" },
                new AccountSubType { AccountSubTypeID = 22, Name = "Rooms OOO (Units)" },
                new AccountSubType { AccountSubTypeID = 23, Name = "Rooms Physical (Units)" },
                new AccountSubType { AccountSubTypeID = 24, Name = "Covers (Units)" },
                new AccountSubType { AccountSubTypeID = 25, Name = "Rooms ReRented (Units)" },
                new AccountSubType { AccountSubTypeID = 26, Name = "Vacant Rooms (Units)" },
                new AccountSubType { AccountSubTypeID = 27, Name = "Number of Guests" },
                new AccountSubType { AccountSubTypeID = 28, Name = "No Show (Units)" },
                new AccountSubType { AccountSubTypeID = 29, Name = "Rooms Comped (Units)" },
                new AccountSubType { AccountSubTypeID = 30, Name = "General" },
                new AccountSubType { AccountSubTypeID = 31, Name = "Rooms Guaranteed (Units)" },
                new AccountSubType { AccountSubTypeID = 32, Name = "Rooms (Other)" },
                new AccountSubType { AccountSubTypeID = 33, Name = "Labor (Units)" }
            };
            accountSubTypes.ForEach(a => context.AccountSubTypes.Add(a));
            context.SaveChanges();

            //  Seed the driver types 
            var driverTypes = new List<DriverType>
            {
                new DriverType { DriverTypeID = 1, Name = "None", Description = "None" },
                new DriverType { DriverTypeID = 2, Name = "POR", Description = "Amount Per Rented Room" },
                new DriverType { DriverTypeID = 3, Name = "POR + COMP", Description = "Amount Per Occupied Room" },
                new DriverType { DriverTypeID = 4, Name = "PAR", Description = "Amount Per Available Room" },
                new DriverType { DriverTypeID = 5, Name = "Daily", Description = "Amount Per Day" },
                new DriverType { DriverTypeID = 6, Name = "Monthly (Default)", Description = "Amount Per Month" },
                new DriverType { DriverTypeID = 7, Name = "Quarterly", Description = "Amount Per Quarter" },
                new DriverType { DriverTypeID = 8, Name = "Annual", Description = "Amount Per Year" },
                new DriverType { DriverTypeID = 9, Name = "% of Account", Description = "Percent of Specific Account" },
                new DriverType { DriverTypeID = 10, Name = "% Diff Dept Rev", Description = "Percent of Different Departments Revenue" },
                new DriverType { DriverTypeID = 11, Name = "% TTL Rev", Description = "Percent of Total Revenue" },
                new DriverType { DriverTypeID = 12, Name = "Work Day Amt", Description = "Amount / Relative Work Days" },
                new DriverType { DriverTypeID = 13, Name = "Same Month LY", Description = "Same Month as Last Year" },
                new DriverType { DriverTypeID = 14, Name = "Trail 1", Description = "Avg of Trailing 1 Month" },
                new DriverType { DriverTypeID = 15, Name = "Trail 2", Description = "Avg of Trailing 2 Month" },
                new DriverType { DriverTypeID = 16, Name = "Trail 3", Description = "Avg of Trailing 3 Month" },
                new DriverType { DriverTypeID = 17, Name = "Trail 4", Description = "Avg of Trailing 4 Month" },
                new DriverType { DriverTypeID = 18, Name = "Trail 5", Description = "Avg of Trailing 5 Month" },
                new DriverType { DriverTypeID = 19, Name = "Trail 6", Description = "Avg of Trailing 6 Month" },
                new DriverType { DriverTypeID = 20, Name = "Trail 7", Description = "Avg of Trailing 7 Month" },
                new DriverType { DriverTypeID = 21, Name = "Trail 8", Description = "Avg of Trailing 8 Month" },
                new DriverType { DriverTypeID = 22, Name = "Trail 9", Description = "Avg of Trailing 9 Month" },
                new DriverType { DriverTypeID = 23, Name = "Trail 10", Description = "Avg of Trailing 10 Month" },
                new DriverType { DriverTypeID = 24, Name = "Trail 11", Description = "Avg of Trailing 11 Month" },
                new DriverType { DriverTypeID = 25, Name = "Trail 12", Description = "Avg of Trailing 12 Month" },
                new DriverType { DriverTypeID = 26, Name = "% Same Dept Wages", Description = "Percent of Same Department Wages" },
                new DriverType { DriverTypeID = 27, Name = "% TTL Wages", Description = "Percent of Total Wages" },
                new DriverType { DriverTypeID = 28, Name = "% Same Dept Rev", Description = "Percent of Same Department Revenues" },
                new DriverType { DriverTypeID = 29, Name = "Salary (Week Days)", Description = "Annual Salary by work days in month" },
                new DriverType { DriverTypeID = 30, Name = "FTE Salary", Description = "Full Time Equivalent Salary" },
                new DriverType { DriverTypeID = 31, Name = "Salary (All Days)", Description = "Annual Salary by total days in month" },
                new DriverType { DriverTypeID = 32, Name = "Hourly Rate", Description = "Hourly Rate X Monthly Hours" },
                new DriverType { DriverTypeID = 33, Name = "Mins/Room", Description = "Minutes/Room" },
                new DriverType { DriverTypeID = 34, Name = "Hrs/Day", Description = "Hours/Day" },
                new DriverType { DriverTypeID = 35, Name = "% Non-Othr Dept Rev", Description = "% Dept Revenue (other income excluded)" },
                new DriverType { DriverTypeID = 36, Name = "% of Mult Accts", Description = "Percent of Multiple Accounts" },
                new DriverType { DriverTypeID = 37, Name = "% of Diff Dept Wages", Description = "Another Departments Wages" },
                new DriverType { DriverTypeID = 38, Name = "Rebill as % TTL Wages", Description = "Rebill department amount to selected department as department's % of total wages" },
                new DriverType { DriverTypeID = 39, Name = "Account Reversal", Description = "Reverse/Inverse selected department and account" },
                new DriverType { DriverTypeID = 40, Name = "Rebill Total Amt", Description = "Rebill department amount to selected department" },
                new DriverType { DriverTypeID = 41, Name = "Mins/Room (ACT)", Description = "Minutes per Room Occupied (Actuals)" },
                new DriverType { DriverTypeID = 42, Name = "Hrs/Day (ACT)", Description = "Hours Per Day (Actuals)" },
                new DriverType { DriverTypeID = 43, Name = "Avg Spend * Covers", Description = "Avg Spend * Associated Covers" },
                new DriverType { DriverTypeID = 44, Name = "% Rooms Rented", Description = "Percent of Rooms Rented" },
                new DriverType { DriverTypeID = 45, Name = "Cost Per Cover", Description = "Cost Per Total Covers" },
                new DriverType { DriverTypeID = 46, Name = "Amount per Unit", Description = "Amount per Selected Unit Account" },
                new DriverType { DriverTypeID = 47, Name = "Amount per Bed Night Sold", Description = "Amount per Bed Night Sold" },
                new DriverType { DriverTypeID = 48, Name = "Comps", Description = "Comp Rooms" },
                new DriverType { DriverTypeID = 49, Name = "GreaterOf_%GOP_%TTLRev", Description = "Greater of X% Total Rev or N% GOP" },
                new DriverType { DriverTypeID = 50, Name = "% Segmented Rm Rev", Description = "% of Segmented Room Revenues Only" },
                new DriverType { DriverTypeID = 51, Name = "POR (Segmented Rms Only)", Description = "Per Segmented Occupied Room" },
                new DriverType { DriverTypeID = 52, Name = "STAFF Meals", Description = "STAFF Meals" },
                new DriverType { DriverTypeID = 53, Name = "RebillAsPctOfSelectedAcct", Description = "Rebill as % of selected deptartment, account..." },
                new DriverType { DriverTypeID = 54, Name = "% of NOI", Description = "Percent of Net Operating Income (NOI)" },
                new DriverType { DriverTypeID = 55, Name = "Worksheet", Description = "Workheet" },
                new DriverType { DriverTypeID = 56, Name = "Salary WorkSheet", Description = "Salary Worksheet" },
                new DriverType { DriverTypeID = 57, Name = "GL Actual", Description = "Actual Amount (GL)" },
                new DriverType { DriverTypeID = 58, Name = "% Bed Nights Sold", Description = "% of Bed Nights Rented" },
                new DriverType { DriverTypeID = 59, Name = "% Multi-Dept Revs", Description = "% of Multiple Departments Revenues" },
                new DriverType { DriverTypeID = 60, Name = "% Multi-Dept Wages", Description = "% of Multiple Departments Wages" },
                new DriverType { DriverTypeID = 61, Name = "% Bnqt Rev", Description = "% Banquet Sales" },
                new DriverType { DriverTypeID = 62, Name = "% Mtg Rm Rev", Description = "% Meeting Room Revenue" },
                new DriverType { DriverTypeID = 63, Name = "% MovieRev", Description = "% Movie Revenue" },
                new DriverType { DriverTypeID = 64, Name = "% PantryRev", Description = "% Pavilion Pantry Revenue" },
                new DriverType { DriverTypeID = 65, Name = "% GstValetRev", Description = "% Guest Valet/Cleaning Revenue" },
                new DriverType { DriverTypeID = 66, Name = "POR + COMP", Description = "Amount Per Occupied Room " }
            };
            driverTypes.ForEach(d => context.DriverTypes.Add(d));
            context.SaveChanges();

            ////  Seed department groups
            //var departmentGroups = new List<DepartmentGroup>
            //{
            //    new DepartmentGroup { DepartmentGroupID = 1, DepartmentGroupCode = "010", GroupName = "Rooms", ShortName = "Rooms" },
            //    new DepartmentGroup { DepartmentGroupID = 2, DepartmentGroupCode = "020", GroupName = "Food & Beverage", ShortName = "F&B" },
            //    new DepartmentGroup { DepartmentGroupID = 3, DepartmentGroupCode = "030", GroupName = "Other", ShortName = "Other" }
            //};
            //departmentGroups.ForEach(g => context.DepartmentGroups.Add(g));
            //context.SaveChanges();

            //  Wage / PTEB Account 
            var wagePTEBAccountType = new List<WagePTEB>
            {
                new WagePTEB { WagePTEBID = 0, Name = "" },
                new WagePTEB { WagePTEBID = 1, Name = "Wage" },
                new WagePTEB { WagePTEBID = 2, Name = "PTEB" }
            };
            wagePTEBAccountType.ForEach(w => context.WagePTEBs.Add(w));
            context.SaveChanges();
        }
    }
}