using Mapster;
using Microsoft.AspNetCore.Mvc;
using University.Services.Abstractions;
using University.Shared;
using University.UI.ViewModels;

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
            var viewModel = new GroupIndexViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                viewModel.ErrorMessage = errorMessage;
            }

            viewModel.CanBeCreated = await _groupService.CanBeCreatedAsync();

            viewModel.Groups = await _groupService.GetAllAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var viewModel = new GroupCreateViewModel();

            viewModel.Courses = await _viewDataService.LoadCoursesDataForGroups();
            viewModel.Teachers = await _viewDataService.LoadTeachersDataForGroups();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GroupToCreateDTO group)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid group data" });
            }

            await _groupService.CreateAsync(group);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var viewModel = new GroupEditViewModel();

            viewModel.Group = (await _groupService.GetByIdAsync(id)).Adapt<GroupToUpdateDTO>();

            viewModel.Courses = await _viewDataService.LoadCoursesDataForGroups();
            viewModel.Teachers = await _viewDataService.LoadTeachersDataForGroups();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(GroupToUpdateDTO group)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { errorMessage = "Invalid group data" });
            }

            await _groupService.UpdateAsync(group);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var viewModel = new GroupDeleteViewModel();

            viewModel.Group = await _groupService.GetByIdAsync(id);

            viewModel.Courses = await _viewDataService.LoadCoursesDataForGroups();
            viewModel.Teachers = await _viewDataService.LoadTeachersDataForGroups();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(GroupDTO groupToDelete)
        {
            await _groupService.DeleteAsync(groupToDelete.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearGroupAsync(Guid id)
        {
            await _groupService.ClearGroupAsync(id);

            return RedirectToAction("Index");
        }
    }
}
