using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogProject.Enums
{
    public enum ModerationType
    {
        [Display(Name = "Offensive language")]
        Language,
        [Display(Name = "Illegal drugs and other substances")]
        IllegalSubstances,
        [Display(Name = "Hate speech")]
        HateSpeech,
        [Display(Name = "Sexual content")]
        Sexual,
        [Display(Name = "Harrasment")]
        Harassment,
        [Display(Name = "Spreading false information")]
        FalseInformation,
        Other

    }
}
