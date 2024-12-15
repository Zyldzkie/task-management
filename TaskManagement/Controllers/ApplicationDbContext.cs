internal class ApplicationDbContext
{
    public object Users { get; internal set; }

    internal void SaveChanges()
    {
        throw new NotImplementedException();
    }
}