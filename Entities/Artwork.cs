using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class Artwork
    {
        public int ArtworkID { get; set; }
        public required string Title { get; set; }
        public required string Descriptions { get; set; }
        public DateTime CreationDate { get; set; }
        public required string Mediums { get; set; }
        public required string ImageURL { get; set; }
        public int ArtistID { get; set; }

        public Artwork() { }

        public Artwork(int artworkID, string title, string descriptions, DateTime creationDate, string mediums, string imageURL, int artistID)
        {
            ArtworkID = artworkID;
            Title = title;
            Descriptions = descriptions;
            CreationDate = creationDate;
            Mediums = mediums;
            ImageURL = imageURL;
            ArtistID = artistID;
        }

        public override string ToString()
        {
            return $"ArtworkID: {ArtworkID}, Title: {Title}, Descriptions: {Descriptions}, Mediums: {Mediums}, ImageURL: {ImageURL}, ArtistID: {ArtistID}, CreationDate: {CreationDate.ToShortDateString()}";
        }
    }
}