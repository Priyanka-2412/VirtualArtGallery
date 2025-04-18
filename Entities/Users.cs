using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class Users
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public required string Passwords { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string ProfilePicture { get; set; }

        public Users() { }

        public Users(int userID, string username, string passwords, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture)
        {
            UserID = userID;
            Username = username;
            Passwords = passwords;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ProfilePicture = profilePicture;
        }

        public override string ToString()
        {
            return $"UserID: {UserID}, Username: {Username}, Email: {Email}, Name: {FirstName} {LastName}, Passwords: {Passwords}, DateOfBirth: {DateOfBirth}, ProfilePicture: {ProfilePicture}";
        }
    }
}