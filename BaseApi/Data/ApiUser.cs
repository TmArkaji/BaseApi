using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BaseApi.Data
{
    public class ApiUser
    {
        public required string ID { get; set; }
        [StringLength(2)]
        public required string CountryA2 { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string ApiKey { get; set; }

        public DateTime createDate { get; set; }
        public string? createUserId { get; set; }
        public DateTime updateDate { get; set; }
        public string? updateUserId { get; set; }
        public bool deleted { get; set; }
    }
}
