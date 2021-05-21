using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    class FoodManager : IFoodService
    {
        IFoodDal _foodDal;
        ICategoryDal _categoryDal;

        public FoodManager(IFoodDal foodDal, ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
            _foodDal = foodDal;
        }

        [SecuredOperation("food.add,admin")]
        [ValidationAspect(typeof(FoodValidator))]
        [CacheRemoveAspect("IFoodService.Get")]
        public IResult Add(Food food)
        {
            _foodDal.Add(food);

            return new SuccessResult(Messages.FoodAdded);
        }

        [SecuredOperation("food.delete,admin")]
        [CacheRemoveAspect("IFoodService.Get")]
        public IResult Delete(Food food)
        {
            _foodDal.Delete(food);

            return new SuccessResult(Messages.FoodDeleted);
        }

        public IDataResult<List<Food>> GetAll()
        {
            if (DateTime.Now.Hour == 4)
            {
                return new ErrorDataResult<List<Food>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Food>>(_foodDal.GetAll(), Messages.FoodsListed);
        }

        public IDataResult<Food> GetById(int id)
        {
            return new SuccessDataResult<Food>(_foodDal.Get(f => f.Id == id));
        }

        public IDataResult<List<FoodDetailDto>> GetFoodDetails()
        {
            if (DateTime.Now.Hour == 4)
            {
                return new ErrorDataResult<List<FoodDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<FoodDetailDto>>(_foodDal.GetFoodsDetails());
        }

        public IDataResult<List<Food>> GetFoodsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Food>>(_foodDal.GetAll(f => f.CategoryId == categoryId));
        }

        [SecuredOperation("food.update,admin")]
        [ValidationAspect(typeof(FoodValidator))]
        [CacheRemoveAspect("IFoodService.Get")]
        public IResult Update(Food food)
        {
            _foodDal.Update(food);

            return new SuccessResult(Messages.FoodUpdated);
        }
    }
}
