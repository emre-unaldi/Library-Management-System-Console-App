using System;

namespace LibraryManagementSystem.business.abstracts
{
    public interface IMemberService
    {
        public void Add();
        public void Delete();
        public void Update();
        public void GetAll();
    }
}
