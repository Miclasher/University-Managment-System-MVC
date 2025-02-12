using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;

namespace University.UI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IViewDataService _viewDataService;

        public GroupController(IGroupService groupService, IViewDataService viewDataService)
        {
            _groupService = groupService;
            _viewDataService = viewDataService;
        }

        public async Task<IActionResult> IndexAsync(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            if (await _groupService.CanBeCreatedAsync())
            {
                ViewData["CanBeCreated"] = true;
            }

            var Groups = await _groupService.GetAllAsync();

            return View(Groups);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await LoadDataToViewModel();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GroupToCreateDTO Group)
        {
            await _groupService.CreateAsync(Group);

            await LoadDataToViewModel();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var Group = await _groupService.GetByIdAsync(id);

            await LoadDataToViewModel();

            return View(Group.Adapt<GroupToUpdateDTO>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(GroupToUpdateDTO Group)
        {
            await LoadDataToViewModel();

            await _groupService.UpdateAsync(Group);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Group = await _groupService.GetByIdAsync(id);

            await LoadDataToViewModel();

            return View(Group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(GroupDTO GroupToDelete)
        {
            await LoadDataToViewModel();

            await _groupService.DeleteAsync(GroupToDelete.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearGroupAsync(Guid id)
        {
            await _groupService.ClearGroupAsync(id);

            return RedirectToAction("Index");
        }

        private async Task LoadDataToViewModel()
        {
            //await _viewDataService.LoadViewDataForGroups(ViewData);

            //ViewBag.Courses = ViewData["Courses"];
            //ViewBag.Teachers = ViewData["Teachers"];
        }
    }
}
