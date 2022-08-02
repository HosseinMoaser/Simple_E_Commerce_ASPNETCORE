using Simple_E_Commerce.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ShowGroupsViewModel> GetShowGroupsViewModels();
    }
}
