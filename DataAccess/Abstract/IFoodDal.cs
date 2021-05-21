using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IFoodDal : IEntityRepository<Food>
    {
        List<FoodDetailDto> GetFoodsDetails(Expression<Func<FoodDetailDto, bool>> filter = null);
        FoodDetailDto GetFoodDetails(int foodId);

    }
}
