using System;

namespace LibraryManagementSystem.entities
{
    public class Book
    {
        private static int lastId = 0;
        private int id;
        private string name;
        private string author;
        private DateTime releaseYear;

        public Book(string _name, string _author, DateTime _releaseYear)
        {
            this.id = ++lastId;
            this.name = _name;
            this.author = _author;
            this.releaseYear = _releaseYear;
        }

        public static int LastId { set => lastId = value; }
        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public DateTime ReleaseYear { get => releaseYear; set => releaseYear = value; }
    }
}
