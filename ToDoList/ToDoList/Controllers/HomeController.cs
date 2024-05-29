using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Factory;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class HomeController(RepositoryFactory repositoryFactory) : Controller
    {
        private readonly RepositoryFactory _repositories = repositoryFactory;
        public async Task<IActionResult> Index()
        {
            var tasksRepository = _repositories.GetRepository<Tasks>();
            var categoriesRepository = _repositories.GetRepository<Category>();

            var viewModel = new IndexViewModel
            {
                TasksList = [.. await tasksRepository.GetAllAsync()],
                Categories = [.. await categoriesRepository.GetAllAsync()],
                StorageTypeSelected = _repositories.GetStorageType()
            };
            viewModel.SortedTasksList();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTask(int id)
        {
            await _repositories.GetRepository<Tasks>().DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTaskStatus(Tasks task)
        {
            await _repositories.GetRepository<Tasks>().UpdateAsync(task);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(Tasks task)
        {
            await _repositories.GetRepository<Tasks>().AddAsync(task);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _repositories.GetRepository<Category>().AddAsync(category);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangeStorageType(StorageType storageType)
        {
            _repositories.SetStorageType(storageType);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
