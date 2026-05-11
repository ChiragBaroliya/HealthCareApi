using System.ComponentModel;

namespace HealthCare.Domain.Enums
{
    public enum BloodGroup
    {
        [Description("A+")]
        APositive,
        [Description("A-")]
        ANegative,
        [Description("B+")]
        BPositive,
        [Description("B-")]
        BNegative,
        [Description("AB+")]
        ABPositive,
        [Description("AB-")]
        ABNegative,
        [Description("O+")]
        OPositive,
        [Description("O-")]
        ONegative
    }
}
