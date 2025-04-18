using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Entities
{
    public class UserFavoriteArtwork
    {
        public int UserID { get; set; }
        public int ArtworkID { get; set; }

        public UserFavoriteArtwork() { }

        public UserFavoriteArtwork(int userID, int artworkID)
        {
            UserID = userID;
            ArtworkID = artworkID;
        }

        public override string ToString()
        {
            return $"UserID: {UserID}, FavoriteArtworkID: {ArtworkID}";
        }
    }
}