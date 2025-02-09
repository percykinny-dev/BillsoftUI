using AspNetCoreHero.ToastNotification.Notyf;
using BS.Domain.Exceptions;
using Microsoft.JSInterop;
using Presentation.Web.Common;
using BS.Application.ViewModels.AR;
/*
using BS.Application.Contracts.AP;
using BS.Application.Services.AP;
using BS.Application.Services.AR;
*/

namespace BS.UI.Web.Controllers
{
    //[Route("[controller]")]
    [Area("AR")]
    [Route("AR/[controller]")]
    public class ItemController : BSBaseController<ItemController>
    {
        readonly ResourceManager _resourceManager;
        readonly INotyfService _notifyService;
        //readonly IARItemService ItemService;
        //TODO: Added for filling challan dropdown
        //readonly IARChallanService challanService;

        public ItemController(ResourceManager resourceManager,
                                INotyfService notifyService
                                //IARItemService ItemService,
                                //IARChallanService challanService
                                //
                                )
        {
            _resourceManager = resourceManager;
            _notifyService = notifyService;
            //this.ItemService = ItemService;
            //this.challanService = challanService;

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
        [Route("GetItemsList")]
        public async Task<ActionResult> GetItemsList()
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
                            new { itemid = 1, hsnno = "HSN001", itemname="Sample Item", rate = 10.99, cgst = 9, sgst = 9, igst = 18, uom = "boxes", category = "xerox"},
                            new { itemid=2, hsnno = "HSN002", itemname="Sample Item 2", rate = 10.99, cgst = 9, sgst = 9, igst = 18, uom = "boxes", category = "xerox"},
                            };

            var jsonData = configs;//JsonConvert.SerializeObject(configs);

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

        // GET: ItemController/Detail/5
        [Route("Detail")]
        public ActionResult Detail(int id)
        {

            return View();
        }

        [Route("ItemAddEdit")]
        //public ActionResult ItemAddEdit(int ItemId)
        public async Task<ActionResult> ItemAddEdit(int? ItemId)
        {
            Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));

            //var existingChallan = await challanService.GetChallan(BSCompanyId, challanId.Value);
            var viewModel = new ARChallanDetailVM(); //ARItemDetailVM();             
            //int? customerId = 0;

            ViewBag.Resources = _resourceManager.GetResources();
            //Dictionary<string, string> data = ViewBag.Resources;
            //var pageTitle = data[ResourceKeyEnum.ITEM_CREATE_PAGETITLE.ToString()];
            if (ItemId > 0)
            {
                //Fill Item data
            }

            //TODO: Dropdown to be filled from ItemService but GetSharedListsVM is not available
            // Retrieve the shared lists of dropdown data required for the view
            //viewModel.SharedLists = await challanService.GetSharedListsVM(BSCompanyId, customerId.Value);
            //viewModel.SharedLists = await ItemService.GetSharedListsVM(BSCompanyId, customerId.Value);
            //BindDropDowns(viewModel.SharedLists, viewModel.Challan);
            //return View(viewModel);

            return View();
        }

        /*
        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ItemAddEdit(ARItem Item)
        {
            try
            {

                Guard.AgainstNull(Item, nameof(Item));

                var result = await ItemService.SaveItem(Item);

                if (!result.IsSuccess)
                    throw new BSApplicationException(result.Messages.FirstOrDefault());

                _notifyService.Success("Item data saved successfully");
                return Json(new { success = true, data = new { ItemId = Item.ItemID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while adding new Item data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = true, data = new { ItemId = Item.ItemID } });
            }
        }
        */

        // POST: ItemController/Create
        [HttpPost]
        [Route("SaveItem")]
        public async Task<ActionResult> SaveItem()
        {
            try
            {

                //Guard.AgainstNull(Item, nameof(Item));
                

                //Item.CompanyID = BSCompanyId;
                //var result = await Item.SaveItem(Item, ItemItems);

                await Task.CompletedTask;
                //if (!result.IsSuccess)
                //    throw new BSInfrastructureException(result.Messages[0].IsNull() ? "error saving Item information" : result.Messages[0]);

                _notifyService.Success("Item data saved successfully");
                return Json(new { success = true, data = new { ItemId = 101 } });
                //return Json(new { success = true, data = new { ItemId = Item.ItemID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving Item data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = false, data = new { ItemId = 101 } });
            }
        }
        /*
        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        */

        // POST: ItemController/Delete/5
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
    }
}
