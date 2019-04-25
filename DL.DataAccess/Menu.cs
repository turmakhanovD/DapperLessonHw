using DL.Models;
using System;
using System.Linq;

namespace DL.DataAccess
{
    public class Menu
    {
        public void StartMenu()
        {
            var mailRepository = new MailRepository();
            var mail = new Mail();
            var receiversRepository = new ReceiversRepository();
            var receiver = new Receiver();
            int chosenNumber = 0;

            Console.WriteLine("Mail.kz");
            Console.WriteLine("1.Add mail\n2.Delete mail\n3.Get all mails");
            Console.WriteLine("4.Add user\n5.Delete user\n6.Get all users");

            while (!int.TryParse(Console.ReadLine(), out chosenNumber))
            {
                Console.WriteLine("Something went wrong! Chose number between 1-6: ");
            }

            switch (chosenNumber)
            {
                case 1:
                    Console.WriteLine("Theme of mail: ");
                    mail.Theme = Console.ReadLine();

                    Console.WriteLine("Text of mail: ");
                    mail.Text = Console.ReadLine();

                    var receivers = receiversRepository.GetAll();
                    Console.WriteLine("Write receiver full name: ");
                    //ecли у нас 1000000 пользователей то это не мастхэв
                    foreach (var user in receivers)
                    {
                        Console.WriteLine($"Name: {user.FullName}");
                    }

                    var receiverName = Console.ReadLine();
                    var newReceiver = receiversRepository.GetReceiver(receiverName);
                    if (newReceiver != null)
                    {
                        mail.ReceiverId = newReceiver.FirstOrDefault().Id;
                        mail.Receiver = newReceiver.FirstOrDefault();
                        mailRepository.Add(mail);
                    }
                    else
                        Console.WriteLine("User not found");

                    Console.WriteLine("Успешно!");
                    break;
                case 2:

                    Console.WriteLine("Enter mail guid: ");
                    Guid guid = new Guid();
                    while(!Guid.TryParse(Console.ReadLine(),out guid))
                        {
                        Console.WriteLine("Не правильный Guid: ");
                        }

                    mailRepository.Delete(guid);
                    Console.WriteLine("Успешно!");
                    
                    break;
                case 3:
                    var mails = mailRepository.GetAll();

                    foreach(var message in mails)
                    {
                        Console.WriteLine($"Theme: {message.Theme} \nText:{message.Text} \nCreation Date: {message.CreationDate}");
                    }
                    break;
                case 4:
                    Console.WriteLine("Receiver's full name: ");
                    receiver.FullName = Console.ReadLine();
                    Console.WriteLine("Receiver's address: ");
                    receiver.Address = Console.ReadLine();

                    receiversRepository.Add(receiver);
                    Console.WriteLine("Успешно!");
                    break;
                case 5:
                    Console.WriteLine("Enter receiver guid: ");
                    Guid guidUser = new Guid();
                    while (!Guid.TryParse(Console.ReadLine(), out guid))
                    {
                        Console.WriteLine("Не правильный Guid: ");
                    }

                    receiversRepository.Delete(guidUser);

                    Console.WriteLine("Успешно!");
                    break;
                case 6:
                    var receiverList = receiversRepository.GetAll();

                    foreach(var user in receiverList)
                    {
                        Console.WriteLine($"Id: {user.Id}\nFull name: {user.FullName}\nAddress:{user.Address}\nCreation date: {user.CreationDate}");
                    }
                    break;
                default:
                    Console.WriteLine($"We have not got action under index: {chosenNumber}, yet!\nChose index between 1-6!");
                    break;
            }

        }

    }
}
