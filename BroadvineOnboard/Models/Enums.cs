namespace BroadvineOnboard.Models
{
    public class Enums
    {
        public enum PropertyServiceType
        {
            Unknown = 0,
            Full = 1,
            Limited = 2,
            Economy = 3
        }

        public enum PropertyStatus
        {
            Unknown = 0,
            Closed = 1,
            Inactive = 2,
            UnderDevelopment = 3,
            InOperation = 4
        }

        public enum PropertyRelationship
        {
            Unknown = 0,
            Operated = 1,
            Owned = 2
        }

        public enum PropertyMaturity
        {
            Unknown = 0,
            Mature = 1,
            Rampup = 2
        }
        public enum PropertyCalendar
        {
            Unknown = 0,
            Calendar = 1,
            Fiscal = 2
        }
    }
}