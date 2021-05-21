using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class FoodDetailDto : IDto
    {
        public int FoodId { get; set; }
        public int CategoryId { get; set; }        
        public string FoodName { get; set; }
        public string CategoryName { get; set; }
        public int Slug { get; set; }

        
    }
}
