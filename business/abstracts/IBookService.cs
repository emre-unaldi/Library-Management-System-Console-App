using System;

namespace LibraryManagementSystem.business.abstracts
{
    public interface IBookService
    {
        public void Add();
        public void Delete();
        public void Update();
        public void GetAll();
    }
}
