using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CP.API.Dto
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "Category Name is required ")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Category Name must Not Contains Number !!")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [RegularExpression(" /^[^0-9]+$/", ErrorMessage = "Description must Not Contains Number !!")]
        public string Description { get; set; }
    }
}
