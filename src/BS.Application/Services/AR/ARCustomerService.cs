using BS.Domain.Entities.AR;
using System.ComponentModel.Design;
using System.Net.Mail;

namespace BS.Application.Services.AR;

public class ARCustomerService : IARCustomerService
{
    readonly IARCustomerRepository customerRepository;
    readonly IARCustomerAddressRepository customerAddressRepository;
    readonly IARDBRepository customerDBRepository;

    public ARCustomerService(
        IARCustomerRepository customerRepository,
        IARCustomerAddressRepository customerAddressRepository,
        IARDBRepository customerDBRepository)
    {
        this.customerRepository = customerRepository;
        this.customerAddressRepository = customerAddressRepository;
        this.customerDBRepository = customerDBRepository;
    }

    //customers
    public async Task<IEnumerable<ARCustomerVM>> GetCustomers(int companyId, QueryFilter queryFilter, string[] allowedStatuses)
    {
        var customers = await customerRepository.GetCustomers(companyId, queryFilter, allowedStatuses);
        return customers;
    }

    public async Task<ResultVM> SaveCustomer(ARCustomerVM customerVM)
    {
        var customer = new ARCustomer
        {
            CustomerID = customerVM.ID,
            TypeID = "Standard",
            StatusID = 1,
            CompanyID = customerVM.CompanyId,
            Code = customerVM.Code,
            FirstName = customerVM.FirstName,
            LastName = customerVM.LastName,
            Title = customerVM.Title,
            Currency = customerVM.Currency,
            GSTNo = customerVM.GSTNo,
            PanNo = customerVM.PanNo,
            MSMECustomer = customerVM.MSMECustomer,
            SEZCustomer = customerVM.SEZCustomer,
            EmailAddress = customerVM.EmailAddress,
            WorkPhone = customerVM.WorkPhone,
            MobilePhone = customerVM.MobilePhone,
            PaymentTermID = customerVM.TermsID,
            UDF1 = customerVM.UDF1,
            UDF2 = customerVM.UDF2,
            UDF3 = customerVM.UDF3,
            UDF4 = customerVM.UDF4,
            UDF5 = customerVM.UDF5,
            BillingAddress1 = customerVM.BillAddress1,
            BillingZipcode = customerVM.BillZipcode
         };

        bool codeAlreadyExists = await customerRepository.CheckCustomerCodeAlreadyExists(customer);
        if (codeAlreadyExists)
            return new ResultVM() { Messages = new string[] { $"customer code: {customer.Code} already exists for another customer" } };

        if (customerVM.ID <= 0)
        {
            customer.DateCreated = customer.DateModified = DateTime.Now;
            await customerRepository.AddAsync(customer);

            if (customer.CustomerID > 0)
            {
                customerVM.ID = customer.CustomerID;
                // Add Billing Address
                var billAddress = new ARCustomerAddress
                {
                    CustomerID = customer.CustomerID,
                    StatusID = 1,
                    AddressType = 1,
                    ContactName = customerVM.BillContactName,
                    Address1 = customerVM.BillAddress1,
                    Address2 = customerVM.BillAddress2,
                    City = customerVM.BillCity,
                    State = customerVM.BillState,
                    Zipcode = customerVM.BillZipcode,
                    IsDefault = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };
                await customerAddressRepository.AddAsync(billAddress);

                //Add Shipping Address
                if (!string.IsNullOrEmpty(customerVM.ShipAddress1))
                {
                    var shipAddress = new ARCustomerAddress
                    {
                        CustomerID = customer.CustomerID,
                        StatusID = 1,
                        AddressType = 2,
                        ContactName = customerVM.ShipContactName,
                        Address1 = customerVM.ShipAddress1,
                        Address2 = customerVM.ShipAddress2,
                        City = customerVM.ShipCity,
                        State = customerVM.ShipState,
                        Zipcode = customerVM.ShipZipcode,
                        IsDefault = true,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };
                    await customerAddressRepository.AddAsync(shipAddress);
                }

            }

            return new ResultVM() { IsSuccess = true, Messages = new string[] { $"new customer {customer.Code} added successfully" } };

        }


        var _ = await customerRepository.Get(customerVM.ID);
        if (_ == null)
            return new ResultVM() { IsSuccess = false, Messages = new string[] { "selected customer id does not exist" } };

        if (_.CompanyID != customerVM.CompanyId)
            return new ResultVM() { IsSuccess = false, Messages = new string[] { "incorrect company id" } };


        _.Code = customerVM.Code;
        _.FirstName = customerVM.FirstName;
        _.LastName = customerVM.LastName;
        _.Title = customerVM.Title;
        _.Currency = customerVM.Currency;
        _.GSTNo = customerVM.GSTNo;
        _.PanNo = customerVM.PanNo;
        _.MSMECustomer = customerVM.MSMECustomer;
        _.SEZCustomer = customerVM.SEZCustomer;
        _.EmailAddress = customerVM.EmailAddress;
        _.WorkPhone = customerVM.WorkPhone;
        _.MobilePhone = customerVM.MobilePhone;
        _.PaymentTermID = customerVM.TermsID;
        _.DateModified = DateTime.Now;

        await customerRepository.UpdateAsync(_);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "customer information updated successfully" } };
    }

    public async Task<ResultVM> SaveCustomerAddress(ARCustomerAddress customerAddress)
    {
        if (customerAddress.AddressID > 0 )
        {
            var _ = await customerAddressRepository.Get(customerAddress.AddressID);
            if (_ == null)
                return new ResultVM() { IsSuccess = false, Messages = new string[] { "selected customer address does not exist" } };

            _.ContactName = customerAddress.ContactName;
            _.Address1 = customerAddress.Address1;
            _.Address2 = customerAddress.Address2;
            _.City = customerAddress.City;
            _.State = customerAddress.State;
            _.Zipcode = customerAddress.Zipcode;
            _.EmailAddress = customerAddress.EmailAddress;
            _.WorkPhone = customerAddress.WorkPhone;
            _.MobilePhone = customerAddress.MobilePhone;
            _.WhatsAppPhone = customerAddress.WhatsAppPhone;
            _.DateModified = DateTime.Now;

            await customerAddressRepository.UpdateAsync(_);
            return new ResultVM() { IsSuccess = true, Messages = new string[] { "customer address information updated successfully" } };
        }
        else
        {
            await customerAddressRepository.AddAsync(customerAddress);
            return new ResultVM() { IsSuccess = true, Messages = new string[] { "customer address information added successfully" } };
        }

    }

    public async Task<bool> UpdateCustomerDefaultAddress(int customerId, int customerAddressId)
    {
        var isSuccess = await customerDBRepository.UpdateCustomerDefaultAddress(customerId, customerAddressId);
        return isSuccess;
    }

    public async Task<bool> DeleteCustomer(int companyId, int customerId)
    {
        var isSuccess = await customerRepository.Delete(companyId, customerId);
        return isSuccess;
    }

    public async Task<bool> DeleteCustomerAddress(ARCustomerAddress customerAddress)
    {
        
        var isSuccess = await customerAddressRepository.Delete(customerAddress);
        return isSuccess;
    }

    public async Task<ARCustomerDetailVM> GetCustomerDetails(int companyId, int customerId)
    {
        var customerVM = await customerRepository.GetCustomerDetails(companyId,customerId);

        var customerNotes = await customerRepository.GetCustomerNotes(customerId, 10);
              
        var billingAddresses = await customerAddressRepository.GetCustomerAddresses(customerId, 1, null);
  
        var shippingAddresses = await customerAddressRepository.GetCustomerAddresses(customerId, 2, null);


        return new ARCustomerDetailVM 
            { Customer = customerVM,
              BillingAddresses = billingAddresses,
              ShippingAddresses = shippingAddresses,
              Notes = customerNotes,
            };
    }

    public async Task<ARCustomer> GetCustomer(int customerId)
    {
        var customer = await customerRepository.Get(customerId);

        return customer;
    }

    public async Task<ARCustomerAddress> GetCustomerDefaultAddress(int customerId, byte addressType)
    {
        var customerAddress = await customerAddressRepository.GetCustomerDefaultAddress(customerId, addressType);

        return customerAddress;
    }

    public async Task<ARCustomerAddress> GetCustomerAddress(int customerId, int addressId)
    {
        var customerAddress = await customerAddressRepository.Get(addressId);

        if (customerAddress == null || customerAddress.CustomerID != customerId)
            throw new BSInfrastructureException("invalid customer");

        return customerAddress;
    }

    public async Task<IEnumerable<ARCustomerAddress>> GetCustomerBillingAddresses(int customerId)
    {
        var billingAddresses = await customerAddressRepository.GetCustomerAddresses(customerId, 1, null);
        return billingAddresses;
    }

    public async Task<IEnumerable<ARCustomerAddress>> GetCustomerShippingAddresses(int customerId)
    {
        var shippingAddresses = await customerAddressRepository.GetCustomerAddresses(customerId, 2, null);
        return shippingAddresses;
    }

    //customer contacts

    public Task<IEnumerable<ARCustomerContactVM>> GetContacts(int companyId, int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<ARCustomerContactDetailVM> GetContactInformation(int companyId, int customerId, int contactId)
    {
        throw new NotImplementedException();
    }

    public Task<ResultVM> SaveCustomerContact(ARCustomerContact contact)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCustomerContact(int companyId, int customerId, int customerContactId)
    {
        throw new NotImplementedException();
    }
}
