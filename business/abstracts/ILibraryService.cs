using System;

namespace LibraryManagementSystem.business.abstracts
{
    public interface ILibraryService
    {
        public void BookLending();
        public void BookDelivery();
    }
}
