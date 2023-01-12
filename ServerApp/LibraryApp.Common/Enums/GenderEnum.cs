using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Common.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Erkek")]
        Erkek = 0,

        [Display(Name = "Kadın")]
        Kadin = 1,
    }
}
