using System;
using System.Collections.Generic;
using NUnit.Framework;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Dao;

namespace VirtualArtGallery.Tests.Tests
{
    [TestFixture]
    public class ArtistManagementTests
    {
        private VirtualArtGalleryImpl _galleryService;

        [SetUp]
        public void SetUp()
        {
            // Initialize the VirtualArtGalleryImpl class
            _galleryService = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_AddNewArtist()
        {
            // Arrange
            var artist = new Artist
            {
                ArtistName = "Vincent van Gogh",
                Biography = "A Dutch post-impressionist painter.",
                BirthDate = new DateTime(1853, 3, 30),
                Nationality = "Dutch",
                Website = "https://www.vangogh.com",
                ContactInfo = "contact@vangogh.com"
            };

            // Act
            var result = _galleryService.AddArtist(artist);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateArtistDetails()
        {
            // Arrange
            var artist = new Artist
            {
                ArtistName = "Leonardo da Vinci",
                Biography = "An Italian polymath of the Renaissance.",
                BirthDate = new DateTime(1990, 4, 15),
                Nationality = "Italian",
                Website = "https://www.leonardodavinci.com",
                ContactInfo = "contact@davinci.com"
            };
            var addResult = _galleryService.AddArtist(artist);

            // Ensure AddArtist succeeded
            Assert.That(addResult, Is.True);
            Assert.That(artist.ArtistID, Is.GreaterThan(0)); // Ensure ArtistID is set

            // Update artist details
            artist.Biography = "A renowned Italian polymath and painter.";
            artist.Website = "https://www.updated-davinci.com";

            // Act
            var updateResult = _galleryService.UpdateArtist(artist);

            // Assert
            Assert.That(updateResult, Is.True);
        }

        [Test]
        public void Test_RemoveArtist()
        {
            // Arrange
            var artist = new Artist
            {
                ArtistName = "Claude Monet",
                Biography = "A founder of French Impressionist painting.",
                BirthDate = new DateTime(1840, 11, 14),
                Nationality = "French",
                Website = "https://www.monet.com",
                ContactInfo = "contact@monet.com"
            };
            _galleryService.AddArtist(artist);

            // Act
            var result = _galleryService.RemoveArtist(artist.ArtistID);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchArtists()
        {
            // Arrange
            var artist1 = new Artist
            {
                ArtistName = "Pablo Picasso",
                Biography = "A Spanish painter and sculptor.",
                BirthDate = new DateTime(1881, 10, 25),
                Nationality = "Spanish",
                Website = "https://www.picasso.com",
                ContactInfo = "contact@picasso.com"
            };
            var artist2 = new Artist
            {
                ArtistName = "Salvador Dalí",
                Biography = "A Spanish surrealist artist.",
                BirthDate = new DateTime(1904, 5, 11),
                Nationality = "Spanish",
                Website = "https://www.dali.com",
                ContactInfo = "contact@dali.com"
            };
            _galleryService.AddArtist(artist1);
            _galleryService.AddArtist(artist2);

            // Act
            var searchResults = _galleryService.SearchArtists("Spanish");

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(a => a.ArtistName == "Pablo Picasso"), Is.True);
            Assert.That(searchResults.Exists(a => a.ArtistName == "Salvador Dalí"), Is.True);
        }
    }
}