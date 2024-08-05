using HosseinkhaniTest.Models;
using HosseinkhaniTest.Models.DataModel;
using HosseinkhaniTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace HosseinkhaniTest.Controllers
{
    public class HomeController : Controller
    {
        #region Feilds
        //  private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        #endregion


        //public HomeController(ILogger<HomeController> logger )
        //{
        //    _logger = logger;
        //}
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Personnel/Create
        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // POST: Personnel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonnelCreateViewModel viewModel)
        {
            try
            {
                if (viewModel.Files == null || viewModel.Files.Count == 0)
                {
                    ModelState.AddModelError("Files", "لطفا حداثل یک فایل بارگزاری نمایید");
                }
                else
                {

                    foreach (var file in viewModel.Files)
                    {
                        var t = file.GetType;
                        var s = file.Length;
                        if (t.ToString() != "application/txt" || t.ToString() != "application/pdf")
                        {
                            ModelState.AddModelError("Files", "لطفا فایل pdf یا txt بارگزاری نمایید");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    // Map the ViewModel to the model
                    var Prs = new Tbl_Personnels
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        NationalCode = viewModel.NationalCode,
                        PersonalCode = viewModel.PersonalCode,
                        State = 1,
                        DateCreated = DateTime.Now,
                    };

                    // Add to the database
                    _db.Tbl_Personnels.Add(Prs);
                    await _db.SaveChangesAsync();


                    #region Save Uploaded Files 

                    if (viewModel.Files != null && viewModel.Files.Count > 0)
                    {
                        var FileName = "";
                        var FilePath = "";
                        foreach (var file in viewModel.Files)
                        {
                            if (file.Length > 0)
                            {
                                FileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                                FilePath = Path.Combine("/Uploads", FileName);

                                using (var stream = new FileStream("wwwroot/" + FilePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                // Optionally, you can save information about the file in the database
                                var document = new Tbl_PersonnelDocument
                                {
                                    Source = FileName,
                                    FK_PersonnelId = Prs.Id
                                };

                                _db.Tbl_PersonnelDocument.Add(document);
                                await _db.SaveChangesAsync();

                            }
                        }
                    }

                    #endregion


                    TempData["success"] = "اطلاعات با موفقیت ذخیره شد.";

                    return RedirectToAction("Index", "Home"); // Redirect to Personnels List
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
