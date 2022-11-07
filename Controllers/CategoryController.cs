using CategoryMaster.Data;
using CategoryMaster.Models;
using CategoryMaster.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CategoryMaster.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MVCCategoryDbcontex mvcCategoryDbcontex;

        public CategoryController(MVCCategoryDbcontex mvcCategoryDbcontex)
        {
            this.mvcCategoryDbcontex = mvcCategoryDbcontex;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Categorys = await mvcCategoryDbcontex.Categorys.ToListAsync();
            return View(Categorys);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel addCategoryrequest)

        {
            var category = new Category()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = addCategoryrequest.CategoryName,
                Description = addCategoryrequest.Description,
            };

            await mvcCategoryDbcontex.Categorys.AddAsync(category);
            await mvcCategoryDbcontex.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         
        [HttpGet]
        public async Task<IActionResult> View(Guid CategoryID )
        {
            var category = await mvcCategoryDbcontex.Categorys.FirstOrDefaultAsync(x => x.CategoryId == CategoryID);
            
           
            if (category != null)
            {
                var viewModel = new UpdateCategoryViewModel()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                };
                return await Task.Run(() => View("View",  viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateCategoryViewModel model)
        { 
            var category = await mvcCategoryDbcontex.Categorys.FindAsync(model.CategoryId);

            if (category != null)
            { 
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;

                await mvcCategoryDbcontex.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCategoryViewModel model)
        {
            var category = await mvcCategoryDbcontex.Categorys.FindAsync(model.CategoryId);

            if (category != null)
            {
                mvcCategoryDbcontex.Categorys.Remove(category);
                await mvcCategoryDbcontex.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
         

        }
    }
}
