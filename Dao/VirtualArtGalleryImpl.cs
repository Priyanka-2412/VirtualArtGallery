using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Exceptions;
using VirtualArtGallery.Util;

namespace VirtualArtGallery.Dao
{
    public class VirtualArtGalleryImpl : IVirtualArtGallery
    {
        private SqlConnection connection;

        public VirtualArtGalleryImpl()
        {
            connection = DBConnUtil.GetConnection();
        }

        // Artwork Table (matches: [Artwork])
        public bool AddArtwork(Artwork artwork)
        {
            string query = @"INSERT INTO [Artwork] 
                (Title, Descriptions, CreationDate, Mediums, ImageURL, ArtistID) 
                VALUES (@Title, @Descriptions, @CreationDate, @Mediums, @ImageURL, @ArtistID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@Descriptions", artwork.Descriptions);
            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
            cmd.Parameters.AddWithValue("@Mediums", artwork.Mediums);
            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            string query = @"UPDATE [Artwork] SET 
                            Title = @Title, Descriptions = @Descriptions, CreationDate = @CreationDate, 
                            Mediums = @Mediums, ImageURL = @ImageURL 
                            WHERE ArtworkID = @ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@Descriptions", artwork.Descriptions);
            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
            cmd.Parameters.AddWithValue("@Mediums", artwork.Mediums);
            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
            cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveArtwork(int artworkId)
        {
            string query = "DELETE FROM [Artwork] WHERE ArtworkID = @ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public Artwork GetArtworkById(int artworkId)
        {
            string query = "SELECT * FROM [Artwork] WHERE ArtworkID = @ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Artwork
                    {
                        ArtworkID = (int)reader["ArtworkID"],
                        Title = reader["Title"].ToString() ?? string.Empty,
                        Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                        CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                        Mediums = reader["Mediums"].ToString() ?? string.Empty,
                        ImageURL = reader["ImageURL"].ToString() ?? string.Empty,
                        ArtistID = (int)reader["ArtistID"]
                    };
                }
            }

            throw new ArtworkNotFoundException("Artwork with ID " + artworkId + " not found.");
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            List<Artwork> artworks = new List<Artwork>();
            string query = @"SELECT * FROM [Artwork] 
                     WHERE Title LIKE @keyword OR Descriptions LIKE @keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    artworks.Add(new Artwork
                    {
                        ArtworkID = (int)reader["ArtworkID"],
                        Title = reader["Title"].ToString() ?? string.Empty,
                        Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                        CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                        Mediums = reader["Mediums"].ToString() ?? string.Empty,
                        ImageURL = reader["ImageURL"].ToString() ?? string.Empty,
                        ArtistID = (int)reader["ArtistID"]
                    });
                }
            }

            return artworks;
        }

        // Gallery Table (matches: [Gallery])
        public bool AddGallery(Gallery gallery)
        {
            string query = @"INSERT INTO [Gallery] 
                (GalleryName, Descriptions, Locations, CuratorID, OpeningHours) 
                VALUES (@GalleryName, @Descriptions, @Locations, @CuratorID, @OpeningHours)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryName", gallery.GalleryName);
            cmd.Parameters.AddWithValue("@Descriptions", gallery.Descriptions);
            cmd.Parameters.AddWithValue("@Locations", gallery.Locations);
            cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateGallery(Gallery gallery)
        {
            string query = @"UPDATE [Gallery] SET 
                GalleryName = @GalleryName, Descriptions = @Descriptions, Locations = @Locations, 
                CuratorID = @CuratorID, OpeningHours = @OpeningHours 
                WHERE GalleryID = @GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryName", gallery.GalleryName);
            cmd.Parameters.AddWithValue("@Descriptions", gallery.Descriptions);
            cmd.Parameters.AddWithValue("@Locations", gallery.Locations);
            cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
            cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveGallery(int galleryId)
        {
            string query = "DELETE FROM [Gallery] WHERE GalleryID = @GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", galleryId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public Gallery GetGalleryById(int galleryId)
        {
            string query = "SELECT * FROM [Gallery] WHERE GalleryID = @GalleryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryID", galleryId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Gallery
                    {
                        GalleryID = (int)reader["GalleryID"],
                        GalleryName = reader["Name"].ToString() ?? string.Empty,
                        Descriptions = reader["Description"].ToString() ?? string.Empty,
                        Locations = reader["Location"].ToString() ?? string.Empty,
                        OpeningHours = reader["OpeningHours"].ToString() ?? string.Empty,
                        CuratorID = (int)reader["Curator"]
                    };
                }
            }

            throw new Exception("Gallery with ID " + galleryId + " not found.");
        }

        public List<Gallery> SearchGalleries(string keyword)
        {
            List<Gallery> galleries = new List<Gallery>();
            string query = @"SELECT * FROM [Gallery] 
                     WHERE GalleryName LIKE @keyword OR Descriptions LIKE @keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    galleries.Add(new Gallery
                    {
                        GalleryID = (int)reader["GalleryID"],
                        GalleryName = reader["GalleryName"].ToString() ?? string.Empty,
                        Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                        Locations = reader["Locations"].ToString() ?? string.Empty,
                        OpeningHours = reader["OpeningHours"].ToString() ?? string.Empty,
                        CuratorID = (int)reader["CuratorID"]
                    });
                }
            }

            return galleries;
        }

        // User_Favorite_Artwork Table
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            // Check if the combination already exists
            string checkQuery = "SELECT COUNT(*) FROM [UserFavoriteArtwork] WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@UserID", userId);
            checkCmd.Parameters.AddWithValue("@ArtworkID", artworkId);

            int count = (int)checkCmd.ExecuteScalar();
            if (count > 0)
            {
                Console.WriteLine($"The artwork with ID {artworkId} is already in the favorites for user {userId}.");
                return false; // Indicate that the record already exists
            }

            // Insert the new record
            string insertQuery = "INSERT INTO [UserFavoriteArtwork] (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@UserID", userId);
            insertCmd.Parameters.AddWithValue("@ArtworkID", artworkId);

            return insertCmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            string query = "DELETE FROM [UserFavoriteArtwork] WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            List<Artwork> artworks = new List<Artwork>();
            string query = @"
        SELECT A.* FROM [Artwork] A
        INNER JOIN [UserFavoriteArtwork] UFA ON A.ArtworkID = UFA.ArtworkID
        WHERE UFA.UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    artworks.Add(new Artwork
                    {
                        ArtworkID = (int)reader["ArtworkID"],
                        Title = reader["Title"].ToString() ?? string.Empty,
                        Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                        CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                        Mediums = reader["Mediums"].ToString() ?? string.Empty,
                        ImageURL = reader["ImageURL"].ToString() ?? string.Empty,
                        ArtistID = (int)reader["ArtistID"]
                    });
                }
            }

            return artworks;
        }
    }
}