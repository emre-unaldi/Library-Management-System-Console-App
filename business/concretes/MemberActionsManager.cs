using LibraryManagementSystem.business.abstracts;
using LibraryManagementSystem.dao;
using LibraryManagementSystem.entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraryManagementSystem.business.concretes
{
    public class MemberActionsManager : IMemberService
    {
        public void Add()
        {
            try
            {
                repeat:
                Console.WriteLine("|------------------- Kütüphane Üyesi Ekle -------------------|");
                Console.Write("Eklenecek üyenin adını girin : ");
                string firstName = Console.ReadLine().Trim();
                Console.Write("Eklenecek üyenin soyadı girin : ");
                string lastName = Console.ReadLine().Trim();
                Console.Write("Eklenecek üyenin üyelik numarasını girin : ");
                int membershipNumber = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Girilen bilgilerinin doğru olduğunu onaylıyor musunuz ?");
                Console.WriteLine($"(1) Evet, Kaydet\n(2) Kayıttan Vazgeç\n(3) Bilgileri Düzelt");
                int choose = Convert.ToInt32(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && int.IsPositive(membershipNumber))
                        {
                            Member member = new Member(firstName, lastName, membershipNumber);
                            Repository.Members.Add(member);
                            Console.WriteLine($"{member.FirstName} {member.LastName} kişisi üye olarak eklendi.");
                        }
                        else
                        {
                            Console.WriteLine("Üye adı, soyadı veya üyelik numarası geçerli değil!!!\nLütfen tekrar deneyin...");
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
            catch(Exception exception) 
            {
                Console.WriteLine($"Exception : {exception.Message}");
            } 
        }

        public void Delete()
        {
            try
            {
            repeat:
                Console.WriteLine("|------------------ Kütüphane Üyesini Sil -------------------|");
                MemberOperations.PrintMembers(Repository.Members, false, out bool continueExecution);
                if (!continueExecution) return;

                Console.Write("Silmek istediğiniz üyenin id numarasını giriniz : ");
                int deleteMemberId = Convert.ToInt32(Console.ReadLine().Trim());

                int searchIndex = Repository.Members.FindIndex((member) => member.Id == deleteMemberId);

                if (searchIndex != -1)
                {
                    Console.WriteLine($"{Repository.Members[searchIndex].FirstName} {Repository.Members[searchIndex].LastName} kişisi kütüphane üyeliğinden silinecektir. Onaylıyor musunuz ?");
                    Console.WriteLine("(1) Evet \n(2) Hayır");
                    int isDelete = Convert.ToInt32(Console.ReadLine());

                    if (isDelete == 1)
                    {
                        Console.WriteLine($"{Repository.Members[searchIndex].FirstName} {Repository.Members[searchIndex].LastName} kişisin üyeliği sonlandırıldı.");
                        Repository.Members.RemoveAt(searchIndex);
                        if (Repository.Members.Count == 0) Member.LastId = 0;
                    }
                    else
                        return;
                } 
                else
                {
                    Console.WriteLine("Aradığınız kişi kütüphane üyelerinde bulunmamaktadır.Tekrar denemek ister misiniz ?");
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
                Console.WriteLine("|---------------- Kütüphane Üyesini Güncelle ----------------|");
                MemberOperations.PrintMembers(Repository.Members, false, out bool continueExecution);
                if (!continueExecution) return;

                Console.Write("Güncellemek istediğiniz kütüphane üyesinin id numarasını giriniz : ");
                int updateMemberId = Convert.ToInt32(Console.ReadLine().Trim());

                int searchIndex = Repository.Members.FindIndex((member) => member.Id == updateMemberId);

                if(searchIndex != -1)
                {
                    updateRepeat:
                    Console.WriteLine($"{Repository.Members[searchIndex].FirstName} {Repository.Members[searchIndex].LastName} üyesi üyeler içinde bulundu.");
                    Console.WriteLine("Kütüphane üyesinin hangi bilgisini güncellemek istiyorsunuz ?");
                    Console.WriteLine("(1) AD\n(2) SOYAD\n(3) ÜYELİK NUMARASI\n(4) Tüm Bilgiler\n(0) Güncellemekten Vazgeç");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 0:
                            return;
                        case 1:
                            firstNameRepeat:
                            Console.Write("Üyenin yeni adını girin : ");
                            string updateFirstName = Console.ReadLine().Trim();
                            
                            if(!string.IsNullOrEmpty(updateFirstName))
                            {
                                Repository.Members[searchIndex].FirstName = updateFirstName;
                                Console.WriteLine($"Üyenin adı {Repository.Members[searchIndex].FirstName} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Üye adı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto firstNameRepeat;
                            }
                            break;
                        case 2:
                            lastNameRepeat:
                            Console.Write("Üyenin yeni soyadını girin : ");
                            string updateLastName = Console.ReadLine().Trim();

                            if (!string.IsNullOrEmpty(updateLastName))
                            {
                                Repository.Members[searchIndex].FirstName = updateLastName;
                                Console.WriteLine($"Üyenin soyadı {Repository.Members[searchIndex].LastName} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Üye adı geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto lastNameRepeat;
                            }
                            break;
                        case 3:
                            membershipNumberRepeat:
                            Console.Write("Üyenin yeni üyelik numarası girin : ");
                            int updateMembershipNumber = Convert.ToInt32(Console.ReadLine().Trim());

                            if (int.IsPositive(updateMembershipNumber))
                            {
                                Repository.Members[searchIndex].MembershipNumber = updateMembershipNumber;
                                Console.WriteLine($"Üyenin üyelik numarası {Repository.Members[searchIndex].MembershipNumber} olarak güncellenmiştir.");
                            }
                            else
                            {
                                Console.WriteLine("Üyelik numarası geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto membershipNumberRepeat;
                            }
                            break;
                        case 4:
                            allUpdateMember:
                            Console.Write("Üyenin yeni adını girin : ");
                            string allUpdateFirstName = Console.ReadLine().Trim();

                            Console.Write("Üyenin yeni soyadını girin : ");
                            string allUpdateLastName = Console.ReadLine().Trim();

                            Console.Write("Üyenin yeni üyelik numarası girin : ");
                            int allUpdateMembershipNumber = Convert.ToInt32(Console.ReadLine().Trim());

                            if(!string.IsNullOrEmpty(allUpdateFirstName) && !string.IsNullOrEmpty(allUpdateLastName) && int.IsPositive(allUpdateMembershipNumber))
                            {
                                Repository.Members[searchIndex].FirstName = allUpdateFirstName;
                                Repository.Members[searchIndex].LastName = allUpdateLastName;
                                Repository.Members[searchIndex].MembershipNumber = allUpdateMembershipNumber;

                                Console.WriteLine(
                                    $"Ad: {Repository.Members[searchIndex].FirstName}\n" +
                                    $"Soyad: {Repository.Members[searchIndex].LastName}\n" +
                                    $"Üyelik Numarası: {Repository.Members[searchIndex].MembershipNumber} olarak üye bilgileri güncellenmiştir."
                                );
                            }
                            else
                            {
                                Console.WriteLine("Üyelik adı, soyadı veya üyelik numarası geçerli değil!!!\nLütfen tekrar deneyin...");
                                goto allUpdateMember;
                            }
                            break;
                        default:
                            Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                            goto updateRepeat;
                    }
                } 
                else
                {
                    Console.WriteLine("Aradığınız kişi kütüphane üyelerinde bulunmamaktadır.Tekrar denemek ister misiniz ?");
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
                List<Member> sortedMemberList = new List<Member>();

                while (!isExit)
                {
                    Console.WriteLine("|--------------- Kütüphane Üyelerini Listele ----------------|");
                    Console.WriteLine("(1) A-Z'ye Listele");
                    Console.WriteLine("(2) Z-A'ya Listele");
                    Console.WriteLine("(0) Ana Menüye Dön");
                    Console.Write("Kütüphane üyelerini nasıl sıralamak istediğinizi seçiniz : ");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 0:
                            isExit = true;
                            break;
                        case 1:
                            sortedMemberList = Repository.Members.OrderBy(member => member.FirstName).ToList();
                            break;
                        case 2:
                            sortedMemberList = Repository.Members.OrderByDescending(member => member.FirstName).ToList();
                            break;
                        default:
                            Console.WriteLine("Lütfen doğru bir seçim yapınız!!!");
                            break;
                    }
                    MemberOperations.PrintMembers(Repository.Members, true, out bool continueExecution);
                    if (!continueExecution) return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception : {exception.Message}");
            }
        }   
    }

    public struct MemberOperations
    {
        public static void PrintMembers(List<Member> memberList, bool isBookList, out bool continueExecution)
        {
            if (memberList.Count != 0)
            {
                continueExecution = true;
                foreach (Member member in memberList)
                {
                    Console.WriteLine("|------------------------------------------------------------|");
                    Console.WriteLine(
                        $"Id: {member.Id}\n" +
                        $"Ad: {member.FirstName}\n" +
                        $"Soyad: {member.LastName}\n" +
                        $"Üyelik Numarası: {member.MembershipNumber}"
                    );
                    if (member.BorrowedBooks.Count > 0 && isBookList)
                    {
                        Console.WriteLine(
                            $"Ödünç Alınan Kitap Sayısı: {member.BorrowedBooks.Count}\n" +
                            $"|----------> Ödünç Alınan Kitaplar"
                        );
                        foreach (Book book in member.BorrowedBooks)
                        {
                            Console.WriteLine($"Kitap Adı : {book.Name}");
                        }
                    }
                    Console.WriteLine("|------------------------------------------------------------|");
                }
            }
            else
            {
                Console.WriteLine("Kütüphanede üye bulunmamaktadır.");
                continueExecution = false;
            }
        }
    }
}

