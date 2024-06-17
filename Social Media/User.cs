using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media
{
    internal class User
    {
        public string username { get; private set; }
        public int age { get; private set; }
        public string location { get; private set; }
        public string gender { get; private set; }

        public bool firstTimeLogin = false;

        public List<User> friends;

        public List<User> notFriends;
        public User(string userName, int age, string location, string gender)
        {
            this.username = userName;
            this.age = age;
            this.location = location;
            this.gender = gender;
            friends = new List<User>();
            notFriends = new List<User>();
        }

        public void AddFriend(User selectedUser)
        {
            friends.Add(selectedUser);
            notFriends.Remove(selectedUser);
        }

        public void RemoveFriend(User selectedUser)
        {
            notFriends.Add(selectedUser);
            friends.Remove(selectedUser);
        }

    }
}
