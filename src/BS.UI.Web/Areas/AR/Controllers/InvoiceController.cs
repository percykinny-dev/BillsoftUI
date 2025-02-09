using AspNetCoreHero.ToastNotification.Notyf;
using BS.Application.Contracts.AP;
using BS.Application.Services.AP;
using BS.Application.Services.AR;
using BS.Application.ViewModels;
using BS.Application.ViewModels.AR;
using BS.Domain.Exceptions;
using BS.Infrastructure.Repositories.AR;
using System.Security.Principal;
using BS.Domain.Entities.AR;
using Microsoft.JSInterop;
using Presentation.Web.Common;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json;

namespace BS.UI.Web.Controllers
{
    //[Route("[controller]")]
    [Area("AR")]
    [Route("AR/[controller]")]
    public class InvoiceController : BSBaseController<InvoiceController>
    {
        readonly ResourceManager _resourceManager;
        readonly INotyfService _notifyService;
        readonly IARInvoiceService invoiceService;
        //TODO: Added for filling challan dropdown
        readonly IARChallanService challanService;

        public InvoiceController(ResourceManager resourceManager,
                                INotyfService notifyService,
                                IARInvoiceService invoiceService,
                                IARChallanService challanService)
        {
            _resourceManager = resourceManager;
            _notifyService = notifyService;
            this.invoiceService = invoiceService;
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
        [Route("GetInvoicesList")]
        public async Task<ActionResult> GetInvoicesList()
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

            //var invoices = await invoiceService.GetInvoices(BSCompanyId, queryFilter, null, null);

            int totalRecords = queryFilter.RecordCount; // Total records in the database

            int filteredRecords = queryFilter.RecordCount; // Needs to be corrected. Total records after applying any filters

            // Convert the IEnumerable<ARCustomerVM> to a JSON array
            //var jsonData = invoices.Select(invoice => new
            //{
            //    invoiceid = invoice.InvoiceID,
            //    invoiceno = invoice.InvoiceNo,
            //    challanid = invoice.ChallanID,
            //    challanno = invoice.ChallanNo,
            //    challandate = invoice.ChallanDate.Value.ToShortDateString(),
            //    customername = invoice.CustomerName,
            //    netamount = invoice.NetAmount,
            //    cgstamount = invoice.CGSTAmount,
            //    sgstamount = invoice.SGSTAmount,
            //    igstamount = invoice.IGSTAmount,
            //    gstamount = invoice.GSTAmount,
            //    totalamount = invoice.TotalAmount
            //});


            // Prepare the response object
            var response = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                //data = jsonData
            };
            //var x = await RenderViewToStringAsync("Index", response, this.ControllerContext);
            //return Json(new { data = response });
            return Json(response);

        }

        // GET: ChallanController/Detail/5
        [Route("Detail/{id}")]
        public ActionResult Detail(int id)
        {

            return View();
        }

        [Route("InvoiceAddEdit/{invoiceId:int?}")]
        //public ActionResult InvoiceAddEdit(int invoiceId)
        public async Task<ActionResult> InvoiceAddEdit(int? invoiceId)
        {
            ViewBag.Resources = _resourceManager.GetResources();
            Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));


            var viewModel = new ARInvoiceDetailVM();
            int? customerId = 0;


            //Dictionary<string, string> data = ViewBag.Resources;
            //var pageTitle = data[ResourceKeyEnum.AR_Invoice_CREATE_PAGETITLE.ToString()];
            if (invoiceId.HasValue)
            {
                // Fetch the existing Challan details from the database
                //var existingChallan = await challanService.GetChallan(BSCompanyId, challanId.Value);
                // var existingInvoice = await invoiceService.GetInvoice(BSCompanyId, invoiceId.Value);


                // if (existingInvoice == null)
                {
                    // Handle the case where the Challan with the given ID doesn't exist
                   // return NotFound();
                }

                // customerId = existingInvoice.Challan.CustomerID;

                //Populate the common properties with existing Challan details
                //viewModel.Invoice.InvoiceID = existingInvoice.invoice.invoiceID;
                //viewModel.Invoice.InvoiceNo = existingInvoice.invoice.invoiceNo;
                //viewModel.Invoice.CustomerID = existingInvoice.invoice.CustomerID;
                //viewModel.Invoice.ChallanID = existingInvoice.invoice.ChallanID;
                //viewModel.Invoice.ChallanNo = existingInvoice.invoice.ChallanNo;
                //viewModel.Invoice.CustomerID = existingInvoice.invoice.CustomerID;
                //viewModel.Invoice.NetAmount = existingInvoice.invoice.NetAmount;
                //viewModel.Invoice.TotalAmount = existingInvoice.invoice.TotalAmount;

                //viewModel.InvoiceDetails = existingInvoice.invoiceDetails?.ToList();
            }

            //TODO: Dropdown to be filled from InvoiceService but GetSharedListsVM is not available
            // Retrieve the shared lists of dropdown data required for the view
            //viewModel.SharedLists = await challanService.GetSharedListsVM(BSCompanyId, customerId.Value);
            //viewModel.SharedLists = await invoiceService.GetSharedListsVM(BSCompanyId, customerId.Value);
            //BindDropDowns(viewModel.SharedLists, viewModel.Challan);



            return View(viewModel);
        }


        [HttpPost]
        [Route("SaveInvoice")]
        public async Task<ActionResult> SaveInvoice(ARInvoice invoice, IEnumerable<ARInvoiceDetail> invoiceItems)
        {
            try
            {
                Guard.AgainstNull(invoice, nameof(invoice));

                invoice.CompanyID = BSCompanyId;

                //var result = await invoice.SaveInvoice(invoice, invoiceItems);

                await Task.CompletedTask;
                //if (!result.IsSuccess)
                //    throw new BSInfrastructureException(result.Messages[0].IsNull() ? "error saving invoice information" : result.Messages[0]);

                _notifyService.Success("Invoice data saved successfully");
                return Json(new { success = true, data = new { InvoiceId = 101 } });
                //return Json(new { success = true, data = new { InvoiceId = invoice.InvoiceID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving Invoice data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = false, data = new { InvoiceId = invoice.InvoiceID } });
            }
        }

        // GET: InvoiceController/Delete/5
        [HttpGet]
        [Route("Delete/{invoiceId}")]
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: InvoiceController/Delete/5
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
    }
}
