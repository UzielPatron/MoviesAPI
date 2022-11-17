namespace MoviesAPI.Entitys.RelationEntitys
{
    public class MoviesActhors
    {
        public int MovieId { get; set; }
        public int ActhorId { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public Movie Movie { get; set; }
        public Acthor Acthor { get; set; }
    }
}
