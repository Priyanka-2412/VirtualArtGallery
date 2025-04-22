using System;
using System.Collections.Generic;
using NUnit.Framework;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Dao;

namespace VirtualArtGallery.Tests.Tests
{
    [TestFixture]
    public class GalleryManagementTests
    {
        private VirtualArtGalleryImpl _galleryService;

        [SetUp]
        public void SetUp()
        {
            _galleryService = new VirtualArtGalleryImpl();
        }

        [Test]
        public void Test_CreateNewGallery()
        {
            // Arrange
            var gallery = new Gallery
            {
                GalleryName = "Modern Art Gallery",
                Descriptions = "A gallery showcasing modern art.",
                Locations = "Paris",
                OpeningHours = "9:00 AM - 5:00 PM",
                CuratorID = 2025033
            };

            // Act
            var result = _galleryService.AddGallery(gallery);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_UpdateGalleryInformation()
        {
            // Arrange
            int existingGalleryId = 10006;

            var gallery = _galleryService.GetGalleryById(existingGalleryId);
            Assert.That(gallery, Is.Not.Null, "Gallery with ID 1 should exist in the database.");

            string originalName = gallery.GalleryName;
            string originalDescription = gallery.Descriptions;

            gallery.GalleryName = "The Uffizi Gallery";
            gallery.Descriptions = "The most famous for Botticelli’s Birth of Venus.";

            // Act
            var result = _galleryService.UpdateGallery(gallery);

            // Assert
            Assert.That(result, Is.True);

            var updatedGallery = _galleryService.GetGalleryById(existingGalleryId);
            Assert.That(updatedGallery.GalleryName, Is.EqualTo("The Uffizi Gallery"));
            Assert.That(updatedGallery.Descriptions, Is.EqualTo("The most famous for Botticelli’s Birth of Venus."));
        }

        [Test]
        public void Test_RemoveGallery()
        {
            // Arrange
            int existingGalleryId = 10002; // Use a GalleryID that you know exists in your test database

            // Act
            var result = _galleryService.RemoveGallery(existingGalleryId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchGalleries()
        {
            // Arrange
            var gallery1 = new Gallery
            {
                GalleryName = "Van Gogh Museum",
                Descriptions = "Dedicated to the works of Vincent van Gogh.",
                Locations = "Amsterdam, Netherlands",
                OpeningHours = "09:00 AM - 05:00 PM",
                CuratorID = 2025033
            };

            // Act
            var searchResults = _galleryService.SearchGalleries("Gogh");

            // Debug: Log all galleries available for search
            Console.WriteLine("Search results: " + string.Join(", ", searchResults.Select(g => g.GalleryName)));

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(g => g.GalleryName == "Van Gogh Museum"), Is.True);
        }
    }
}