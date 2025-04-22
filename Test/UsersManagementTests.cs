using System;
using System.Collections.Generic;
using NUnit.Framework;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Dao;

namespace VirtualArtGallery.Tests.Tests
{
    [TestFixture]
    public class UserManagementTests
    {
        private VirtualArtGalleryImpl _usersService;

        [SetUp]
        public void SetUp()
        {
            _usersService = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_CreateNewUser()
        {
            var user = new Users
            {
                Username = "Priyanka sharma",
                Passwords = "SecurePass123",
                Email = "priyanka.sharma@gmail.com",
                FirstName = "Priyanka",
                LastName = "Sharma",
                DateOfBirth = new DateTime(2001, 6, 15),
                ProfilePicture = "prisha.jpg"
            };

            var result = _usersService.AddUser(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_RemoveUser()
        {
            var username = "Mark Zuckerberg"; // Existing username
            int userId = _usersService.GetUserIdByUsername(username);

            var result = _usersService.RemoveUser(userId);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateExistingUser()
        {
            string existingUsername = "Priyanka sharma";
            int userId = _usersService.GetUserIdByUsername(existingUsername);

            var updatedUser = new Users
            {
                UserID = userId,
                Username = "Priyanka sharma",
                Passwords = "NewSecurePass456",
                Email = "priyanka@gmail.com",
                FirstName = "Priyanka",
                LastName = "Sharma",
                DateOfBirth = new DateTime(2001, 6, 15),
                ProfilePicture = "prisha_updated.jpg"
            };

            var result = _usersService.UpdateUser(updatedUser);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchUsers()
        {
            var user1 = new Users
            {
                Username = "John Doe",
                Passwords = "password123",
                Email = "johndoe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 05, 14),
                ProfilePicture = "JohnDoe.jpg"
            };
            var user2 = new Users
            {
                Username = "Jane Smith",
                Passwords = "securepass456",
                Email = "janesmith@gmail.com",
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1995, 11, 22), // fixed year
                ProfilePicture = "JaneSmith.png"
            };

            _usersService.AddUser(user1);
            _usersService.AddUser(user2);

            var searchResults = _usersService.SearchUsers("Jane");

            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(u => u.Username == "Jane Smith"), Is.True);
        }
    }
}