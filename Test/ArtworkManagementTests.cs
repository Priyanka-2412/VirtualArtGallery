using System;
using System.Collections.Generic;
using NUnit.Framework;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Dao;

namespace VirtualArtGallery.Tests.Tests
{
    [TestFixture]
    public class ArtworkManagementTests
    {
        private VirtualArtGalleryImpl _artworkService;

        [SetUp]
        public void SetUp()
        {
            // Initialize the VirtualArtGalleryImpl class
            _artworkService = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_AddNewArtwork()
        {
            // Arrange
            var artwork = new Artwork
            {
                Title = "Starry Night",
                Descriptions = "A famous painting by Vincent van Gogh.",
                CreationDate = new DateTime(1889, 6, 1),
                Mediums = "Oil on canvas",
                ImageURL = "https://example.com/starry-night.jpg",
                ArtistID = 2025033
            };

            // Act
            var result = _artworkService.AddArtwork(artwork);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_RemoveArtwork()
        {
            // Arrange
            int existingArtworkId = 7; // Use a known existing ArtworkID from your test DB

            // Act
            var result = _artworkService.RemoveArtwork(existingArtworkId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateArtworkDetails()
        {
            // Arrange
            var artwork = new Artwork
            {
                ArtworkID = 10, // Assume the artwork ID exists in the database
                Title = "The Kiss",
                Descriptions = "A famous Art Nouveau painting of a couple embracing.",
                CreationDate = new DateTime(2019, 01, 01),
                Mediums = "Oil and gold leaf on canvas",
                ImageURL = "https://www.belvedere.at/en/kiss-gustav-klimt",
                ArtistID = 2025040
            };

            // Act - Update artwork details
            artwork.Title = "The Couple";
            artwork.Descriptions = "A famous painting of a couple.";

            var result = _artworkService.UpdateArtwork(artwork);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchArtworks()
        {
            // Arrange
            var artwork1 = new Artwork
            {
                Title = "The Starry Night",
                Descriptions = "A swirling night sky over a peaceful village.",
                CreationDate = new DateTime(2022, 06, 01),
                Mediums = "Oil on canvas",
                ImageURL = "https://www.moma.org/collection/works/79802",
                ArtistID = 2025033
            };

            // Act
            var searchResults = _artworkService.SearchArtworks("Starry"); // Changed to a keyword that will actually match

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(a => a.Title == "The Starry Night"), Is.True);
        }
    }
}