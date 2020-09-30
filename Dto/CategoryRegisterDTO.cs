using CP.API.Model;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class CategoryRegisterDTO
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required ")]
        [RegularExpression(" /^[^0-9]+$/",ErrorMessage = "Category Name must Not Contains Number !!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Description must Not Contains Number !!")]

        public string Description { get; set; }
    }
}  