using Microsoft.AspNetCore.Mvc;
using BS.Domain.Exceptions;
using BS.Application.ViewModels;
using System.Security.Principal;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.JSInterop;
using Presentation.Web.Common;
using BS.Application.Services.AP;
using BS.Application.ViewModels.AR;
/*
using BS.Application.ViewModels.AR;
using BS.Application.Services.AR;
using BS.Infrastructure.Repositories.AR;
using BS.Domain.Entities.AR;
*/

namespace BS.UI.Web.Controllers
//namespace BS.UI.Web.Areas.AP.Controllers
{
    [Area("AP")]
    [Route("AP/[controller]")]
    public class PurchaseController : BSBaseController<PurchaseController>
    {
        readonly ResourceManager _resourceManager;
        readonly INotyfService _notifyService;
        readonly IARChallanService challanService;

        public PurchaseController(ResourceManager resourceManager,
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
        public IActionResult Index()
        {
            ViewBag.Resources = _resourceManager.GetResources();
            return View();
        }

        [HttpPost]
        [Route("GetPurchasesList")]
        public async Task<ActionResult> GetPurchasesList()
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
            queryFilter.SearchText = Request.Form["search[value]"];
            queryFilter.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];

            //todo: Get the items list
            //var items = await itemService.GetItemsList(BSCompanyId, queryFilter, null);

            int totalRecords = queryFilter.RecordCount; // Total records in the database

            int filteredRecords = queryFilter.RecordCount; // Needs to be corrected. Total records after applying any filters

            var configs = new[]
                            {
             new{ billno="P-001", billdate="20-Dec-2024",vendorname="Ambika Stationery",netamount=100,cgstamount=20,sgstamount=20,igstamount=0,gstamount=40,totalamount=140 },
                            };

            var jsonData = configs; //JsonConvert.SerializeObject(configs);

            // Convert the IEnumerable<ITEMVM> to a JSON array
            //var jsonData = items.Select(item => new
            //var jsonData = (new
            //{
            //    //itemid = item.ItemID,
            //    ////itemcode = item.ItemCode,
            //    //hsnono = item.HSNNo,
            //    //itemname = item.ItemName,
            //    //rate = item.Rate,
            //    //cgst = item.CGST,
            //    //sgst = item.SGST,
            //    //igst = item.IGST,
            //    //uom = item.UOM,
            //    //category = item.Category

            //    itemid = 1,
            //    //itemcode = item.ItemCode,
            //    hsnno = "HSN001",
            //    itemname = "Sample Item",
            //    rate = 10.99,
            //    cgst = 9,
            //    sgst = 9,
            //    igst = 18,
            //    uom = "boxes",
            //    category = "xerox"
            //});


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

        [Route("PurchaseAddEdit")]
        //public ActionResult ItemAddEdit(int ItemId)
        public async Task<ActionResult> PurchaseAddEdit(int? challanId)
        {
            ViewBag.Resources = _resourceManager.GetResources();
            Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));

            var viewModel = new ARChallanDetailVM();
            int? customerId = 0;

            if (challanId.HasValue)
            {
                // Fetch the existing Challan details from the database
                var existingChallan = await challanService.GetChallan(BSCompanyId, challanId.Value);
                if (existingChallan == null)
                {
                    // Handle the case where the Challan with the given ID doesn't exist
                    return NotFound();
                }

                customerId = existingChallan.CustomerID;

                // Populate the common properties with existing Challan details
                //viewModel.ChallanId = existingChallan.Id;
                //viewModel.CustomerId = existingChallan.CustomerId;
                //viewModel.NetAmount = existingChallan.NetAmount;
                //viewModel.TotalAmount = existingChallan.TotalAmount;
                //viewModel.ChallanNo = existingChallan.ChallanNo;
            }

            // Retrieve the shared lists of dropdown data required for the view
            viewModel.SharedLists = await challanService.GetSharedListsVM(BSCompanyId, customerId.Value);

           // BindDropDowns(viewModel.SharedLists, viewModel.Challan);

            // Pass the ViewModel to the view
            return View(viewModel);
        }


        //todo: Note here ARChallan is referred to fill dropdowns
        private void BindDropDowns(ARSharedListsVM data, ARChallan challan)
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

        // POST: PurchaseController/Create
        [HttpPost]
        [Route("SavePurchase")]
        public async Task<ActionResult> SavePurchase(ARChallan challan, IEnumerable<ARChallanDetail> challanItems)
        {
            try
            {

                Guard.AgainstNull(challan, nameof(challan));


                //challan.CompanyID = BSCompanyId;
                //var result = await challanService.SavePurchase(challan, challanItems);

                await Task.CompletedTask;
                //if (!result.IsSuccess)
                //    throw new BSInfrastructureException(result.Messages[0].IsNull() ? "error saving challan information" : result.Messages[0]);

                _notifyService.Success("Purchase data saved successfully");
                return Json(new { success = true, data = new { PurchaseId = 101 } });
                //return Json(new { success = true, data = new { PurchaseId = challan.PurchaseID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving Purchase data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = false, data = new { PurchaseId = challan.ChallanID } });
            }
        }

    }
}
