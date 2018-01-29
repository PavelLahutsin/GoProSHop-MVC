using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;

namespace GoProShop.BLL.Services
{
    public class EmailService : BaseService, IEmailService
    {
             public EmailService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task SendSuccessOrderEmail(int orderId)
        {
            var order = await _uow.Orders.GetAsync(orderId);

            using (var mailMessage = new MailMessage())
            {
                mailMessage.To.Add(order.Customer.Email);
                mailMessage.Subject = "Информация о заказе";
                mailMessage.Body = GetHtmlBody(order);
                mailMessage.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }  
        }

        private static string GetHtmlBody(Order order)
        {
            var result = new StringBuilder();
            result.Append($"<style type=\"text/css\">td th {{padding: 10px;}}</style>");
            result.Append($"<h3>Здравствуйте, {order.Customer.Name}</h3>");
            result.Append($"<p> Ваш заказ <strong>№{order.Id}</strong>, размещенный на нашем сайте <a href=\"http://localhost:8888\">GoProShop</a> принят в обработку. <br/>" +
                $"Ожидайте доставку по указанному вами адресу. Общая стоимость <strong>{order.TotalPrice} рублей</strong></p>");
            result.Append("<h3>Информация о заказе</h3>");
            result.Append($"<p>Телефон: {order.Customer.Phone}<br/>");
            result.Append($"Адрес: {order.Customer.Address}<br/>");
            result.Append($"Email: {order.Customer.Email}</p>");
            result.Append("<h3>Состав заказа</h3>");
            result.Append("<table><tr><th  style=\"padding:10px;text-align: left;width: 200px\">Название</th><th style=\"padding:10px;text-align: left;width: 150px\">Количество</th><th style=\"padding:10px;text-align: left; width: 150px\">Цена</th></tr>");

            foreach (var product in order.OrdersList)
            {
                result.Append($"<tr><td style=\"padding:10px;\">{product.Product.Name}</td><td style=\"padding:10px;\">{product.Quantity}</td><td style=\"padding:10px;\">{product.Product.Price}</td></tr>");
            }
            result.Append("</table>");

            return result.ToString();
        }
    }
}
