using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFoodDal : EfEntityRepositoryBase<Food, FoodDatabaseContext>, IFoodDal
    {
        public FoodDetailDto GetFoodDetails(int foodId)
        {
            using (FoodDatabaseContext context = new FoodDatabaseContext())
            {
                var result = from food in context.Foods.Where(f => f.Id == foodId)

                             
                             join category in context.Categories
                             on food.CategoryId equals category.CategoryId

                             select new FoodDetailDto()
                             {
                                 CategoryId = category.CategoryId,
                                 
                                 CategoryName = category.CategoryName,
                                 
                                 
                                 FoodId = food.Id,
                                 
                             };

                return result.SingleOrDefault();
            }
        }

        public List<FoodDetailDto> GetFoodsDetails(Expression<Func<FoodDetailDto, bool>> filter = null)
        {
            using (FoodDatabaseContext context = new FoodDatabaseContext())
            {
                var result = from food in context.Foods

                             

                             join category in context.Categories
                             on food.CategoryId equals category.CategoryId

                             select new FoodDetailDto()
                             {
                                 FoodId = food.Id,
                                 
                                 CategoryId = category.CategoryId,
                                 CategoryName = category.CategoryName,
                                 
                                 
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
