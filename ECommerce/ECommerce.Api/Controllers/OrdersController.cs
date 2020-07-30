using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.JWT;
using ECommerce.Api.Models;
using ECommerce.Business.Abstract;
using ECommerce.Entity.Models;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public OrdersController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }

        /// <summary>
        /// Getting Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOrders()
        {
            int UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ECommerceClaimTypes.UserId)?.Value);

            var user = _userService.GetById(UserId);

            if (user.Role == Role.Admin)
            {
                return Ok(_orderService.Quarayable
                    .Include(x => x.User)
                    .Include(x => x.OrderItems)
                        .ThenInclude(x => x.Product)
                        .OrderByDescending(x => x.Id)
                        .Select(x => new
                        {
                            x.Id,
                            x.OrderDate,
                            x.Name,
                            x.Surname,
                            x.Address,
                            x.City,
                            x.Phone,
                            x.Email,
                            x.ConcersationId,
                            OrderState = x.OrderState.ToString(),
                            PaymentType = x.PaymentType.ToString(),
                            OrderItems = x.OrderItems.Select(i => new
                            {
                                i.Product.Name,
                                i.Product.ImageUrl,
                                i.Quantity,
                                i.Price
                            }).ToList()
                        }).ToList());
            }
            else
            {
                return Ok(_orderService.Quarayable
              .Include(x => x.OrderItems)
                  .ThenInclude(x => x.Product)
              .OrderByDescending(x => x.Id)
              .Where(x => x.UserId == UserId)
              .Select(x => new
              {
                  x.Id,
                  x.OrderDate,
                  x.Name,
                  x.Surname,
                  x.Address,
                  x.City,
                  x.Phone,
                  x.Email,
                  x.ConcersationId,
                  OrderState = x.OrderState.ToString(),
                  PaymentType = x.PaymentType.ToString(),
                  OrderItems = x.OrderItems.Select(i => new
                  {
                      i.Product.Name,
                      i.Product.ImageUrl,
                      i.Quantity,
                      i.Price
                  }).ToList()
              }).ToList());

            }



        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] OrderModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            int UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ECommerceClaimTypes.UserId)?.Value);

            // Odeme

            var payment = PaymentProcess(model, UserId);

            if (payment.Status.ToUpper() == "SUCCESS")
            {
                await SaveOrder(model, payment, UserId);

                return Ok(new ResponseModel()
                {
                    Message = "Payment Success",
                    Status = 200,
                    Result = true

                });
            }
            else
            {
                return Ok(new ResponseModel()
                {
                    Message = "Payment Fail",
                    Status = 400,
                    Result = false

                });
            }

        }

        [NonAction]
        private async Task SaveOrder(OrderModel model, Payment payment, int userId)
        {
            var order = new Order()
            {
                OrderNumber = new Random().Next(111111, 9999999).ToString(),
                OrderState = EnumOrderState.Complated,
                OrderDate = DateTime.Now,
                PaymentType = (EnumPaymentType)model.PaymentType,
                PaymentId = payment.PaymentId,
                UserId = userId,
                Name = model.Name,
                Surname = model.Surname,
                Address = model.Address,
                City = model.City,
                Phone = model.Phone,
                Email = model.Email,
                ConcersationId = payment.ConversationId,
                OrderItems = model.OrderItems.Select(x => new OrderItem()
                {
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ProductId = x.ProductId
                }).ToList()
            };

            await _orderService.CreateAsync(order);

            foreach (var item in model.OrderItems)
            {
                var product = _productService.GetById((int)item.ProductId);
                product.Stock -= (int)item.Quantity;
                _productService.Update(product);
            }

        }

        [NonAction]
        private Payment PaymentProcess(OrderModel model, int UserId)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-fWpwcVa0tG93cq6igdWpm1kz4v0sgdNd";
            options.SecretKey = "sandbox-SVlpc8AbaMdgUNxFyCMX4M2cImZut8KH";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";


            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.OrderItems.Sum(x => (x.Price * x.Quantity)).ToString();
            request.PaidPrice = (model.OrderItems.Sum(x => (x.Price * x.Quantity)) + 1).ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpireMonth.ToString();
            paymentCard.ExpireYear = model.ExpireYear.ToString();
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = UserId.ToString();
            buyer.Name = model.Name;
            buyer.Surname = model.Surname;
            buyer.GsmNumber = model.Phone;
            buyer.Email = model.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = model.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = model.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = model.Name + " " + model.Surname;
            shippingAddress.City = model.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.Address;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = model.Name + " " + model.Surname;
            billingAddress.City = model.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model.Address;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in model.OrderItems)
            {
                var product = _productService.GetById((int)item.ProductId);

                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.ProductId.ToString();
                firstBasketItem.Name = product.Name;
                firstBasketItem.Category1 = "Collectibles";
                firstBasketItem.Category2 = "Accessories";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = (item.Price * item.Quantity).ToString();
                basketItems.Add(firstBasketItem);
            }



            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
    }
}
