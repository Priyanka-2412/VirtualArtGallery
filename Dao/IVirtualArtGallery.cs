using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entities;

namespace VirtualArtGallery.Dao
{
    public interface IVirtualArtGallery
    {
        // Artwork Management
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkId);
        Artwork GetArtworkById(int artworkId);
        List<Artwork> SearchArtworks(string keyword);

        // Gallery Management
        bool AddGallery(Gallery gallery);
        bool UpdateGallery(Gallery gallery);
        bool RemoveGallery(int galleryId);
        Gallery GetGalleryById(int galleryId);
        List<Gallery> SearchGalleries(string keyword);

        // Artist Management
        bool AddArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool RemoveArtist(int artistId);
        List<Artist> SearchArtists(string keyword);

        // User Favorite Artworks
        bool AddArtworkToFavorite(int userId, int artworkId);
        bool RemoveArtworkFromFavorite(int userId, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(int userId);
        List<Artwork> GetAllArtworks();
    }
}
