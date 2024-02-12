using System.ComponentModel.DataAnnotations;

namespace MerchProjectModels
{

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Category Name")]
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        //public DateTime? CreatedDate { get; set; }
    }
}
