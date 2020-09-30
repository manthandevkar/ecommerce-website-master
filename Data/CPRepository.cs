using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP.API.Dto;
using CP.API.Helpers;
using CP.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CP.API.Data
{
    public class CPRepository : ICPRepository
    {

        private readonly DataContext _context;
        public CPRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customer = await _context.Customers.ToListAsync();
            return customer;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(u => u.CustomerId == id);
            return customer;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.Include(p => p.SubCategorys).FirstOrDefaultAsync(u => u.CategoryId == id);
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategorys()
        {
            var categorys = await _context.Categories.Include(p => p.SubCategorys).ToListAsync();
            return categorys;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Include(s => s.Vendor).Include(e=>e.SubCategory).Include(w => w.PhotoForProducts).FirstOrDefaultAsync(u => u.ProductId == id);

            return product;
        }


        public async Task<PagedList<Product>> GetProducts(ProductParams productParams)
        {
            var products = _context.Products.Include(p => p.SubCategory).Include(s => s.Vendor).Include(w => w.PhotoForProducts).AsQueryable();

            products = products.Where(f => (f.ProductNumber == productParams.ProductNumber ||
                                           f.ProductNameEnglish == productParams.ProductNameEnglish ||
                                            f.ProductNameArabic == productParams.ProductNameArabic));
            return await PagedList<Product>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);

        }

        public async Task<PaymentType> GetPayment(int id)
        {
            var paymentTypes = await _context.PaymentTypes.FirstOrDefaultAsync(u => u.PaymentId == id);

            return paymentTypes;
        }

        public async Task<IEnumerable<PaymentType>> GetPayments()
        {
            var paymentTypes = await _context.PaymentTypes.ToListAsync();
            return paymentTypes;
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.Orders.Include(s => s.Shipper).Include(p => p.Payment).Include(p => p.OrderDetails).FirstOrDefaultAsync(u => u.OrderId == id);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Shipper> GetShipper(int id)
        {
            var shipper = await _context.Shippers.Include(o => o.Orders).FirstOrDefaultAsync(u => u.ShipperId == id);

            return shipper;
        }

        public async Task<PagedList<Shipper>> GetShippers(ShipperParams shipperParams)
        {
            var shippers = _context.Shippers.Include(o => o.Orders).AsQueryable();
            shippers = shippers.Where(f => (f.CompanyName == shipperParams.CompanyName ||
                                          f.Phone == shipperParams.Phone));


            return await PagedList<Shipper>.CreateAsync(shippers, shipperParams.PageNumber, shipperParams.PageSize);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(o=>o.PhotoForSuppliers).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(SupplierParams supplierParams)
        {
            var users = _context.Users.Include(o=>o.PhotoForSuppliers).AsQueryable();
            users = users.Where(f => 
                f.Phone == supplierParams.Phone  );


            return await PagedList<User>.CreateAsync(users, supplierParams.PageNumber, supplierParams.PageSize);
        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.Include(p => p.Product).FirstOrDefaultAsync(u => u.OrderDetailId == id);

            return orderDetail;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            var orderDetails = await _context.OrderDetails.Include(p => p.Product).ToListAsync();
            var count = orderDetails.Sum(s => s.Price);
            return orderDetails;
        }

        public async Task<PhotoForSupplier> GetPhoto(int id)
        {
            var photo = await _context.PhotoForSuppliers.FirstOrDefaultAsync(u => u.PhotoId == id);

            return photo;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategorys()
        {
            var sections = await _context.SubCategorys.Include(o => o.Products).ToListAsync();
            return sections;
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            var section = await _context.SubCategorys.Include(p => p.Products).FirstOrDefaultAsync(u => u.subCategoryID == id);

            return section;
        }

        public async Task<Coupon> GetCoupan(int id)
        {
            var discount = await _context.Coupons.FirstOrDefaultAsync(u => u.CoupanId == id);

            return discount;
        }

        public async Task<PagedList<Coupon>> GetCoupans(DiscountParams discountParams)
        {
            var discounts = _context.Coupons.AsQueryable();
            discounts = discounts.Where(f => (f.CoupanTitle == discountParams.DiscountName ||
                                           f.CouponCode == discountParams.CouponCode
                                                                                   ));


            return await PagedList<Coupon>.CreateAsync(discounts, discountParams.PageNumber, discountParams.PageSize);
        }

        public async Task<int> GetProductCount()
        {
            var products = await _context.Products.ToListAsync();
            var count = products.Count();
            return count;
        }

        public async Task<int> GetOrderCount()
        {
            var orders = await _context.Orders.ToListAsync();
            var count = orders.Count();
            return count;
        }

        public async Task<int> GetOrderNowCount()
        {
            var orders = await _context.OrderDetails.Where(o=>o.BillDate == DateTime.Now).ToListAsync();
            var count = orders.Count();
            return count;
        }

        public async Task<decimal> GetOrderTotalCount()
        {
            var orders = await _context.OrderDetails.SumAsync(o=>o.Total);
            
            return orders;
        }

       

       
        public async Task<IEnumerable<SocialCommunication>> GetSocialCommunications()
        {
            var socialCommunications = await _context.SocialCommunications.ToListAsync();
            return socialCommunications;
        }

        public async Task<SocialCommunication> GetSocialCommunication(int id)
        {
             var socialCommunication = await _context.SocialCommunications.FirstOrDefaultAsync(u => u.SocialCommunicationId == id);

            return socialCommunication;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoryWhereCatgeoryId(int catgeoryId)
        {
            var sectionWhereCatgeoryId = await _context.SubCategorys.Where(u => u.CategoryId == catgeoryId).Include(c=>c.Category).ToListAsync();

            return sectionWhereCatgeoryId;
        }

        public async Task<PhotoForSupplier> GetPhotoForSupplier(int id)
        {
            var photo = await _context.PhotoForSuppliers.IgnoreQueryFilters().FirstOrDefaultAsync(p=>p.PhotoId==id);
            return photo;
        }

        

        public async Task<PhotoForSupplier> GetMainPhotoForSupplier(int supplierId)
        {
             return await _context.PhotoForSuppliers.Where(u=>u.SupplierId==supplierId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public async Task<PhotoForProduct> GetPhotoForProduct(int id)
        {
            var photo = await _context.PhotoForProducts.FirstOrDefaultAsync(u => u.PhotoId == id);

            return photo;
        }

        public async Task<PhotoForProduct> GetMainPhotoForProduct(int productId)
        {
            return await _context.PhotoForProducts.Where(u=>u.ProductId==productId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public Task<Shipper> GetShipper(int id, bool isCurrentUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products.Include(o => o.PhotoForProducts).ToListAsync();
            return products;
        }

        public async Task<Vendor> GetVendor(int id)
        {
            var vendor = await _context.Vendors.Include(o => o.Products).FirstOrDefaultAsync(u => u.Id == id);
            return vendor;
        }

        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            var vendors = await _context.Vendors.Include(o => o.Products).ToListAsync();
            return vendors;
        }
    }
} 