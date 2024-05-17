namespace BaseApi.Data
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey ID { get; set; }
        public DateTime createDate { get; set; }
        public string? createUserId { get; set; }
        public DateTime updateDate { get; set; }
        public string? updateUserId { get; set; }
        public bool deleted { get; set; }
    }
}
