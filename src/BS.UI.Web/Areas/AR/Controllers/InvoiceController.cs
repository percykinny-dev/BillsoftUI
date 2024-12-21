using AspNetCoreHero.ToastNotification.Notyf;
using BS.Application.Contracts.AP;
using BS.Application.Services.AP;
using BS.Application.Services.AR;
using BS.Domain.Exceptions;
using Microsoft.JSInterop;
using Presentation.Web.Common;

namespace BS.UI.Web.Controllers
{
    //[Route("[controller]")]
    [Area("AR")]
    [Route("AR/[controller]")]
    public class InvoiceController :BSBaseController<InvoiceController>
    {
        readonly ResourceManager _resourceManager;
        readonly INotyfService _notifyService;
        readonly IARInvoiceService invoiceService;

        public InvoiceController(ResourceManager resourceManager, 
                                INotyfService notifyService, 
                                IARInvoiceService invoiceService) 
        {
            _resourceManager = resourceManager;
            _notifyService = notifyService;
            this.invoiceService = invoiceService;

        }

       

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.Resources = _resourceManager.GetResources();
            return View();
        }

        // GET: InvoiceController/Detail/5
        [Route("Detail")]
        public ActionResult Detail(int id)
        {

            return View();
        }

        [Route("InvoiceAddEdit")]
        public ActionResult InvoiceAddEdit(int invoiceId)
        {
            ViewBag.Resources = _resourceManager.GetResources();
            //Dictionary<string, string> data = ViewBag.Resources;
            //var pageTitle = data[ResourceKeyEnum.AR_Invoice_CREATE_PAGETITLE.ToString()];
            if (invoiceId > 0)
            {
                //Fill Invoice data
            }

            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InvoiceAddEdit(ARInvoice invoice)
        {
            try
            {
               
                Guard.AgainstNull(invoice, nameof(invoice));

                var result = await invoiceService.SaveInvoice(invoice);

                if (!result.IsSuccess)
                    throw new BSApplicationException(result.Messages.FirstOrDefault());

                _notifyService.Success("invoice data saved successfully");
                return Json(new { success = true, data = new { invoiceId = invoice.InvoiceID } });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while adding new invoice data";
                LogException(msg, ex);
                _notifyService.Warning(msg);
                return Json(new { success = true, data = new { invoiceId = invoice.InvoiceID } });
            }
        }

        

        // GET: InvoiceController/Delete/5
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
    }
}
