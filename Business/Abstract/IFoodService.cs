using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFoodService
    {
        IResult Add(Food food);
        IResult Delete(Food food);
        IResult Update(Food food);

        IDataResult<List<Food>> GetAll();
        IDataResult<Food> GetById(int id);

        IDataResult<List<Food>> GetFoodsByCategoryId(int categoryId);

        IDataResult<List<FoodDetailDto>> GetFoodDetails();
    }
}
