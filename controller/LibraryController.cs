using LibraryManagementSystem.business.abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.controller
{
    public class LibraryController
    {
        IMemberService _memberService;
        ILibraryService _libraryService;
        IBookService _bookService;
        bool isExit = false;

        public LibraryController(IMemberService memberService, ILibraryService libraryService, IBookService bookService)
        {
            _memberService = memberService;
            _libraryService = libraryService;
            _bookService = bookService;
        }

        public bool IsExit { get => isExit; set => isExit = value; }

        public void Start()
        {
            try
            {
                while (!IsExit)
                {
                    repeat:
                    Console.WriteLine("|------------------------------------------------------------|");
                    Console.WriteLine("|---------------- Kütüphane Yönetim Sistemi -----------------|");
                    Console.WriteLine("|------------------------------------------------------------|");
                    Console.WriteLine(" (1) Üye İşlemleri");
                    Console.WriteLine(" (2) Kitap İşlemleri");
                    Console.WriteLine(" (3) Kütüphane İşlemleri");
                    Console.WriteLine(" (0) Çıkış Yap");
                    Console.WriteLine("|------------------------------------------------------------|");
                    Console.Write("Yapmak İstediğiniz işlemi Seçiniz : ");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 0:
                            this.IsExit = true;
                            Console.WriteLine("Uygulamadan Çıkış Yapıldı...");
                            break;
                        case 1:
                            MemberActions();
                            break;
                        case 2:
                            BookActions();
                            break;
                        case 3:
                            LibraryActions();
                            break;
                        default:
                            Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                            goto repeat;
                    }

                }
            }
            catch(Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }

        public void MemberActions()
        {
            memberActionsRepeat:
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine("|---------------------- Üye İşlemleri -----------------------|");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine(" (1) Kütüphane Üyesi Ekle");
            Console.WriteLine(" (2) Kütüphane Üyesini Sil");
            Console.WriteLine(" (3) Kütüphane Üyesini Güncelle");
            Console.WriteLine(" (4) Kütüphane Üyelerini Listele");
            Console.WriteLine(" (0) Çıkış Yap");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.Write("Yapmak İstediğiniz işlemi Seçiniz : ");
            int choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 0:
                    Console.WriteLine("Üye İşlemlerinden Çıkış Yapıldı...");
                    break;
                case 1:
                    this._memberService.Add();
                    goto memberActionsRepeat;
                case 2:
                    this._memberService.Delete();
                    goto memberActionsRepeat;
                case 3:
                    this._memberService.Update();
                    goto memberActionsRepeat;
                case 4:
                    this._memberService.GetAll();
                    goto memberActionsRepeat;
                default:
                    Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                    goto memberActionsRepeat;
            }
        }

        public void BookActions()
        {
            bookActionsRepeat:
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine("|--------------------- Kitap İşlemleri ----------------------|");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine(" (1) Kütüphaneye Kitap Ekle");
            Console.WriteLine(" (2) Kütüphaneden Kitap Sil");
            Console.WriteLine(" (3) Kütüphaneden Kitap Güncelle");
            Console.WriteLine(" (4) Kütüphane Kitaplarını Listele");
            Console.WriteLine(" (0) Çıkış Yap");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.Write("Yapmak İstediğiniz işlemi Seçiniz : ");
            int choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 0:
                    Console.WriteLine("Kitap İşlemlerinden Çıkış Yapıldı...");
                    break;
                case 1:
                    this._bookService.Add();
                    goto bookActionsRepeat;
                case 2:
                    this._bookService.Delete();
                    goto bookActionsRepeat;
                case 3:
                    this._bookService.Update();
                    goto bookActionsRepeat;
                case 4:
                    this._bookService.GetAll();
                    goto bookActionsRepeat;
                default:
                    Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                    goto bookActionsRepeat;
            }
        }

        public void LibraryActions()
        {
            libraryActionsRepeat:
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine("|------------------- Kütüphane İşlemleri --------------------|");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.WriteLine(" (1) Kütüphane Üyesine Kitap Ödünç Verme");
            Console.WriteLine(" (2) Kütüphane Üyesinden Kitap İadesi Alma");
            Console.WriteLine(" (0) Çıkış Yap");
            Console.WriteLine("|------------------------------------------------------------|");
            Console.Write("Yapmak İstediğiniz işlemi Seçiniz : ");
            int choose = Convert.ToInt32(Console.ReadLine());

            switch (choose)
            {
                case 0:
                    Console.WriteLine("Kütüphane İşlemlerinden Çıkış Yapıldı...");
                    break;
                case 1:
                    this._libraryService.BookLending();
                    goto libraryActionsRepeat;
                case 2:
                    this._libraryService.BookDelivery();
                    goto libraryActionsRepeat;
                default:
                    Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                    goto libraryActionsRepeat;
            }
        }
    }
}
