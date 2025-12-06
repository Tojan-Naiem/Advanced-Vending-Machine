using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Stripe.Checkout;

using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using VendingMachine.BLL.Service.Interfaces;

namespace VendingMachine.BLL.Service.Classes
{
    public class CheckOutService:ICheckOutService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEmailSender _emailSender;
        public CheckOutService(
            ITransactionRepository transactionRepository,
            IEmailSender emailSender
            )
        {
            _transactionRepository = transactionRepository;
            _emailSender = emailSender;
        }

        //public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        //{
        //    var order = await _orderRepository.GetUserByOrderId(orderId);
        //    var subject = "";
        //    var body = "";
        //    if (order.PaymentMethod == PaymentMethod.Visa)
        //    {
        //        subject = "Payment Successful";
        //        body = $"Thank u for ur payment , ur payment for order {orderId}, total amount={order.TotalAmount}";
        //    }
        //    else if (order.PaymentMethod == PaymentMethod.Cash)
        //    {
        //        subject = "Order Successful";
        //        body = $"Thank u for ur order , ur order  {orderId}, total amount={order.TotalAmount}";
        //    }
        //    else return false;
        //    await _emailSender.SendEmailAsync(order.User.Email, subject, body);
        //    return true;


        //}

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, HttpRequest httpRequest)
        {
            var item = await _transactionRepository.GetTransactionAsync(request.transactionId);
            if (item is null)
            {
                return new CheckOutResponse()
                {
                    Success = false,
                    Message = "No Cart for this user"
                };
            }

            
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkout/success/{request.transactionId}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkout/cancel",
                };
               
                    options.LineItems.Add(
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "USD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name,
                                },
                                UnitAmount = (long)item.Product.Price,
                            },
                        }
                        );
                
                var service = new SessionService();
                var session = service.Create(options);
                return new CheckOutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully",
                    PaymentId = session.Id,
                    Url = session.Url
                };
            
          
        }
    }
}
