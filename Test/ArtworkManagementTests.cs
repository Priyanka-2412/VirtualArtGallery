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
        private VirtualArtGalleryImpl _galleryService;

        [SetUp]
        public void SetUp()
        {
            // Initialize the VirtualArtGalleryImpl class
            _galleryService = new VirtualArtGalleryImpl();
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
            var result = _galleryService.AddArtwork(artwork);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateArtworkDetails()
        {
            // Arrange
            var artwork = new Artwork
            {
                Title = "Sunset over Mountains",
                Descriptions = "A beautiful sunset landscape with acrylics.",
                CreationDate = new DateTime(2015, 6, 10),
                Mediums = "Acrylic on Canvas",
                ImageURL = "https://example.com/sunset-mountains.jpg",
                ArtistID = 2025035
            };
            _galleryService.AddArtwork(artwork);

            // Ensure ArtworkID is set after insertion
            Assert.That(artwork.ArtworkID, Is.GreaterThan(0));

            // Update artwork details
            artwork.Title = "Sunset Over the Hills";
            artwork.Descriptions = "A mesmerizing sunset view with richer details.";

            // Act
            var result = _galleryService.UpdateArtwork(artwork);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_RemoveArtwork()
        {
            // Arrange
            var artwork = new Artwork
            {
                Title = "Mona Lisa",
                Descriptions = "A portrait of a woman with an enigmatic expression.",
                CreationDate = new DateTime(1753, 01, 01), // Updated to a valid date
                Mediums = "Oil on poplar panel",
                ImageURL = "https://www.louvre.fr/en/explore/the-palace/mona-lisa",
                ArtistID = 2025031
            };
            _galleryService.AddArtwork(artwork);

            // Act
            var result = _galleryService.RemoveArtwork(artwork.ArtworkID);

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
                CreationDate = new DateTime(1753, 06, 01),
                Mediums = "Oil on canvas",
                ImageURL = "https://www.moma.org/collection/works/79802",
                ArtistID = 2025033
            };
            var artwork2 = new Artwork
            {
                Title = "Girl with a Pearl Earring",
                Descriptions = "A portrait of a young girl with a pearl earring.",
                CreationDate = new DateTime(1753, 01, 01),
                Mediums = "Oil on canvas",
                ImageURL = "https://www.mauritshuis.nl/en/discover/girl-with-a-pearl-earring/",
                ArtistID = 2025035
            };

            // Assume _galleryService is initialized (you can use a mock or actual service)
            Assert.That(_galleryService.AddArtwork(artwork1), Is.True, "Failed to add artwork1");
            Assert.That(_galleryService.AddArtwork(artwork2), Is.True, "Failed to add artwork2");

            // Act
            var searchResults = _galleryService.SearchArtworks("starry"); // Changed to a keyword that will actually match

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(a => a.Title == "The Starry Night"), Is.True);
            // Only asserting one match because "starry" doesn’t match the second title
        }
    }
}