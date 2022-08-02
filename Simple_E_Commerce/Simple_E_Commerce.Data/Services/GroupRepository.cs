using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Models;
using Simple_E_Commerce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Services
{
    public class GroupRepository : IGroupRepository
    {
        private SimpleEcommerceDbContext _simpleEcommerceDbContext;

        public GroupRepository(SimpleEcommerceDbContext simpleEcommerceDbContext)
        {
            _simpleEcommerceDbContext = simpleEcommerceDbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _simpleEcommerceDbContext.Categories.ToList();
        }

        public IEnumerable<ShowGroupsViewModel> GetShowGroupsViewModels()
        {
            return _simpleEcommerceDbContext.Categories
                .Select(c => new ShowGroupsViewModel()
                {
                    GroupId = c.Id,
                    GroupName = c.CategoryName,
                    ProductsCount = _simpleEcommerceDbContext.CategoryToProducts.Count(group => group.CategoryId == c.Id)
                }).ToList();
        }
    }
}
