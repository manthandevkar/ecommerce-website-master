using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.API.Model
{
    public class SubCategory
    {
        [Key]
        public int subCategoryID  { get; set; }

        public string subCategoryName { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public ICollection<Product> Products { get; set; }


       
    }
}