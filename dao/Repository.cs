using LibraryManagementSystem.entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraryManagementSystem.dao
{
    public static class Repository
    {
        private static List<Member> members = new List<Member>();
        private static List<Book> books = new List<Book>();

        static Repository() 
        {
            members.Add(new Member("Emre1", "Ünaldı1", 38));
            members.Add(new Member("Emre2", "Ünaldı2", 40));
            members.Add(new Member("Emre3", "Ünaldı3", 42));

            Books.Add(new Book("Harry Potter ve Zümrüdüanka Yoldaşlığı", "J.K Rowling", new DateTime(2003, 1, 1)));
            Books.Add(new Book("Harry Potter ve Melez Prens", "J.K Rowling", new DateTime(2005, 1, 1)));
            Books.Add(new Book("Harry Potter ve Ölüm Yadigârları", "J.K Rowling", new DateTime(2007, 1, 1)));
        }

        public static List<Member> Members { get => members; set => members = value; }
        public static List<Book> Books { get => books; set => books = value; }
    }
}
