using System.ComponentModel.DataAnnotations;

namespace AnnouncApp.Resources
{
    public class AnnouncementResource
    {
        [Required]
        [MinLength(5, ErrorMessage = "Duyuru başlığınız en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Duyuru başlığınız en fazla 50 karater olabilir.")]
        public string Title { get; set; }
        [Required]
        [MinLength(25, ErrorMessage = "Duyuru içeriğiniz en az 25 karakter olabilir.")]
        public string Contents { get; set; }
    }
}
