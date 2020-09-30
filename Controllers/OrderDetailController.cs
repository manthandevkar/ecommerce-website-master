using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CP.API.Data;
using CP.API.Dto;
using CP.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CP.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public OrderDetailController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }
        //hhh
        [HttpGet("{id}", Name = "GetOrderDetail")]
        public async Task<IActionResult> GetOrderDetail(int customerId, int id)
        {
            if (customerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var orderDetailFroRepo = await _repo.GetOrderDetail(id);
            var post = _mapper.Map<OrderDetailReturnDTO>(orderDetailFroRepo);
            if ((orderDetailFroRepo == null))
                return NotFound();
            return Ok(orderDetailFroRepo);
        }

         [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            var orderDetails = await _repo.GetOrderDetails();
            var orderDetailsToReturn = _mapper.Map<IEnumerable<OrderDetailReturnDTO>>(orderDetails);
            if ((orderDetails  == null))
                return NotFound();
            return Ok(orderDetailsToReturn);
        }

         [HttpPost]
        public async Task<IActionResult> CreateOrderDetails(int customerId, OrderDetailRegisterDTO orderDetailRegisterDTO)
        {
            if (customerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var orderDetails = _mapper.Map<OrderDetail>(orderDetailRegisterDTO);
            _repo.Add(orderDetails);
            if (await _repo.SaveAll())
            {
                var orderDetailsToReturn = _mapper.Map<OrderDetailReturnDTO>(orderDetails);
                return CreatedAtRoute("GetCategory", new { id = orderDetails.OrderId }, orderDetailsToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ الرسالة الجديده");
        }

        [HttpGet("count")]
         public async Task<IActionResult> GetOrderNowCount()
         {
             var count = await _repo.GetOrderNowCount();
             return Ok (count);
         }

         [HttpGet("sumTotalOrder")]
         public async Task<IActionResult> GetsumTotalOrder()
         {
             var count = await _repo.GetOrderTotalCount();
             return Ok (count);
         }

        [HttpPost("PushThroughStripe/{totalOrderAmount}/{stripeToken}")]
        public async Task<IActionResult> PushThroughStripe(long totalOrderAmount,  string stripeToken)

        {
            var customers = new CustomerService();
            var charges = new ChargeService();


            // var options = new TokenCreateOptions
            // {
            // Card = new CreditCardOptions
            //     {
            //         // Number = "4242424242424242",
            //         // ExpYear = 2020,
            //         // ExpMonth = 3,
            //         // Cvc = "123"
            //     }
            // };                                   //AllThisInFront-End

            // var service = new TokenService();
            // Token stripeToken = service.Create(options);


            var customer = customers.Create(new CustomerCreateOptions
            {
                Source = stripeToken
            });
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = totalOrderAmount,
                Description = "Pay the order",
                Currency = "usd", // select the Currency //as a location the customer 
                Customer = customer.Id
            });
            var payment = new Payment
            {
                PaymentDate = DateTime.Now,
                Amount = charge.Amount / 100, //if $
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                ReceiptUrl = charge.ReceiptUrl,
                Description = charge.Description,
                Currency = charge.Currency,
                IsPaid = charge.Paid
            };
            _repo.Add<Payment>(payment);
            if (await _repo.SaveAll())
            {
                return Ok(new { IsPaid = charge.Paid });
            }

            return BadRequest("Fail Charge");
        }



    }
}