using AspNetCoreHero.ToastNotification.Notyf;
using BS.Application.Services.AP;
using BS.Application.Services.AR;
using BS.Application.ViewModels.AR;
using BS.Domain.Entities.AR;
using BS.Domain.Exceptions;
using BS.Infrastructure.Repositories.AR;
using Microsoft.JSInterop;
using Presentation.Web.Common;

namespace BS.UI.Web.Controllers
{
    //[Route("[controller]")]
    [Area("AR")]
    [Route("AR/[controller]")]
    public class CustomerController :BSBaseController<CustomerController>
    {
        readonly ResourceManager _resourceManager;
        readonly IARCustomerService customerService;
        readonly INotyfService notifyService;

        public CustomerController(
                ResourceManager resourceManager, 
                IARCustomerService customerService,
                INotyfService notifyService) 
        {
            _resourceManager = resourceManager;
            this.customerService = customerService;
            this.notifyService = notifyService;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Resources = _resourceManager.GetResources();
            //QueryFilter queryFilter = null;
            //var customers = await customerService.GetCustomers(BSCompanyId, queryFilter, null);
            //return View(customers);
            return View();
        }

        [HttpPost]
        [Route("GetCustomersData")]
        public async Task<ActionResult> GetCustomersData()
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
            var customers = await customerService.GetCustomers(BSCompanyId, queryFilter, null);

            int totalRecords = queryFilter.RecordCount; // Total records in the database
            int filteredRecords = queryFilter.RecordCount; // Total records after applying any filters

            // Convert the IEnumerable<ARCustomerVM> to a JSON array
            var jsonData = customers.Select(customer => new
            {
                id = customer.ID,
                title = customer.Title,
                code = customer.Code,
                gstno = customer.GSTNo,
                billingaddress = customer.BillAddress1 + ' ' + customer.BillAddress2 + ' ' + customer.BillCity + ' ' + customer.BillState,
                emailaddress = customer.EmailAddress
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
    

        // GET: CustomerController/Detail/5
        [Route("Detail/{id}")]
        public async Task<ActionResult> Detail(int id)
        {

            var customerDetail = await customerService.GetCustomerDetails(BSCompanyId,id);
            return View(customerDetail);
        }

        [HttpPost]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            try
            {
                Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));
                Guard.GreaterThan(0, customerId, nameof(customerId));

                var customer = await customerService.GetCustomer(customerId);

                return Json(new { success = true, data = customer });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while loading ar invoice detail data";
                LogException(msg, ex);
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("GetDefaultAddress")]
        public async Task<IActionResult> GetDefaultAddress(int customerId, byte addressType)
        {
            try
            {
                Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));
                Guard.GreaterThan(0, customerId, nameof(customerId));

                var customerAddress = await customerService.GetCustomerDefaultAddress(customerId, addressType);

                return Json(new { success = true, data = customerAddress });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while retreiving customer address data";
                LogException(msg, ex);
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("GetCustomerDefaultAddresses")]
        public async Task<IActionResult> GetCustomerDefaultAddresses(int customerId)
        {
            try
            {
                Guard.GreaterThan(0, BSCompanyId, nameof(BSCompanyId));
                Guard.GreaterThan(0, customerId, nameof(customerId));

                var billingAddress = await customerService.GetCustomerDefaultAddress(customerId, 1);
                var shippingAddress  = await customerService.GetCustomerDefaultAddress(customerId, 2);
                List<ARCustomerAddress> addresses = new List<ARCustomerAddress>();
                if (billingAddress != null) { addresses.Add(billingAddress); }
                if (shippingAddress != null) { addresses.Add(shippingAddress);}

                return Json(new { success = true, data = addresses });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while retreiving customer default address data";
                LogException(msg, ex);
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                return Json(new { success = false });
            }
        }


