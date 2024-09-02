using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpManageSys2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Hosting;
using System.Data.SqlTypes;
using EmpManageSys2.ViewModel;
using System.Numerics;
using NuGet.Common;
using EmpManageSys2.Services.Validation;
using System.Text;

namespace EmpManageSys2.Controllers
{
    public class EmployeeMastersController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        private readonly EmployeeManageSys1Context _context;
        private readonly ICreateUpdateValidationCheck _createUpdateValidationCheck;

        public EmployeeMastersController(EmployeeManageSys1Context context, IWebHostEnvironment webHostEnvironment,ICreateUpdateValidationCheck createUpdateValidationCheck)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _createUpdateValidationCheck = createUpdateValidationCheck;
        }

        // GET: EmployeeMasters
        public async Task<IActionResult> Index(string NotMessage = "")
        {
            try
            {
                ViewBag.Notmsg = NotMessage;
                var employeeManageSys1Context = _context.EmployeeMasters.Include(e => e.City).Include(e => e.Contry).Include(e => e.State).Where(a => a.IsActive == true);
                //int pageSize = 10;
                //var paginatedList = await PaginatedList<EmployeeMaster>.CreateAsync(employeeManageSys1Context, pageIndex, pageSize);
                //return View(paginatedList);
                return View(await employeeManageSys1Context.ToListAsync());

                // New logic
                // Searching
                //if (!String.IsNullOrEmpty(searchTerm))
                //{
                //    employeeManageSys1Context = employeeManageSys1Context.Where(e => e.EmailAddress.Contains(searchTerm) || 
                //                                                                     e.FirstName.Contains(searchTerm) || 
                //                                                                     e.City.CityName.Contains(searchTerm) ||
                //                                                                     e.State.StateName.Contains(searchTerm) ||
                //                                                                     e.Contry.CountryName.Contains(searchTerm) ||
                //                                                                     e.PanNumber.Contains(searchTerm) ||
                //                                                                     e.PassportNumber.Contains(searchTerm) ||
                //                                                                     e.MobileNumber.Contains(searchTerm));
                //}

                //// Sorting
                //switch (sortOrder)
                //{
                //    case "Email Address":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.EmailAddress);
                //        break;
                //    case "First Name":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.FirstName);
                //        break;
                //    case "Country":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.Contry.CountryName);
                //        break;
                //    case "State":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.State.StateName);
                //        break;
                //    case "City":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.City.CityName);
                //        break;
                //    case "Pan Number":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.PanNumber);
                //        break;
                //    case "Passport Number":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.PassportNumber);
                //        break;
                //    case "Gender":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.Gender);
                //        break;
                //    case "Is Active":
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.IsActive);
                //        break; 
                //    default:
                //        employeeManageSys1Context = employeeManageSys1Context.OrderBy(e => e.CreatedDate);
                //        break;
                //}
                //// Pagination
                //var totalRecords = await employeeManageSys1Context.CountAsync();
                //var paginatedEmployees = await employeeManageSys1Context.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                ////var paginatedList = await PaginatedList<EmployeeMaster>.CreateAsync(employeeManageSys1Context, pageNumber, pageSize);

                //var viewModel = new PaginatedList
                //{
                //    employeeMasters = paginatedEmployees,
                //    SearchTerm = searchTerm,
                //    SortOrder = sortOrder,
                //    PageIndex = pageNumber,
                //    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                //};

                //return View(viewModel);
            }
            
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.InnerException?.Message });
            }
        }

        // GET: EmployeeMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMaster = await _context.EmployeeMasters
                .Include(e => e.City)
                .Include(e => e.Contry)
                .Include(e => e.State)
                .FirstOrDefaultAsync(m => m.RowId == id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Create
        public IActionResult Create()
        {
            try
            {
                var countries = _context.Countries.ToList();
                ViewData["Countries"] = countries;
                var Country = _context.Countries.SingleOrDefault(s => s.RowId == 1);
                if (Country != null)
                {
                    ViewBag.Country = Country;
                }

                var State = _context.States.SingleOrDefault(s => s.RowId == 3);
                if (State != null)
                {
                    //ViewBag.Country = cnt.CountryName;
                    ViewBag.State = State;
                }

                var City = _context.Cities.SingleOrDefault(s => s.RowId == 1);
                if (City != null)
                {
                    //ViewBag.State = st.StateName;
                    ViewBag.City = City;
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.InnerException?.Message });
            }
        }

        // POST: EmployeeMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RowId,EmployeeCode,FirstName,LastName,ContryId,StateId,CityId,EmailAddress,MobileNumber,PanNumber,PassportNumber,ProfileImage,Gender,IsActive,DateOfBirth,DateOfJoinee,CreatedDate,UpdatedDate,IsDeleted,DeletedDate")] EmployeeMaster employeeMaster, IFormFile? image, string? notMsg = "")
        {
            try
            {
                var a = new SelectList(_context.EmployeeMasters, "EmployeeCode", "EmployeeCode", employeeMaster.EmployeeCode);
                List<int> empnos = new List<int>();
                foreach (var eno in a)
                {
                    empnos.Add(int.Parse(eno.Value));
                }
                int maxEmpCode = empnos.Max();
                //ViewBag.MaxEmpCode = $"00{maxEmpCode + 1}";
                employeeMaster.EmployeeCode = $"00{maxEmpCode + 1}";
                if (ModelState.IsValid)
                {
                    if (employeeMaster.DateOfJoinee < employeeMaster.DateOfBirth)
                    {
                        ModelState.AddModelError("DateOfJoinee", "Joining date cannot be earlier than the birth date.");
                        return View(employeeMaster);
                    }
                    if (image != null && image.Length > 0)
                    {
                        // Generate a unique filename
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        var extension = Path.GetExtension(image.FileName);
                        var todaysDate = DateTime.Now.ToString("ddMMyyyy");
                        var uniqueFileName = $"{employeeMaster.FirstName}_{employeeMaster.LastName}_{todaysDate}_{Guid.NewGuid()}{extension}";

                        // Get the path to the wwwroot folder
                        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                        // Ensure the directory exists
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        // Combine the path with the filename
                        var filePath = Path.Combine(uploadPath, uniqueFileName);

                        // Save the file to the local filesystem
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            employeeMaster.ProfileImage = uniqueFileName;
                            await image.CopyToAsync(fileStream);
                        }

                        //ViewBag.Message = "Image uploaded successfully!";
                    }
                    else
                    {
                        ModelState.AddModelError("image", "Please select Image");
                        return View(employeeMaster);
                    }
                    //_context.Cities.SingleOrDefault(s => s.RowId == employeeMaster.CityId);

                    employeeMaster.PassportNumber = employeeMaster.PassportNumber.ToUpper();
                    employeeMaster.PanNumber = employeeMaster.PanNumber.ToUpper();

                    employeeMaster.CreatedDate = DateTime.Now;
                    _context.Add(employeeMaster);
                    await _context.SaveChangesAsync();
                    ViewBag.SuccessMessage = "Success";
                    return RedirectToAction(actionName: "Index",new { NotMessage = "Data added successfully" });
                }
                var Gender = employeeMaster.Gender;
                if (Gender != null)
                {
                    ViewBag.Gender = Gender;
                }
               
                ViewData["CityId"] = new SelectList(_context.Cities, "RowId", "RowId", employeeMaster.CityId);
                ViewData["ContryId"] = new SelectList(_context.Countries, "RowId", "RowId", employeeMaster.ContryId);
                ViewData["StateId"] = new SelectList(_context.States, "RowId", "RowId", employeeMaster.StateId);
                return View(employeeMaster);
            }
            catch (Exception ex)
            {
                var message = _createUpdateValidationCheck.checkException(ex.InnerException.Message);
                employeeMaster.Gender = employeeMaster.Gender;
                var countries = _context.Countries.ToList();
                ViewData["Countries"] = countries;

                var Gender = employeeMaster.Gender;
                if (Gender != null)
                {
                    ViewBag.Gender = Gender;
                }

                var Country = _context.Countries.SingleOrDefault(s => s.RowId == employeeMaster.ContryId);
                if (Country != null)
                {
                    ViewBag.Country = Country;
                }

                var State = _context.States.SingleOrDefault(s => s.RowId == employeeMaster.StateId);
                if (State != null)
                {
                    //ViewBag.Country = cnt.CountryName;
                    ViewBag.State = State;
                }

                var City = _context.Cities.SingleOrDefault(s => s.RowId == employeeMaster.CityId);
                if (City != null)
                {
                    //ViewBag.State = st.StateName;
                    ViewBag.City = City;
                }

                ViewBag.ErrorMessage = message;

                ViewBag.isRefreshed = "Yes";
                //ViewBag.SelecCountry = from country in _context.Countries where country.RowId == employeeMaster.ContryId select country;
                //ViewBag.SelecState = from state in _context.States where state.RowId == employeeMaster.ContryId select state;
                //ViewBag.SelecCity = from city in _context.Cities where city.RowId == employeeMaster.ContryId select city;

                return View(employeeMaster);

                //return View("Error", new ErrorViewModel { Message = ex.InnerException?.Message });
            }
        }

        // GET: EmployeeMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    ViewData["Countries"] = _context.Countries.ToList();
                    var employeeMaster = await _context.EmployeeMasters.FindAsync(id);
                    DateTime createdDate = _context.EmployeeMasters
                                    .Where(e => e.RowId == employeeMaster.RowId)
                                    .Select(e => e.CreatedDate)
                                    .FirstOrDefault();

                    employeeMaster.CreatedDate = createdDate;
                    if (employeeMaster == null)
                    {
                        return NotFound();
                    }

                    var Gender = employeeMaster.Gender;
                    if (Gender != null)
                    {
                        ViewBag.Gender = Gender;
                    }

                    var Country = _context.Countries.SingleOrDefault(s => s.RowId == employeeMaster.ContryId);
                    if (Country != null)
                    {
                        ViewBag.Country = Country;
                    }

                    var State = _context.States.SingleOrDefault(s => s.RowId == employeeMaster.StateId);
                    if (State != null)
                    {
                        //ViewBag.Country = cnt.CountryName;
                        ViewBag.State = State;
                    }

                    var City = _context.Cities.SingleOrDefault(s => s.RowId == employeeMaster.CityId);
                    if (City != null)
                    {
                        //ViewBag.State = st.StateName;
                        ViewBag.City = City;
                    }
                    return View(employeeMaster);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: EmployeeMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RowId,EmployeeCode,FirstName,LastName,ContryId,StateId,CityId,EmailAddress,MobileNumber,PanNumber,PassportNumber,ProfileImage,Gender,IsActive,DateOfBirth,DateOfJoinee,CreatedDate,UpdatedDate,IsDeleted,DeletedDate")] EmployeeMaster employeeMaster, IFormFile? image)
        {
            try
            {
                if (id != employeeMaster.RowId)
                {
                    return NotFound();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            if (image != null && image.Length > 0)
                            {
                                if (employeeMaster.ProfileImage != null)
                                {
                                    var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", employeeMaster.ProfileImage);
                                    //System.IO.File.Delete(existingFilePath);
                                }

                                // Generate a unique filename
                                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                                var extension = Path.GetExtension(image.FileName);
                                var todaysDate = DateTime.Now.ToString("ddMMyyyy");
                                var uniqueFileName = $"{employeeMaster.FirstName}_{employeeMaster.LastName}_{todaysDate}_{Guid.NewGuid()}{extension}";

                                // Get the path to the wwwroot folder
                                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                                // Ensure the directory exists
                                if (!Directory.Exists(uploadPath))
                                {
                                    Directory.CreateDirectory(uploadPath);
                                }

                                // Combine the path with the filename
                                var filePath = Path.Combine(uploadPath, uniqueFileName);

                                // Save the file to the local filesystem
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    employeeMaster.ProfileImage = uniqueFileName;
                                    await image.CopyToAsync(fileStream);
                                }

                                //ViewBag.Message = "Image uploaded successfully!";
                            }
                            else
                            {
                                string? profileImage = _context.EmployeeMasters
                                .Where(e => e.RowId == employeeMaster.RowId)
                                .Select(e => e.ProfileImage)
                                .FirstOrDefault();

                                employeeMaster.ProfileImage = profileImage;
                            }

                            DateTime createdDate = _context.EmployeeMasters
                                    .Where(e => e.RowId == employeeMaster.RowId)
                                    .Select(e => e.CreatedDate)
                                    .FirstOrDefault();

                            employeeMaster.PassportNumber = employeeMaster.PassportNumber.ToUpper();
                            employeeMaster.PanNumber = employeeMaster.PanNumber.ToUpper();
                            employeeMaster.CreatedDate = createdDate;
                            employeeMaster.UpdatedDate = DateTime.Now;
                            _context.Update(employeeMaster);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EmployeeMasterExists(employeeMaster.RowId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }

                    
                    

                }
                var Gender = employeeMaster.Gender;
                if (Gender != null)
                {
                    ViewBag.Gender = Gender;
                }
                ViewData["CityId"] = new SelectList(_context.Cities, "RowId", "RowId", employeeMaster.CityId);
                ViewData["ContryId"] = new SelectList(_context.Countries, "RowId", "RowId", employeeMaster.ContryId);
                ViewData["StateId"] = new SelectList(_context.States, "RowId", "RowId", employeeMaster.StateId);

                return View(employeeMaster);
            }
            catch (Exception ex)
            {
                var message = _createUpdateValidationCheck.checkException(ex.InnerException.Message);
                ViewBag.ErrorMessage = message;
                ViewData["Countries"] = _context.Countries.ToList();
                //var employeeMaster = await _context.EmployeeMasters.FindAsync(id);
                DateTime createdDate = _context.EmployeeMasters
                                .Where(e => e.RowId == employeeMaster.RowId)
                                .Select(e => e.CreatedDate)
                                .FirstOrDefault();

                employeeMaster.CreatedDate = createdDate;
                if (employeeMaster == null)
                {
                    return NotFound();
                }

                var Gender = employeeMaster.Gender;
                if (Gender != null)
                {
                    ViewBag.Gender = Gender;
                }

                var Country = _context.Countries.SingleOrDefault(s => s.RowId == employeeMaster.ContryId);
                if (Country != null)
                {
                    ViewBag.Country = Country;
                }

                var State = _context.States.SingleOrDefault(s => s.RowId == employeeMaster.StateId);
                if (State != null)
                {
                    //ViewBag.Country = cnt.CountryName;
                    ViewBag.State = State;
                }

                var City = _context.Cities.SingleOrDefault(s => s.RowId == employeeMaster.CityId);
                if (City != null)
                {
                    //ViewBag.State = st.StateName;
                    ViewBag.City = City;
                }
                return View(employeeMaster);

                //return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: EmployeeMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var employeeMaster = await _context.EmployeeMasters.FindAsync(id);
                if (employeeMaster != null)
                {
                    employeeMaster.IsActive = false;
                    employeeMaster.IsDeleted = true;
                    employeeMaster.DeletedDate = DateTime.Now;
                    //_context.EmployeeMasters.Remove(employeeMaster);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: EmployeeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employeeMaster = await _context.EmployeeMasters.FindAsync(id);
                if (employeeMaster != null)
                {
                    employeeMaster.IsActive = false;
                    employeeMaster.IsDeleted = true;
                    employeeMaster.DeletedDate = DateTime.Now;
                    //_context.EmployeeMasters.Remove(employeeMaster);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        private bool EmployeeMasterExists(int id)
        {
            return _context.EmployeeMasters.Any(e => e.RowId == id);
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            var countries = _context.Countries.ToList();
            return Json(countries);
        }

        [HttpGet]
        public JsonResult GetStates(int countryId)
        {
            var states = _context.States.Where(s => s.CountryId == countryId).ToList();
            return Json(states);
        }

        [HttpGet]
        public JsonResult GetCities(int stateId)
        {
            var cities = _context.Cities.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }
    }
}
