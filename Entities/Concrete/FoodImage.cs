using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class FoodImage : IEntity
    {
        public int ImageId { get; set; }
        public int FoodId { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Date { get; set; }
    }
}
