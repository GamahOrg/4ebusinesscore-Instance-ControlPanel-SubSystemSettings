namespace Infrastructure.Repostories
{
    public class LK_NoteTypesRepository : GRepository<LK_NoteTypes>, ILK_NoteTypesRepository
    {
        public LK_NoteTypesRepository(DBContext context) : base(context)
        {

        }
    }
}
