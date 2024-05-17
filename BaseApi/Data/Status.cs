namespace BaseApi.Data
{
    public class Status : BaseEntity<int>
    {
        public override int ID { get; set; }
        public required string StatusDescription { get; set; }
    }
}
