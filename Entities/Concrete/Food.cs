using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Food : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string FoodName { get; set; }
        public int Slug { get; set; }


    }
}
