namespace CRUDApp.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
