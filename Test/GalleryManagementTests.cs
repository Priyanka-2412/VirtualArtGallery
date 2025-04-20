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
            // Initialize the VirtualArtGalleryImpl class
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
                Locations = "Downtown",
                OpeningHours = "9 AM - 5 PM",
                CuratorID = 2025037
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
            var gallery = new Gallery
            {
                GalleryName = "Old Name",
                Descriptions = "Old Description",
                Locations = "Downtown",
                OpeningHours = "9 AM - 5 PM",
                CuratorID = 2025031
            };
            _galleryService.AddGallery(gallery);

            // Ensure GalleryID is set
            Assert.That(gallery.GalleryID, Is.GreaterThan(0));

            gallery.GalleryName = "Updated Gallery Name";
            gallery.Descriptions = "Updated gallery description.";

            // Act
            var result = _galleryService.UpdateGallery(gallery);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_RemoveGallery()
        {
            // Arrange
            var gallery = new Gallery
            {
                GalleryName = "Gallery to Remove",
                Descriptions = "Description",
                Locations = "Downtown",
                OpeningHours = "9 AM - 5 PM",
                CuratorID = 2025039
            };
            _galleryService.AddGallery(gallery);

            // Act
            var result = _galleryService.RemoveGallery(gallery.GalleryID);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_SearchGalleries()
        {
            // Arrange
            var gallery1 = new Gallery
            {
                GalleryName = "Gallery One",
                Descriptions = "Description One",
                Locations = "Downtown",
                OpeningHours = "9 AM - 5 PM",
                CuratorID = 2025037
            };
            var gallery2 = new Gallery
            {
                GalleryName = "Gallery Two",
                Descriptions = "Description Two",
                Locations = "Uptown",
                OpeningHours = "10 AM - 6 PM",
                CuratorID = 2025034
            };
            _galleryService.AddGallery(gallery1);
            _galleryService.AddGallery(gallery2);

            // Act
            var searchResults = _galleryService.SearchGalleries("Gallery");

            // Assert
            Assert.That(searchResults, Is.Not.Empty);
            Assert.That(searchResults.Exists(g => g.GalleryName == "Gallery One"), Is.True);
            Assert.That(searchResults.Exists(g => g.GalleryName == "Gallery Two"), Is.True);
        }
    }
}