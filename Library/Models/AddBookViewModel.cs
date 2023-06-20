using System.ComponentModel.DataAnnotations;
using static Library.Common.EntityValidations.BookValidations;

namespace Library.Models
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(TitleMaxLenght, MinimumLength = TitleMinLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorMaxLenght,MinimumLength =AuthorMinLenght)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLenght, MinimumLength = ImageUrlMinLenght)]
        public string Url { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public string Rating { get; set; } = null!;

        public int CategoryId { get; set; }

        public ICollection<CategoryAddBookViewModel> Categories { get; set; }=new HashSet<CategoryAddBookViewModel>();
    }
}
