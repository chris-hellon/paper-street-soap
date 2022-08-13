using Newtonsoft.Json;

namespace PaperStreetSoap.Core.DTOs.Requests
{
    public class CreateOpenNodeChargeRequest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "USD";

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("notif_email")]
        public string NotificationEmail { get; set; }

        [JsonProperty("success_url")]
        public string SuccessUrl { get; set; }

        [JsonProperty("auto_settle")]
        public bool AutoSettle { get; set; } = false;

        public CreateOpenNodeChargeRequest(string description, decimal amount, int orderId, string customerName, string customerEmail, string notificationEmail, string successUrl)
        {
            Description = description;
            Amount = amount;
            OrderId = orderId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            NotificationEmail = notificationEmail;
            SuccessUrl = successUrl;
        }
    }
}