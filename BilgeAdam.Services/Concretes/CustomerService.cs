using BilgeAdam.Common.Dtos;
using BilgeAdam.Data.Context;
using BilgeAdam.Data.Entities;
using BilgeAdam.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdam.Services.Concretes
{
    internal class CustomerService:ICustomerService
    {
        private readonly NorthwindDbContext dbContext;

        public CustomerService(NorthwindDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PagedList<List<CustomerListDto>> GetPagedCustomers(int count, int page)
        {
            var data = dbContext.Customers.Skip((page - 1) * count).Take(count).Select(x => new CustomerListDto
            {
                Id = x.CustomerID,
                Address = $"{x.Address}, {x.Region}, {x.City}/{x.Country.ToUpper()}",
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Title = x.ContactTitle,
                Fax = x.Fax,
                Phone = x.Phone,
            }).ToList();
            var totalCount = dbContext.Customers.Skip(page * count).Count();
            return new PagedList<List<CustomerListDto>>() { Data = data, TotalCount = totalCount };
        }
        public CustomerDto GetCustomerById(string id)
        {
            return dbContext.Customers.Where(x => x.CustomerID == id).Select(x => new CustomerDto
            {
                Address = x.Address,
                City = x.City,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                ContactTitle = x.ContactTitle,
                Country = x.Country,
                Fax = x.Fax,
                Phone = x.Phone,
                PostalCode = x.PostalCode,
                Region = x.Region,

            }).SingleOrDefault();
        }

        public bool AddNewCustomer(CustomerAddDto dto)
        {
            var @new = new Customer
            {   
                CustomerID = dto.CustomerID,
                Address = dto.Address,
                City = dto.City,
                CompanyName = dto.CompanyName,
                ContactName = dto.ContactName,
                ContactTitle = dto.ContactTitle,
                Country = dto.Country,
                Fax = dto.Fax,
                Phone = dto.Phone,
                PostalCode = dto.PostalCode,
                Region = dto.Region,
            };
            dbContext.Add(@new);
            return dbContext.SaveChanges() > 0;

          
        }

        public bool RemoveCustomer(string id)
        {
            var entity = dbContext.Customers.SingleOrDefault(x => x.CustomerID == id);
            if (entity is null)
            {
                return false;
            }
            dbContext.Customers.Remove(entity);
            return dbContext.SaveChanges() > 0;


        }

        public bool UpdateCustomer(string id,CustomerAddDto dto)
        {
            var data = dbContext.Customers.FirstOrDefault(x => x.CustomerID == id);
            if (data is null)
            {
                return false;
            }
            data.Address = dto.Address;
            data.City = dto.City;
            data.CompanyName = dto.CompanyName;
            data.ContactName = dto.ContactName;
            data.ContactTitle = dto.ContactTitle;
            data.Country = dto.Country;
            data.Fax = dto.Fax;
            data.Phone = dto.Phone;
            data.PostalCode = dto.PostalCode;
            data.Region = dto.Region;

            dbContext.Update(data);

            return dbContext.SaveChanges() > 0;
        }
    }
}
