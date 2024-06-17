using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media
{
    internal class App
    {

        List<User> registeredUsers;
        User currentUser;

        bool showingFriends = false;
        bool showingUsers = false;

        public void Run()
        {
            registeredUsers = InitRegisteredUsers();

            MainMenu();
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to FriendFace.");
            Console.WriteLine("Please create a new user or log in with an existing user.");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Log in");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var newUser = CreateNewUser();
                    registeredUsers.Add(newUser);
                    MainMenu();
                    break;
                case "2":
                    TryLogIn();
                    break;

            }
        }

        public User CreateNewUser()
        {
            Console.WriteLine("Please create a new user and log in");
            Console.WriteLine("Type in your username");
            string username = Console.ReadLine();
            Console.WriteLine("How old are you?");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which country do you live in?");
            string location = Console.ReadLine();
            Console.WriteLine("What is your gender?");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            Console.WriteLine("3. None of the above");
            Console.WriteLine("4. All of the above");
            string gender;
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    gender = "Male";
                    break;
                case "2":
                    gender = "Female";
                    break;
                case "3":
                    gender = "None";
                    break;
                case "4":
                    gender = "Multiple";
                    break;
                default:
                    gender = "None";
                    break;
            }

            return new User(username, age, location, gender);
        }

        public void TryLogIn()
        {
            bool loggedIn = false;
            while (!loggedIn)
            {
                Console.WriteLine("Please enter your username: ");
                var input = Console.ReadLine();
                for (int i = 0; i < registeredUsers.Count; i++)
                {
                    if (input == registeredUsers[i].username)
                    {
                        currentUser = registeredUsers[i];
                        loggedIn = true;
                        LogIn(currentUser);
                    }
                }
            }
        }

        public void LogIn(User user)
        {
            currentUser = user;

            foreach (var users in registeredUsers)
            {
                if (users != currentUser)
                {
                    users.AddFriend(registeredUsers[0]);
                    users.notFriends.Remove(registeredUsers[0]);
                }
            }
            if (!currentUser.firstTimeLogin)
            {
                foreach (var users in registeredUsers)
                {
                    currentUser.notFriends.Add(users);
                    users.notFriends.Add(currentUser);
                }
                
                currentUser.friends.Add(registeredUsers[0]);
                currentUser.notFriends.Remove(registeredUsers[0]);
            }
            currentUser.firstTimeLogin = true;

            Console.WriteLine("Welcome " + currentUser.username);
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Find new friends");
            Console.WriteLine("2. See your friend's profiles");
            Console.WriteLine("0. Log out");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Registered FriendFace users: ");
                    Console.WriteLine();
                    ShowUsers();
                    break;
                case "2":
                    Console.WriteLine("Your friends: ");
                    Console.WriteLine();
                    ShowFriends();
                    break;
                case "0":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid key");
                    break;
            }
        }

        public void ProfilePage()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Find new friends");
            Console.WriteLine("2. See your friend's profiles");
            Console.WriteLine("0. Log out");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Registered FriendFace users: ");
                    Console.WriteLine();
                    ShowUsers();
                    break;
                case "2":
                    Console.WriteLine("Your friends: ");
                    Console.WriteLine();
                    ShowFriends();
                    break;
                case "0":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid key");
                    break;
            }
        }




        public void ShowUsers()
        {
            var index = 1;
            Console.WriteLine("Type a user's number to enter their profile");
            Console.WriteLine("or type 0 to go back");
            foreach (var notFriend in currentUser.notFriends)
            {
                if (notFriend != currentUser)
                {
                    Console.WriteLine($"{index}. {notFriend.username}");
                    index++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. Go back");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int output) && output > 0 && output <= registeredUsers.Count)
            {
                var selectedUser = currentUser.notFriends[output - 1];
                showingUsers = true;
                ShowProfilePage(currentUser, selectedUser, registeredUsers);
            }
            else if (input == "0")
            {
                LogIn(currentUser);
            }
        }

        public void ShowFriends()
        {
            Console.WriteLine("Type a user's number to enter their profile");
            Console.WriteLine("or type 0 to go back");

            int index = 1;

            foreach (var friend in currentUser.friends)
            {
                if (friend != currentUser)
                {
                    Console.WriteLine(index + ". " + friend.username);
                    index++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. Go back");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int output) && output > 0 && output <= currentUser.friends.Count)
            {
                var selectedUser = currentUser.friends[output - 1];
                showingFriends = true;
                ShowProfilePage(currentUser, selectedUser, registeredUsers);
            }
            else if (input == "0")
            {
                LogIn(currentUser);
            }

        }

        public void ShowProfilePage(User currentUser, User selectedUser, List<User> registeredUsers)
        {
            Console.WriteLine(selectedUser.username + "'s profile!");
            Console.WriteLine("Age: " + selectedUser.age);
            Console.WriteLine("Gender: " + selectedUser.gender);
            Console.WriteLine("Location: " + selectedUser.location);
            if (!currentUser.friends.Contains(selectedUser))
            {
                Console.WriteLine("1. Add friend");
            }
            else
            {
                Console.WriteLine("1. Remove friend");
            }
            Console.WriteLine();
            Console.WriteLine("0. Go back");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (!currentUser.friends.Contains(selectedUser))
                    {
                        currentUser.AddFriend(selectedUser);
                        Console.WriteLine("You have added " + selectedUser.username + " to your friends!");
                    }
                    else
                    {
                        currentUser.RemoveFriend(selectedUser);
                        Console.WriteLine(selectedUser.username + " was removed from your friends list.");
                    }
                    ShowProfilePage(currentUser, selectedUser, registeredUsers);
                    break;
                case "0":
                    if (showingFriends)
                    {
                        showingFriends = false;
                        ShowFriends();
                    }
                    else if (showingUsers)
                    {
                        showingUsers = false;
                        ShowUsers();
                    }
                    break;
                default:
                    Console.WriteLine("Invalid key");
                    break;
            }
        }
        public List<User> InitRegisteredUsers()
        {
            var initUserList = new List<User>
            {
                new User("Admin", 100, "Everywhere", "None"),
                new User("Alice", 25, "USA", "Female"),
                new User("Bob", 30, "Canada", "Male"),
                new User("Charlie", 22, "UK", "Male"),
                new User("David", 28, "Australia", "Male"),
                new User("Eve", 27, "Germany", "Female"),
                new User("Faythe", 35, "France", "Female"),
                new User("Grace", 32, "Japan", "Female"),
                new User("Heidi", 40, "Italy", "Female"),
                new User("Jack", 29, "Brazil", "Male"),
                new User("Kate", 26, "India", "Female"),
            };

            return initUserList;
        }


    }
}
