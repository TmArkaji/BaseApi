using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Data
{
    public class ApiUserLogin
    {
        [Key]
        public int ID { get; set; }
        public string ApiUserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenStart { get; set; }
        public DateTime TokenEnd { get; set; }

        [ForeignKey("ApiUserId")]
        public ApiUser ApiUser { get; set; }
    }
}
