using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnouncApp.Domain
{
    public partial class Announcement
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }    
        [Required]
        [MinLength(5, ErrorMessage = "Duyuru başlığınız en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Duyuru başlığınız en fazla 50 karater olabilir.")]
        public string Title { get; set; }
        [Required]
        [MinLength(25, ErrorMessage = "Duyuru içeriğiniz en az 25 karakter olabilir.")]
        public string Contents { get; set; }
        public List<Like> Likes { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<Recommendation> Recommendations { get; set; }
    }
}
