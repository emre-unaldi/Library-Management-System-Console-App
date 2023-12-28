using System;

namespace LibraryManagementSystem.entities
{
    public class Member
    {
        private static int lastId = 0;
        private int id;
        private string firstName;
        private string lastName;
        private int membershipNumber;
        private List<Book> borrowedBooks;

        public Member(string _firstName, string _lastName, int _membershipNumber)
        {
            this.id = ++lastId;
            this.firstName = _firstName;
            this.lastName = _lastName;
            this.membershipNumber = _membershipNumber;
            this.borrowedBooks = new List<Book>();
        }

        public static int LastId { set => lastId = value; }
        public int Id { get => id; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int MembershipNumber { get => membershipNumber; set => membershipNumber = value; }
        public List<Book> BorrowedBooks { get => borrowedBooks; set => borrowedBooks = value; }
    }
}
