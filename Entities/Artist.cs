using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public required string ArtistName { get; set; }
        public required string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Nationality { get; set; }
        public required string Website { get; set; }
        public required string ContactInfo { get; set; }

        public Artist() { }

        public Artist(int artistID, string artistName, string biography, DateTime birthDate, string nationality, string website, string contactInfo)
        {
            ArtistID = artistID;
            ArtistName = artistName;
            Biography = biography;
            BirthDate = birthDate;
            Nationality = nationality;
            Website = website;
            ContactInfo = contactInfo;
        }

        public override string ToString()
        {
            return $"ArtistID: {ArtistID}, Name: {ArtistName}, Biography: {Biography}, ContactInfo: {ContactInfo}, Nationality: {Nationality}, BirthDate: {BirthDate.ToShortDateString()}";
        }
    }
}