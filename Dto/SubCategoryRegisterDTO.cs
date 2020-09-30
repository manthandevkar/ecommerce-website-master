using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class SubCategoryRegisterDTO
    {
        [Required(ErrorMessage = "Category Name is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Category Name must Not Contains Number !!")]
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}