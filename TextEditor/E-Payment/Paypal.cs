using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Payment
{
    public class Paypal
    {
        public async Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            // var envProd = new LiveEnvironment(_configuration["PaypalProduction:ClientId"],
            //     _configuration["PaypalProduction:SecretKey"]);

            var envSandbox =
                new SandboxEnvironment(_configuration["Paypal:ClientId"], _configuration["Paypal:SecretKey"]);
            var client = new PayPalHttpClient(envSandbox);
            var paypalOrderId = DateTime.Now.Ticks;
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
            var payment = new PayPal.v1.Payments.Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
        {
            new Transaction()
            {
                Amount = new Amount()
                {
                    Total = ConvertVndToDollar(model.Amount).ToString(),
                    Currency = "USD",
                    Details = new AmountDetails
                    {
                        Tax = "0",
                        Shipping = "0",
                        Subtotal = ConvertVndToDollar(model.Amount).ToString(),
                    }
                },
                ItemList = new ItemList()
                {
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = " | Order: " + model.OrderDescription,
                            Currency = "USD",
                            Price = ConvertVndToDollar(model.Amount).ToString(),
                            Quantity = 1.ToString(),
                            Sku = "sku",
                            Tax = "0",
                            Url = "https://www.code-mega.com" // Url detail of Item
                        }
                    }
                },
                Description = $"Invoice #{model.OrderDescription}",
                InvoiceNumber = paypalOrderId.ToString()
            }
        },
                RedirectUrls = new RedirectUrls()
                {
                    ReturnUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=1&order_id={paypalOrderId}",
                    CancelUrl =
                        $"{urlCallBack}?payment_method=PayPal&success=0&order_id={paypalOrderId}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            var request = new PaymentCreateRequest();
            request.RequestBody(payment);

            var paymentUrl = "";
            var response = await client.Execute(request);
            var statusCode = response.StatusCode;

            if (statusCode is not (HttpStatusCode.Accepted or HttpStatusCode.OK or HttpStatusCode.Created))
                return paymentUrl;

            var result = response.Result<Payment>();
            using var links = result.Links.GetEnumerator();

            while (links.MoveNext())
            {
                var lnk = links.Current;
                if (lnk == null) continue;
                if (!lnk.Rel.ToLower().Trim().Equals("approval_url")) continue;
                paymentUrl = lnk.Href;
            }

            return paymentUrl;
        }
    }
}
