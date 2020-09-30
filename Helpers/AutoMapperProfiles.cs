using System.Linq;
using AutoMapper;
using CP.API.Dto;
using CP.API.Model;

namespace SAMMAPP.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryRegisterDTO,Category>();
            CreateMap<Category,CategoryUpdateDTO>();
            CreateMap<Category,CategoryReturnDTO>()
            .ForMember(d => d.SubCategorys, m => m.MapFrom(s => s.SubCategorys));
            CreateMap<ProductForUpdateDTO,Product>();
            CreateMap<Product,ProductForUpdateDTO>();
            CreateMap<ProductRegisterDTO ,Product>();
            CreateMap< Product ,ProductReturnDTO>()
            .ForMember(dest=>dest.PhotoURL,opt=>{opt.MapFrom(src=>src.PhotoForProducts.FirstOrDefault(p=>p.IsMain).Url);})
            .ForMember(d => d.Photos, m => m.MapFrom(s => s.PhotoForProducts))
            .ForMember(d => d.VendorName, m => m.MapFrom(s => s.Vendor.UserName))
            .ForMember(d => d.SubCategoryName, m => m.MapFrom(s => s.SubCategory.subCategoryName));
            
            CreateMap<PaymentRegisterDTO,PaymentType>();
            CreateMap<PaymentType,PaymentReturnDTO>();
            CreateMap<ShipperRegisterDTO,Shipper>();
            CreateMap<Shipper,ShipperReturnDTO>();


            
            CreateMap<UserRegisterDTO,User>();
            CreateMap<UserForUpdateDTO,User>();
            CreateMap<User,UserReturnDTO>()
            .ForMember(dest=>dest.PhotoURL,opt=>{opt.MapFrom(src=>src.PhotoForSuppliers.FirstOrDefault(p=>p.IsMain).Url);})
            //.ForMember(d => d.Products, m => m.MapFrom(s => s.Products))
            .ForMember(d => d.Photos, m => m.MapFrom(s => s.PhotoForSuppliers));


            CreateMap<VendorRegisterDTO, Vendor>();
            CreateMap<VendorUpdateDTO, Vendor>();
            CreateMap<Vendor, VendorUpdateDTO>();

            CreateMap<OrderDetailRegisterDTO , OrderDetail>();
            CreateMap<OrderDetail,OrderDetailReturnDTO>();
            
            // .ForMember(d => d.ProductName, m => m.MapFrom(s => s.Product.));

            CreateMap<OrderRegisterDTO,Order>();
            CreateMap<Order,OrderDetailReturnDTO>();

            CreateMap<PhotoForSupplier,PhotoForDetailsDto>();
            CreateMap<PhotoForSupplier,PhotoForReturnDto>();
            CreateMap<PhotoForCreateDto,PhotoForSupplier>();

            CreateMap<PhotoForProduct,PhotoForDetailsDto>();
            CreateMap<PhotoForProduct,PhotoForReturnDto>();
            CreateMap<PhotoForCreateDto,PhotoForProduct>();
            // .ForMember(x =>x.Products, opt => opt.Ignore());
            // .ForMember(dest => dest.PhotoUrl, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); }).ForMember(dest => dest.Age, opt => { opt.ResolveUsing(src => src.DateOfBirth.CalculateAge()); });
            //  CreateMap<User, UserForDetailsDTO>()
            // .ForMember(dest => dest.PhotoUrl, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); })
            // .ForMember(dest => dest.Age, opt => { opt.ResolveUsing(src => src.DateOfBirth.CalculateAge()); });
            //  CreateMap<Photo, PhotoForDetailsDTO>();

             CreateMap<SubCategoryRegisterDTO,SubCategory>();
            CreateMap<SubCategoryUpdateDTO, SubCategory>();
            CreateMap<SubCategory, SubCategoryReturnDTO>()
                .ForMember(d => d.CategoryName, m => m.MapFrom(s => s.Category.CategoryName));

            CreateMap<CouponRegisterDTO,Coupon>();
            CreateMap<Coupon, CouponUpdateDTO>();
            CreateMap<Coupon,CouponReturnDTO>();


            CreateMap<SocialCommunicationRegisterDTO , SocialCommunication>();
            CreateMap<SocialCommunication,SocialCommunicationReturnDTO>();
        }
    }
}