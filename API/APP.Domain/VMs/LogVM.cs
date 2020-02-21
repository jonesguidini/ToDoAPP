using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class LogVM : BaseEntity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}