namespace Dme.Persistence.Models.Models
{
    public class UserEntity
    {
        public long Id { get; set; }

        public int GenderId { get; set; }
        public int NationalityId { get; set; }

        public int NameId { get; set; }
		public NameEntity Name { get; set; }
        public ICollection<LocationEntity> Location { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public DateTimeOffset Registered { get; set; }
        public ContactEntity Contact { get; set; }

        public ICollection<DocumentEntity> Document { get; set; }
		public ICollection<PictureEntity> Picture { get; set; }
    }
}
