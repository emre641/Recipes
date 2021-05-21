using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class FoodImageManager : IFoodImageService
    {
        IFoodImageDal _foodImageDal;

        public FoodImageManager(IFoodImageDal foodImageDal)
        {
            _foodImageDal = foodImageDal;
        }


        [CacheRemoveAspect("IFoodImageService.Get")]
        [SecuredOperation("foodImages.add,admin")]
        public IResult Add(IFormFile file, FoodImage foodImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(foodImage.FoodId));
            if (result != null)
            {
                return result;
            }

            foodImage.ImagePath = FileHelper.Add(file);
            foodImage.Date = DateTime.Now;
            _foodImageDal.Add(foodImage);
            return new SuccessResult();
        }



        [CacheRemoveAspect("IFoodImageService.Get")]
        [SecuredOperation("foodImages.delete,admin")]
        public IResult Delete(FoodImage foodImage)
        {
            FileHelper.Delete(foodImage.ImagePath);
            _foodImageDal.Delete(foodImage);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<FoodImage> Get(int id)
        {
            return new SuccessDataResult<FoodImage>(_foodImageDal.Get(f => f.FoodId == id));
        }

        [CacheAspect]
        public IDataResult<List<FoodImage>> GetAll()
        {
            return new SuccessDataResult<List<FoodImage>>(_foodImageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<FoodImage>> GetImageByFoodId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfFoodImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<FoodImage>>(result.Message);
            }

            return new SuccessDataResult<List<FoodImage>>(CheckIfFoodImageNull(id).Data);
        }


        [CacheRemoveAspect("IFoodImageService.Get")]
        [SecuredOperation("foodImages.update,admin")]
        public IResult Update(IFormFile file, FoodImage foodImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(foodImage.FoodId));
            if (result != null)
            {
                return result;
            }

            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _foodImageDal.Get(f => f.FoodId == foodImage.FoodId).ImagePath;

            foodImage.ImagePath = FileHelper.Update(oldPath, file);
            foodImage.Date = DateTime.Now;
            _foodImageDal.Update(foodImage);
            return new SuccessResult();
        }



        private IResult CheckImageLimitExceeded(int foodId)
        {
            var carImageCount = _foodImageDal.GetAll(f => f.FoodId == foodId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult(Messages.FoodImageLimitExceeded);
            }

            return new SuccessResult();
        }

        [CacheAspect]
        private IDataResult<List<FoodImage>> CheckIfFoodImageNull(int id)
        {
            try
            {
                string path = @"\uploads\default.jpg";
                var result = _foodImageDal.GetAll(f => f.FoodId == id).Any();
                if (!result)
                {
                    List<FoodImage> foodImage = new List<FoodImage>();
                    foodImage.Add(new FoodImage { FoodId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<FoodImage>>(foodImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<FoodImage>>(exception.Message);
            }
            return new SuccessDataResult<List<FoodImage>>(_foodImageDal.GetAll(f => f.FoodId == id));
        }
    }
}
