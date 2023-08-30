namespace FileSharingApp.Areas.Admin.Models
{
    public class ContactUsAdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }

    }
}
