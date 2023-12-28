using LibraryManagementSystem.business.concretes;
using LibraryManagementSystem.controller;
using System;

namespace LibraryManagementSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            LibraryController libraryController = new LibraryController(new MemberActionsManager(), new LibraryActionsManager(), new BookActionsManager());
            libraryController.Start();
        }
    }
}