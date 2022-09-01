using System.ComponentModel;

namespace BlogProject.Enums
{
    public enum ModerationType
    {
        [Description("Offensive language")]
        Language,
        [Description("Illegal drugs and other substances")]
        IllegalSubstances,
        [Description("Racism, xenophobia, homophobia, etc.")]
        HateSpeech,
        [Description("Sexual content")]
        Sexual,
        [Description("Harrasment")]
        Harassment,
        [Description("Spreading false information")]
        FalseInformation,
        [Description("Everything that doesn't include other categories")]
        Other

    }
}
