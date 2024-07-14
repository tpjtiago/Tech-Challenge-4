using Tech.Challenge4.Domain.Contracts.Services.Entity;

namespace Tech.Challenge4.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
