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
        private VirtualArtGalleryImpl _artistService;

        [SetUp]
        public void SetUp()
        {
            // Initialize the VirtualArtGalleryImpl class
            _artistService = new VirtualArtGalleryImpl();
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
            var result = _artistService.AddArtist(artist);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_RemoveArtist()
        {
            // Arrange
            string existingArtistId = "2025034"; // Use a known existing ArtistID as a string from your test DB

            // Act
            var result = _artistService.RemoveArtist(existingArtistId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateArtistDetails()
        {
            // Arrange
            var artist = new Artist
            {
                ArtistID = 2025040, // Assume the artist ID exists in the database
                ArtistName = "Gustav Klimt",
                Biography = "Austrian symbolist painter, known for his rich, golden style.",
                BirthDate = new DateTime(1962, 07, 14),
                Nationality = "Austrian",
                Website = "https://www.belvedere.at/en",
                ContactInfo = "info@belvedere.at"
            };

            // Act - Update artist details
            artist.ArtistName = "Gustav";
            artist.Biography = "A renowned Austrian painter known for his unique contributions to the Art Nouveau movement.";

            var result = _artistService.UpdateArtist(artist);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchArtists()
        {
            // Arrange
            var artist = new Artist
            {
                ArtistName = "Pablo Picasso",
                Biography = "Spanish painter and sculptor, co-founder of Cubism.",
                BirthDate = new DateTime(1981, 10, 25),
                Nationality = "Spanish",
                Website = "https://www.museepicassoparis.fr/",
                ContactInfo = "contact@museepicassoparis.fr"
            };

            // Act
            var searchResults = _artistService.SearchArtists("Spanish");

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(a => a.ArtistName == "Pablo Picasso"), Is.True);
        }
    }
}