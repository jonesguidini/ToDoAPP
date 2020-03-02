using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class TodoVM : BaseEntity
    {
        public string Title { get; set; }
        public bool? IsDone { get; set; }
    }
}
