using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class ArtworkGallery
    {
        public int ArtworkID { get; set; }
        public int GalleryID { get; set; }

        public ArtworkGallery() { }

        public ArtworkGallery(int artworkID, int galleryID)
        {
            ArtworkID = artworkID;
            GalleryID = galleryID;
        }

        public override string ToString()
        {
            return $"ArtworkID: {ArtworkID}, GalleryID: {GalleryID}";
        }
    }
}