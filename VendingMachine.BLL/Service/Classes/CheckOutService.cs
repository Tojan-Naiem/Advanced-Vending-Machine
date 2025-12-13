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
        private readonly IProductRepository _productRepository;

        public CheckOutService(
            ITransactionRepository transactionRepository,
            IEmailSender emailSender,
             IProductRepository productRepository
            )
        {
            _transactionRepository = transactionRepository;
            _emailSender = emailSender;
            _productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(string session_id,int transactionId)
        {
            if (string.IsNullOrEmpty(session_id))
                return false;
            var item = await _transactionRepository.GetTransactionAsync(transactionId);
            if (item is null || item.Product is null) return false;
            var service = new SessionService();
            var session = service.Get(session_id);
            Product product = item.Product;
           await  _productRepository.DecreaseProductQuantity(product);
            string email = session.CustomerDetails?.Email;

            var subject = "Payment Successful";

            var body =
                $"Thank you for your purchase from our Vending Machine! " +
                $"We appreciate your trust and hope you enjoy your ice cream 🍦. " +
                $"Your payment for the product \"{item.Product.Name}\" was successful. " +
                $"Total amount paid: {item.Product.Price} USD.\n\n" +
                $"Best regards,\n" +
                $"Frozen Team\n" +
                $"Frozen Vending Machine";


            await _emailSender.SendEmailAsync(email, subject, body);
            return true;


        }

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
                    CustomerEmail = null,
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/v1/checkout/success/{{CHECKOUT_SESSION_ID}}/{request.transactionId}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/v1/checkout/cancel",
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
                                UnitAmount = (long)(item.Product.Price * 100),
                            },
                            Quantity=1
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
