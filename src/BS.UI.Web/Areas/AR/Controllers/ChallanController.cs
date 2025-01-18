using BS.Application.Services.AR;
using BS.Application.ViewModels;
using BS.Application.ViewModels.AR;
using BS.Domain.Exceptions;
using BS.Infrastructure.Repositories.AR;
using System.Security.Principal;


namespace BS.UI.Web.Controllers
{
    [Area("AR")]
    [Route("AR/[controller]")]
    public class ChallanController : BSBaseController<ChallanController>
    {
        readonly ResourceManager _resourceManager;
        readonly INotyfService _notifyService;
        readonly IARChallanService challanService;

        public ChallanController(ResourceManager resourceManager,
                                INotyfService notifyService,
                                IARChallanService challanService)
        {
            _resourceManager = resourceManager;
            _notifyService = notifyService;
            this.challanService = challanService;

        }



        [HttpGet]
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.Resources = _resourceManager.GetResources();
            return View();
        }

        [HttpPost]
        [Route("GetChallansList")]
        public async Task<ActionResult> GetChallansList()
        {
            // Get DataTables parameters

            int draw = int.Parse(Request.Form["draw"]);
            int start = int.Parse(Request.Form["start"]);
            int length = int.Parse(Request.Form["length"]);
            int page = 0;
            int pageSize = 10;

            page = (start / length) + 1;
            pageSize = length;


            QueryFilter queryFilter = new QueryFilter();
            queryFilter.PageNumber = page;
            queryFilter.PageSize = pageSize;
            var challans = await challanService.GetChallansList(BSCompanyId, queryFilter, null);

            int totalRecords = queryFilter.RecordCount; // Total records in the database

            int filteredRecords = queryFilter.RecordCount; // Needs to be corrected. Total records after applying any filters

            // Convert the IEnumerable<ARCustomerVM> to a JSON array
            var jsonData = challans.Select(challan => new
            {
                id = challan.ChallanID,
                challanno = challan.ChallanNo,
                challandate = challan.ChallanDate.Value.ToShortDateString(),
                customername = challan.CustomerName,
                netamount = challan.NetAmount,
                cgstamount = challan.CGSTAmount,
                sgstamount = challan.SGSTAmount,
                igstamount = challan.IGSTAmount,
                gstamount = challan.GSTAmount,
                totalamount = challan.TotalAmount
            });


            // Prepare the response object
            var response = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = jsonData
            };
            //var x = await RenderViewToStringAsync("Index", response, this.ControllerContext);
            //return Json(new { data = response });
            return Json(response);

        }

        // GET: ChallanController/Detail/5
        [Route("Detail")]
        public ActionResult Detail(int id)
        {

            return View();
        }

        [Route("ChallanAddEdit/{challanId?}")]
        public async Task<ActionResult> ChallanAddEdit(int? challanId)
        {
            ViewBag.Resources = _resourceManager.GetResources();
            Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));

            var viewModel = new ARChallanDetailVM();
            int? customerId = 0;

            if (challanId.HasValue)
            {
                // Fetch the existing Challan details from the database
                //var existingChallan = await challanService.GetChallan(BSCompanyId, challanId.Value);
                var existingChallan = await challanService.GetChallanDetailVM(BSCompanyId, challanId.Value);


                if (existingChallan == null)
                {
                    // Handle the case where the Challan with the given ID doesn't exist
                    return NotFound();
                }

                customerId = existingChallan.Challan.CustomerID;
                // Populate the common properties with existing Challan details
                //viewModel.ChallanId = existingChallan.Id;
                //viewModel.CustomerId = existingChallan.CustomerId;
                //viewModel.NetAmount = existingChallan.NetAmount;
                //viewModel.TotalAmount = existingChallan.TotalAmount;
                //viewModel.ChallanNo = existingChallan.ChallanNo;
            }

            // Retrieve the shared lists of dropdown data required for the view
            viewModel.SharedLists = await challanService.GetSharedListsVM(BSCompanyId, customerId.Value);

            BindDropDowns(viewModel.SharedLists, viewModel.Challan);


            // Pass the ViewModel to the view
            return View(viewModel);
        }

        // POST: ChallanController/Create
        [HttpPost]
        [Route("SaveChallan")]
        public async Task<ActionResult> SaveChallan(ARChallan challan, IEnumerable<ARChallanDetail> challanItems)
        {
            try
            {

                Guard.AgainstNull(challan, nameof(challan));


                //challan.CompanyID = BSCompanyId;
                //var result = await challanService.SaveChallan(challan, challanItems);

                await Task.CompletedTask;
                //if (!result.IsSuccess)
                //    throw new BSInfrastructureException(result.Messages[0].IsNull() ? "error saving challan information" : result.Messages[0]);

                _notifyService.Success("Challan data saved successfully");
                return Json(new { success = true, data = new { ChallanId = 101} });
                //return Json(new { success = true, data = new { ChallanId = challan.ChallanID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving Challan data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = false, data = new { ChallanId = challan.ChallanID } });
            }
        }



        // GET: ChallanController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChallanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region private methods

        private void BindDropDowns(ARSharedListsVM data, ARChallanDataVM challan)
        {
            ViewBag.Customers = data.Customers
            .Select(s => new SelectListItem()
            {
                Text = $"{s.Code} - {s.Title}",
                Value = s.CustomerID.ToString(),
                Selected = s.CustomerID == challan?.CustomerID  
            }).ToList();


            ViewBag.Products = data.Items
            .Select(s => new SelectListItem()
            {
                Text = $"{s.HSNNo} - {s.ItemName}",
                Value = s.ItemID.ToString(),
            }).ToList();

            ViewBag.UOMTypes = data.UOMTypes
            .OrderBy(s => s.UOMType)
            .Select(s => new SelectListItem()
            {
                Text = $"{s.UOMType}",
                Value = s.UOMTypeID.ToString(),
            }).ToList();

            ViewBag.GSTTypes = data.GSTRateTypes
           .Select(s => new SelectListItem()
           {
               Text = $"{s.RateDescription} [ {s.CGST_Rate.ToString()}% ]",
               Value = s.GSTRateTypeID.ToString(),
           }).ToList();


        }

        #endregion
    }



}
