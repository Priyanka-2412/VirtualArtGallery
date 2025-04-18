using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public required string GalleryName { get; set; }
        public required string Descriptions { get; set; }
        public required string Locations { get; set; }
        public required string OpeningHours { get; set; }
        public int CuratorID { get; set; }

        public Gallery() { }

        public Gallery(int galleryID, string galleryName, string descriptions, string locations, string openingHours, int curatorID)
        {
            GalleryID = galleryID;
            GalleryName = galleryName;
            Descriptions = descriptions;
            Locations = locations;
            OpeningHours = openingHours;
            CuratorID = curatorID;
        }

        public override string ToString()
        {
            return $"GalleryID: {GalleryID}, Name: {GalleryName}, Description: {Descriptions}, OpeningHours: {OpeningHours}, CuratorID: {CuratorID}, Location: {Locations}";
        }
    }
}
