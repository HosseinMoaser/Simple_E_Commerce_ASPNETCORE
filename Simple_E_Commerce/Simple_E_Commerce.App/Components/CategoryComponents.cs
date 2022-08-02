using Microsoft.AspNetCore.Mvc;
using Simple_E_Commerce.App.Models;
using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Repositories;

namespace Simple_E_Commerce.App.Components
{
    public class CategoryComponents : ViewComponent
    {
        private IGroupRepository _groupRepository;

        public CategoryComponents(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _groupRepository.GetShowGroupsViewModels();
            return View("/Views/Components/CategoryComponents.cshtml",categories);
        }
    }
}
