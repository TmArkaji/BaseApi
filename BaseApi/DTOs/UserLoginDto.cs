﻿using System.ComponentModel.DataAnnotations;

namespace BaseApi.DTOs
{
    public class UserLoginDto
    {
        public required string ID { get; set; }
        [StringLength(2)]
        public required string CountryA2 { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string ApiKey { get; set; }
    }
}
