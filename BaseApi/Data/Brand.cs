using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseApi.Data
{
    public class Brand : BaseEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int ID { get; set; }
        public required string BrandName { get; set; }
    }
}
