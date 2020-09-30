using System.Collections.Generic;
using System.Threading.Tasks;
using CP.API.Dto;
using CP.API.Helpers;
using CP.API.Model;

namespace CP.API.Data
{
    public interface ICPRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();


        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);

        Task<Category> GetCategory(int id);
        Task<IEnumerable<Category>> GetCategorys();

        Task<Product> GetProduct(int id);
        Task<PagedList<Product>> GetProducts(ProductParams productParams);
        Task <IEnumerable<Product>> GetAllProducts();

        Task<PaymentType> GetPayment(int id);
        Task<IEnumerable<PaymentType>> GetPayments();

        Task<Order> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders();

        
        Task<Shipper> GetShipper(int id );
        Task<PagedList<Shipper>> GetShippers(ShipperParams supplierParams);

         Task<User> GetUser(int id );
        Task<PagedList<User>> GetUsers(SupplierParams supplierParams);
        Task<Vendor> GetVendor(int id);
        Task<IEnumerable<Vendor>> GetVendors();


        Task<OrderDetail> GetOrderDetail(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetails();

    

         Task<IEnumerable<SubCategory>> GetSubCategorys();

        Task <SubCategory> GetSubCategory(int id);
        Task<IEnumerable<SubCategory>> GetSubCategoryWhereCatgeoryId(int catgeoryId);


        Task<SocialCommunication> GetSocialCommunication(int id);
        Task<IEnumerable<SocialCommunication>> GetSocialCommunications();
           


        Task<Coupon> GetCoupan(int id);
        Task<PagedList<Coupon>> GetCoupans(DiscountParams discountParams);

        Task<int> GetProductCount();
        Task<int> GetOrderCount();

        Task<int> GetOrderNowCount();
        Task<decimal> GetOrderTotalCount();


        Task<PhotoForSupplier> GetPhotoForSupplier(int id);

        Task<PhotoForSupplier> GetMainPhotoForSupplier(int supplierId);


        Task<PhotoForProduct> GetPhotoForProduct(int id);

        Task<PhotoForProduct> GetMainPhotoForProduct(int productId);
         

    }
}