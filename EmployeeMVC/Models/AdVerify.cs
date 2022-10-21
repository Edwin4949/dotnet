namespace EmployeeMVC.Models
{
    public class AdVerify
    {
        public int id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] Passwordhash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string VerificationToken { get; set; } = string.Empty;
    }
}
