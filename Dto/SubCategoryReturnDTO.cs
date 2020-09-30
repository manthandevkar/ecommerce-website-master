using System.Collections.Generic;

namespace CP.API.Dto
{
    public class SubCategoryReturnDTO
    {
         public int SubCategoryID  { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }

         public ICollection<ProductReturnDTO> Products { get; set; }

    }
}