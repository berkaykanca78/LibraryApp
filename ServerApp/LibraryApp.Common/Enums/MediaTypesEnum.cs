using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Common.Enums
{
    public enum MediaTypesEnum
    {
        [Display(Name = "Belirtilmemiş")]
        Belirtilmemis = 0,

        [Display(Name = "İnce Kapak")]
        InceKapak = 1,

        [Display(Name = "Ciltli")]
        Ciltli = 2,

        [Display(Name = "Cep Boy")]
        CepBoy = 3,
    }
}
