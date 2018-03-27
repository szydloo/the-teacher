namespace TheTeacher.Infrastructure.EntityFramework
{
    public class SqlSettings
    {
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
    }
}