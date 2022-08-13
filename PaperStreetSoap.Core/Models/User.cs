﻿namespace PaperStreetSoap.Core.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool HasSubscription { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}

