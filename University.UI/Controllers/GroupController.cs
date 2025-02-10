using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

namespace University.UI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public GroupController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            if (await _serviceManager.GroupService.CanBeCreatedAsync())
            {
                ViewData["CanBeCreated"] = true;
            }

            var Groups = await _serviceManager.GroupService.GetAllAsync();

            return View(Groups);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadViewBagAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GroupToCreateDTO Group)
        {
            await _serviceManager.GroupService.CreateAsync(Group);

            await LoadViewBagAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Group = await _serviceManager.GroupService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Group.Adapt<GroupToUpdateDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(GroupToUpdateDTO Group)
        {
            await LoadViewBagAsync();

            await _serviceManager.GroupService.UpdateAsync(Group);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Group = await _serviceManager.GroupService.GetByIdAsync(id);

            await LoadViewBagAsync();

            return View(Group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(GroupDTO GroupToDelete)
        {
            await LoadViewBagAsync();

            await _serviceManager.GroupService.DeleteAsync(GroupToDelete.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearGroupAsync(Guid id)
        {
            await _serviceManager.GroupService.ClearGroupAsync(id);

            return RedirectToAction("Index");
        }

        private async Task LoadViewBagAsync()
        {
            await _serviceManager.ViewDataService.LoadViewDataForGroups(ViewData);

            ViewBag.Courses = ViewData["Courses"];
            ViewBag.Teachers = ViewData["Teachers"];
        }
    }
}
