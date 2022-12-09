namespace FarmFresh.Email.Models
{
    public static class TemplatesList
    {
        public static TemplatesDetails ForgotPassword { get; set; }
        static TemplatesList()
        {
            ForgotPassword = new TemplatesDetails("forgot-password.html", "Forgot Password");
        }
    }
    
    public class TemplatesDetails
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public TemplatesDetails(string _Name, string _Subject)
        {
            Name = _Name;
            Subject = _Subject;
        }
    }
}
