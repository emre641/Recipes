using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFoodImageService
    {
        IResult Add(IFormFile file, FoodImage foodImage);
        IResult Update(IFormFile file, FoodImage foodImage);
        IResult Delete(FoodImage foodImage);

        IDataResult<FoodImage> Get(int id);
        IDataResult<List<FoodImage>> GetAll();
        IDataResult<List<FoodImage>> GetImageByFoodId(int id);
    }
}
