using LibraryManagementSystem.business.abstracts;
using LibraryManagementSystem.dao;
using LibraryManagementSystem.entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LibraryManagementSystem.business.concretes
{
    public class BookActionsManager : IBookService
    {
        public void Add()
        {
            try
            {
                repeat:
                Console.WriteLine("|------------------ Kütüphaneye Kitap Ekle ------------------|");
                Console.Write("Eklenecek kitabın adını girin : ");
                string name = Console.ReadLine().Trim();
                Console.Write("Eklenecek kitabın yazarını girin : ");
                string author = Console.ReadLine().Trim();
                Console.Write("Eklenecek kitabın yayınlanma yılını girin : ");
                string releaseYear = Console.ReadLine().Trim();
                DateTime dateTime;

                Console.WriteLine("Girilen bilgilerinin doğru olduğunu onaylıyor musunuz ?");
                Console.WriteLine($"(1) Evet, Kaydet\n(2) Kayıttan Vazgeç\n(3) Bilgileri Düzelt");
                int choose = Convert.ToInt32(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        if (
                            !string.IsNullOrEmpty(name) && 
                            !string.IsNullOrEmpty(author) && 
                            (DateTime.TryParseExact(releaseYear, "yyyy", null, System.Globalization.DateTimeStyles.None, out dateTime))
                        )
                        {
                            Book book = new Book(name, author, dateTime);
                            Repository.Books.Add(book);
                            Console.WriteLine($"{book.Name} adlı kitap kütüphaneye eklendi.");
                        }
                        else
                        {
                            Console.WriteLine("Kitap adı, yazarı veya yayınlanma yılı geçerli değil!!!\nLütfen tekrar deneyin...");
                            goto repeat;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Kayıt iptal edildi.");
                        return;
                    case 3:
                        goto repeat;
                    default:
                        Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                        break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }

        public void Delete()
        {
            try
            {
                repeat:
                Console.WriteLine("|------------------ Kütüphaneden Kitap Sil ------------------|");
                BookOperations.PrintBooks(Repository.Books, out bool continueExecution);
                if (!continueExecution) return;

                Console.Write("Silmek istediğiniz kitabın id numarasını giriniz : ");
                int deleteBookId = Convert.ToInt32(Console.ReadLine().Trim());

                int searchIndex = Repository.Books.FindIndex((book) => book.Id == deleteBookId);

                if (searchIndex != -1)
                {
                    Console.WriteLine($"{Repository.Books[searchIndex].Name} kitabı kütüphaneden silinecektir. Onaylıyor musunuz ?");
                    Console.WriteLine("(1) Evet \n(2) Hayır");
                    int isDelete = Convert.ToInt32(Console.ReadLine());

                    if (isDelete == 1)
                    {
                        Console.WriteLine($"{Repository.Books[searchIndex].Name} kitabı kütüphaneden kaldırıldı.");
                        Repository.Books.RemoveAt(searchIndex);
                        if (Repository.Books.Count == 0) Book.LastId = 0;
                    }
                    else
                        return;
                }
                else
                {
                    Console.WriteLine("Aradığınız kitap kütüphane kitaplarında bulunmamaktadır.Tekrar denemek ister misiniz ?");
                    Console.WriteLine("(1) Evet \n(2) Hayır");
                    int isRepeat = Convert.ToInt32(Console.ReadLine());

                    if (isRepeat == 1)
                        goto repeat;
                    else return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }

        public void Update()
        {
            try
            {
                repeat:
                Console.WriteLine("|---------------- Kütüphaneden Kitap Güncelle ---------------|");
                BookOperations.PrintBooks(Repository.Books, out bool continueExecution);
                if (!continueExecution) return;

                Console.Write("Güncellemek istediğiniz kütüphane kitabının id numarasını giriniz : ");
                int updateBookId = Convert.ToInt32(Console.ReadLine().Trim());

                int searchIndex = Repository.Books.FindIndex((book) => book.Id == updateBookId);

                if (searchIndex != -1)
                {
                    updateRepeat:
                    Console.WriteLine($"{Repository.Books[searchIndex].Name} adlı kitap kütüphane kitapları içinde bulundu.");
                    Console.WriteLine("Kütüphane kitabının hangi bilgisini güncellemek istiyorsunuz ?");
                    Console.WriteLine("(1) AD\n(2) YAZAR\n(3) YAYINLANMA YILI\n(4) Tüm Bilgiler\n(0) Güncellemekten Vazgeç");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 0:
                            return;
                        case 1:
                            nameRepeat:
                            Console.Write("Kitabın yeni adını girin : ");
                            string updateName = Console.ReadLine().Trim();

                            if (!string.IsNullOrEmpty(updateName))
                            {
                                Repository.Books[searchIndex].Name = updateName;
                                Console.WriteLine($"Kitabın adı {Repository.Books[searchIndex].Name} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Kitap adı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto nameRepeat;
                            }
                            break;
                        case 2:
                            authorRepeat:
                            Console.Write("Kitabın yeni yazarını girin : ");
                            string updateAuthor = Console.ReadLine().Trim();

                            if (!string.IsNullOrEmpty(updateAuthor))
                            {
                                Repository.Books[searchIndex].Author = updateAuthor;
                                Console.WriteLine($"Kitabın yazarı {Repository.Books[searchIndex].Author} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Kitap yazarı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto authorRepeat;
                            }
                            break;
                        case 3:
                            releaseYearRepeat:
                            Console.Write("Kitabın yeni Kitaplik numarası girin : ");
                            string updateReleaseYear = Console.ReadLine().Trim();
                            DateTime updateDateTime;

                            if (DateTime.TryParseExact(updateReleaseYear, "yyyy", null, System.Globalization.DateTimeStyles.None, out updateDateTime))
                            {
                                Repository.Books[searchIndex].ReleaseYear = updateDateTime;
                                Console.WriteLine($"Kitabın yayınlanma yılı {Repository.Books[searchIndex].ReleaseYear} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Kitabın yayınlanma yılı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto releaseYearRepeat;
                            }
                            break;
                        case 4:
                            allUpdateBook:
                            Console.Write("Kitabın yeni adını girin : ");
                            string allupdateName = Console.ReadLine().Trim();

                            Console.Write("Kitabın yeni soyadını girin : ");
                            string allupdateAuthor = Console.ReadLine().Trim();

                            Console.Write("Kitabın yeni Kitaplik numarası girin : ");
                            string allupdateReleaseYear = Console.ReadLine().Trim();
                            DateTime allUpdateDateTime;

                            if (
                                !string.IsNullOrEmpty(allupdateName) &&
                                !string.IsNullOrEmpty(allupdateAuthor) &&
                                (DateTime.TryParseExact(allupdateReleaseYear, "yyyy", null, System.Globalization.DateTimeStyles.None, out allUpdateDateTime))
                            )
                            {
                                Repository.Books[searchIndex].Name = allupdateName;
                                Repository.Books[searchIndex].Author = allupdateAuthor;
                                Repository.Books[searchIndex].ReleaseYear = allUpdateDateTime;

                                Console.WriteLine(
                                    $"Ad: {Repository.Books[searchIndex].Name}\n" +
                                    $"Yazar: {Repository.Books[searchIndex].Author}\n" +
                                    $"Yayınlanma Yılı: {Repository.Books[searchIndex].ReleaseYear.Year} olarak kitap bilgileri güncellenmiştir."
                                );
                            }
                            else
                            {
                                Console.WriteLine("Kitap adı, yazarı veya yayınlanma yılı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto allUpdateBook;
                            }
                            break;
                        default:
                            Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                            goto updateRepeat;
                    }
                }
                else
                {
                    Console.WriteLine("Aradığınız kitap kütüphane Kitaplerında bulunmamaktadır.Tekrar denemek ister misiniz ?");
                    Console.WriteLine("(1) Evet \n(2) Hayır");
                    int isRepeat = Convert.ToInt32(Console.ReadLine());

                    if (isRepeat == 1)
                        goto repeat;
                    else return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }

        public void GetAll()
        {
            try
            {
                bool isExit = false;
                List<Book> sortedBookList = new List<Book>();

                while (!isExit)
                {
                    Console.WriteLine("|-------------- Kütüphane Kitaplarını Listele ---------------|");
                    Console.WriteLine("(1) A-Z'ye Listele");
                    Console.WriteLine("(2) Z-A'ya Listele");
                    Console.WriteLine("(0) Ana MenKitap Dön");
                    Console.Write("Kütüphane kitaplarını nasıl sıralamak istediğinizi seçiniz : ");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 0:
                            isExit = true;
                            return;
                        case 1:
                            sortedBookList = Repository.Books.OrderBy(book => book.Name).ToList();
                            break;
                        case 2:
                            sortedBookList = Repository.Books.OrderByDescending(book => book.Name).ToList();
                            break;
                        default:
                            Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                            break;
                    }
                    BookOperations.PrintBooks(sortedBookList, out bool continueExecution);
                    if (!continueExecution) return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }
    }

    public struct BookOperations
    {
        public static void PrintBooks(List<Book> bookList, out bool continueExecution)
        {
            if (bookList.Count != 0)
            {
                continueExecution = true;
                foreach (Book book in bookList)
                {
                    Console.WriteLine("|------------------------------------------------------------|");
                    Console.WriteLine(
                        $"Id: {book.Id}\n" +
                        $"Ad: {book.Name}\n" +
                        $"Yazar: {book.Author}\n" +
                        $"Yayınlanma Yılı: {book.ReleaseYear.Year}"
                    );
                    Console.WriteLine("|------------------------------------------------------------|");
                }
            }
            else
            {
                Console.WriteLine("Kütüphanede kitap bulunmamaktadır.");
                continueExecution = false;
            }
        }
    }
}