        [HttpPost]
        [Route("GetCustomerAddress")]
        public async Task<IActionResult> GetCustomerAddress(int customerId, int addressId)
        {
            try
            {
                Guard.GreaterThan(0, customerId, nameof(customerId));
                Guard.GreaterThan(0, addressId, nameof(addressId));

                var customerAddress = await customerService.GetCustomerAddress(customerId, addressId);

                return Json(new { success = true, data = customerAddress });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while loading ar invoice detail data";
                LogException(msg, ex);
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("GetCustomerAddressList")]
        public async Task<IActionResult> GetCustomerAddressList(int customerId, int addressType)
        {
            try
            {
                Guard.GreaterThan(0, customerId, nameof(customerId));
                Guard.GreaterThan(0, addressType, nameof(addressType));

                IEnumerable<ARCustomerAddress> addressList;

                if (addressType == 1)
                {
                    addressList = await customerService.GetCustomerBillingAddresses(customerId);
                }
                else
                {
                    addressList = await customerService.GetCustomerShippingAddresses(customerId);
                }
                

                return Json(new { success = true, data = addressList });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while loading ar invoice detail data";
                LogException(msg, ex);
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                return Json(new { success = false });
            }
        }

        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.Resources = _resourceManager.GetResources();
            //Dictionary<string, string> data = ViewBag.Resources;
            //var pageTitle = data[ResourceKeyEnum.AR_CUSTOMER_CREATE_PAGETITLE.ToString()];
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [Route("SaveCustomer")]
        public async Task<IActionResult> SaveCustomer(ARCustomerVM arCustomer)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return InvalidModelStateJsonResponse();

                var customer = arCustomer as ARCustomerVM;
                customer.CompanyId = BSCompanyId;
                var result = await customerService.SaveCustomer(customer);

                if (!result.IsSuccess)
                    throw new BSInfrastructureException(result.Messages[0].IsNull()? "error saving customer information" : result.Messages[0] );

                var msg = "customer saved successfully";
                notifyService.Success(msg);
                return Json(new { success = true, message = msg, customerId = customer.ID });
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving customer information";
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                LogException(msg, ex);
            }
            return Json(new { success = false, message = "error" });
        }


        [HttpPost]
        [Route("SaveCustomerAddress")]
        public async Task<IActionResult> SaveCustomerAddresss(ARCustomerAddress arCustomerAddress)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return InvalidModelStateJsonResponse();

              
                var result = await customerService.SaveCustomerAddress(arCustomerAddress);

                if (!result.IsSuccess)
                    throw new BSInfrastructureException(result.Messages[0].IsNull() ? "error saving customer address information" : result.Messages[0]);

                IEnumerable<ARCustomerAddress> addressList;
                if (arCustomerAddress.AddressType == 1)
                {
                    addressList = await customerService.GetCustomerBillingAddresses(arCustomerAddress.CustomerID);
                }
                else
                {
                    addressList = await customerService.GetCustomerShippingAddresses(arCustomerAddress.CustomerID);
                }

                var msg = "customer address saved successfully";
                notifyService.Success(result.Messages[0]);
                return Json(new 
                        {   success = true, 
                            message = result.Messages[0], 
                            addressType = arCustomerAddress.AddressType,
                            adrressList = addressList
                        }
                );
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving customer address information";
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                LogException(msg, ex);
            }
            return Json(new { success = false, message = "error" });
        }

        [HttpPost]
        [Route("DeleteCustomerAddress")]
        public async Task<IActionResult> DeleteCustomerAddress(int customerId, int customerAddressId)
        {
            try
            {
                var customerAddress = await customerService.GetCustomerAddress(customerId,customerAddressId);

                var result = await customerService.DeleteCustomerAddress(customerAddress);

                if (!result)
                    throw new BSInfrastructureException("error deleting customer address information");

                IEnumerable<ARCustomerAddress> addressList;
                if (customerAddress.AddressType == 1)
                {
                    addressList = await customerService.GetCustomerBillingAddresses(customerAddress.CustomerID);
                }
                else
                {
                    addressList = await customerService.GetCustomerShippingAddresses(customerAddress.CustomerID);
                }

                var msg = "customer address deleted successfully";
                notifyService.Success(msg);
                return Json(new
                {
                    success = true,
                    message = msg,
                    addressType = customerAddress.AddressType,
                    adrressList = addressList
                }
                );
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while saving customer information";
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                LogException(msg, ex);
            }
            return Json(new { success = false, message = "error" });
        }


        [HttpPost]
        [Route("UpdateCustomerDefaultAddress")]
        public async Task<IActionResult> UpdateCustomerDefaultAddress(int customerId, int addressId, byte addressType)
        {
            try
            {

                var result = await customerService.UpdateCustomerDefaultAddress(customerId, addressId);

                if (!result)
                    throw new BSInfrastructureException("error setting Default Address for the customer");

                IEnumerable<ARCustomerAddress> addressList;
                if (addressType == 1)
                {
                    addressList = await customerService.GetCustomerBillingAddresses(customerId);
                }
                else
                {
                    addressList = await customerService.GetCustomerShippingAddresses(customerId);
                }

                var msg = "Default address changed successfully";
                notifyService.Success(msg);
                return Json(new
                    {
                        success = true,
                        message = msg,
                        addressType = addressType,
                        adrressList = addressList
                    }
                );
            }
            catch (Exception ex)
            {
                var msg = "an error occurred while changing default address";
                if (ex is BSInfrastructureException)
                    msg = ex.Message;

                notifyService.Warning(msg);
                LogException(msg, ex);
            }
            return Json(new { success = false, message = "error" });
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
