namespace CoreWebApis.Models
{
    public class Employees : IEmployees
    {
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
    }
}
