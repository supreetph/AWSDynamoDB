using System;

namespace DynamodbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UserRepository repository = new UserRepository();
            var user = new Users
            {
                UserName = "Supreet",

            };
            // repository.Store<Users>(user);
            // repository.DeleteItem<Users>(user);
            // var result = repository.GetItem<Users>(user.UserName);


        }
    }
}
