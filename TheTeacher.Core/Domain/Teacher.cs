namespace TheTeacher.Core.Domain
{
    public enum ExperienceLevel { Basic, }
    public class Teacher
    {
        public string Subject { get; protected set; }
        public string Adress { get; protected set; }
        public decimal PricePerHour { get; protected set; }
        public string Experience { get; protected set; }
        // AvailableDays    
    }
}