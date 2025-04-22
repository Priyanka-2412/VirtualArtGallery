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

        // Artist Management
        public bool AddArtist(Artist artist)
        {
            string query = @"INSERT INTO [Artist] 
                    (ArtistName, Biography, BirthDate, Nationality, Website, ContactInfo) 
                    OUTPUT INSERTED.ArtistID
                    VALUES (@ArtistName, @Biography, @BirthDate, @Nationality, @Website, @ContactInfo)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtistName", artist.ArtistName);
            cmd.Parameters.AddWithValue("@Biography", artist.Biography);
            cmd.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
            cmd.Parameters.AddWithValue("@Nationality", artist.Nationality);
            cmd.Parameters.AddWithValue("@Website", artist.Website);
            cmd.Parameters.AddWithValue("@ContactInfo", artist.ContactInfo);

            try
            {
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int artistId))
                {
                    artist.ArtistID = artistId; // Ensure ArtistID is set
                    return artist.ArtistID > 0;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve ArtistID.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artist: {ex.Message}");
                return false;
            }
        }

        public bool UpdateArtist(Artist artist)
        {
            string query = @"UPDATE [Artist] SET 
                     ArtistName = @ArtistName, Biography = @Biography, BirthDate = @BirthDate, 
                     Nationality = @Nationality, Website = @Website, ContactInfo = @ContactInfo 
                     WHERE ArtistID = @ArtistID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ArtistName", artist.ArtistName);
            cmd.Parameters.AddWithValue("@Biography", artist.Biography);
            cmd.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
            cmd.Parameters.AddWithValue("@Nationality", artist.Nationality);
            cmd.Parameters.AddWithValue("@Website", artist.Website);
            cmd.Parameters.AddWithValue("@ContactInfo", artist.ContactInfo);
            cmd.Parameters.AddWithValue("@ArtistID", artist.ArtistID);

            try
            {
                // Execute the SQL command
                int rowsAffected = cmd.ExecuteNonQuery();

                // Log the number of affected rows
                Console.WriteLine($"Rows affected: {rowsAffected}");

                // Return true if rows were affected, otherwise false
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating artist: {ex.Message}");
                return false;
            }
        }

        public bool RemoveArtist(string artistId)
        {
            string deleteArtistQuery = "DELETE FROM Artist WHERE ArtistID = @ArtistId";
            using (SqlCommand cmd = new SqlCommand(deleteArtistQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ArtistId", artistId);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Artist deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Artist not found or not deleted.");
                }

                return rowsAffected > 0;
            }
        }


        public int GetArtistIdByName(string artistName, SqlTransaction transaction)
        {
            string query = "SELECT ArtistID FROM [Artist] WHERE Name = @ArtistName";
            SqlCommand cmd = new SqlCommand(query, connection, transaction);
            cmd.Parameters.AddWithValue("@ArtistName", artistName);

            try
            {
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int artistId))
                {
                    return artistId;
                }
                else
                {
                    return -1; // Artist not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching artist ID: {ex.Message}");
                return -1; // Return -1 if an error occurs
            }
        }


        public List<Artist> SearchArtists(string keyword)
        {
            string query = @"SELECT * FROM [Artist] 
                             WHERE ArtistName LIKE @Keyword OR Biography LIKE @Keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            List<Artist> artists = new List<Artist>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        artists.Add(new Artist
                        {
                            ArtistID = reader.GetInt32(0),
                            ArtistName = reader.GetString(1),
                            Biography = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3),
                            Nationality = reader.GetString(4),
                            Website = reader.GetString(5),
                            ContactInfo = reader.GetString(6)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching artists: {ex.Message}");
            }

            return artists;
        }

        // Artwork Table (matches: [Artwork])
        public bool AddArtwork(Artwork artwork)
        {
            string query = @"INSERT INTO [Artwork] 
                     (Title, Descriptions, CreationDate, Mediums, ImageURL, ArtistID) 
                     OUTPUT INSERTED.ArtworkID
                     VALUES (@Title, @Descriptions, @CreationDate, @Mediums, @ImageURL, @ArtistID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Title", artwork.Title);
            cmd.Parameters.AddWithValue("@Descriptions", artwork.Descriptions);
            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
            cmd.Parameters.AddWithValue("@Mediums", artwork.Mediums);
            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

            try
            {
                artwork.ArtworkID = (int)cmd.ExecuteScalar();
                return artwork.ArtworkID > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artwork: {ex.Message}");
                return false;
            }
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
            // Delete from UserFavoriteArtwork table (if applicable)
            string deleteFavoritesQuery = "DELETE FROM [UserFavoriteArtwork] WHERE ArtworkID = @ArtworkID";
            using (SqlCommand deleteFavoritesCmd = new SqlCommand(deleteFavoritesQuery, connection))
            {
                deleteFavoritesCmd.Parameters.AddWithValue("@ArtworkID", artworkId);
                deleteFavoritesCmd.ExecuteNonQuery();
            }

            // Delete from Artwork table
            string deleteArtworkQuery = "DELETE FROM [Artwork] WHERE ArtworkID = @ArtworkID";
            using (SqlCommand deleteArtworkCmd = new SqlCommand(deleteArtworkQuery, connection))
            {
                deleteArtworkCmd.Parameters.AddWithValue("@ArtworkID", artworkId);
                int rowsAffected = deleteArtworkCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
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

            Console.WriteLine($"Executing Query: {query} with keyword: {keyword}");

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

            if (artworks.Count == 0)
            {
                Console.WriteLine("No artworks found for the given keyword.");
            }

            return artworks;
        }

        public Artwork GetArtworkByTitleAndArtist(string title, int artistId)
        {
            Artwork artwork = new Artwork
            {
                Title = string.Empty,        // Default empty string for Title
                Descriptions = string.Empty, // Default empty string for Descriptions
                Mediums = string.Empty,      // Default empty string for Mediums
                ImageURL = string.Empty      // Default empty string for ImageURL
            };

            using (SqlConnection connection = new SqlConnection("your_connection_string"))
            {
                connection.Open(); // Open the connection

                string query = "SELECT * FROM Artwork WHERE Title = @Title AND ArtistID = @ArtistID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@ArtistID", artistId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artwork = new Artwork
                            {
                                ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                Title = reader["Title"].ToString() ?? string.Empty,
                                Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                Mediums = reader["Mediums"].ToString() ?? string.Empty,
                                ImageURL = reader["ImageURL"].ToString() ?? string.Empty,
                                ArtistID = Convert.ToInt32(reader["ArtistID"])
                            };
                        }
                    }
                }
            } // The connection will be automatically closed here when the 'using' block ends.

            return artwork;
        }

        public List<Artwork> GetAllArtworks()
        {
            List<Artwork> artworks = new List<Artwork>();
            string query = "SELECT * FROM [Artwork]";
            SqlCommand cmd = new SqlCommand(query, connection);

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
        OUTPUT INSERTED.GalleryID
        VALUES (@GalleryName, @Descriptions, @Locations, @CuratorID, @OpeningHours)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryName", gallery.GalleryName);
            cmd.Parameters.AddWithValue("@Descriptions", gallery.Descriptions);
            cmd.Parameters.AddWithValue("@Locations", gallery.Locations);
            cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);

            gallery.GalleryID = (int)cmd.ExecuteScalar(); // Retrieve the generated GalleryID
            return gallery.GalleryID > 0;
        }

        public bool UpdateGallery(Gallery gallery)
        {
            string query = @"UPDATE [Gallery] SET 
                     GalleryName = @GalleryName, 
                     Descriptions = @Descriptions, 
                     Locations = @Locations, 
                     OpeningHours = @OpeningHours, 
                     CuratorID = @CuratorID 
                     WHERE GalleryID = @GalleryID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GalleryName", gallery.GalleryName);
            cmd.Parameters.AddWithValue("@Descriptions", gallery.Descriptions);
            cmd.Parameters.AddWithValue("@Locations", gallery.Locations);
            cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
            cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
            cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);

            return cmd.ExecuteNonQuery() > 0; // Return true if the update was successful
        }

        public bool GetGalleryByName(string galleryName)
        {
            string query = @"SELECT 1 FROM [Gallery] WHERE GalleryName = @GalleryName";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@GalleryName", galleryName);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public bool RemoveGallery(int galleryId)
        {
            // Check if the gallery exists
            string checkQuery = "SELECT COUNT(*) FROM [Gallery] WHERE GalleryID = @GalleryID";
            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@GalleryID", galleryId);

            int count = (int)checkCmd.ExecuteScalar();
            if (count == 0)
            {
                return false; // Gallery does not exist
            }

            // Start a transaction to ensure both deletes are done atomically
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // Remove the references from the ArtworkGallery table
                    string removeReferencesQuery = "DELETE FROM [ArtworkGallery] WHERE GalleryID = @GalleryID";
                    SqlCommand removeReferencesCmd = new SqlCommand(removeReferencesQuery, connection, transaction);
                    removeReferencesCmd.Parameters.AddWithValue("@GalleryID", galleryId);
                    removeReferencesCmd.ExecuteNonQuery();

                    // Now, remove the gallery itself
                    string deleteQuery = "DELETE FROM [Gallery] WHERE GalleryID = @GalleryID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection, transaction);
                    deleteCmd.Parameters.AddWithValue("@GalleryID", galleryId);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    // Commit the transaction if both deletes were successful
                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        return true; // Gallery successfully removed
                    }
                    else
                    {
                        transaction.Rollback();
                        return false; // Gallery could not be deleted
                    }
                }
                catch (Exception)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    return false; // Indicate failure due to an error
                }
            }
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
                        GalleryName = reader["GalleryName"].ToString() ?? string.Empty,
                        Descriptions = reader["Descriptions"].ToString() ?? string.Empty,
                        Locations = reader["Locations"].ToString() ?? string.Empty,
                        OpeningHours = reader["OpeningHours"].ToString() ?? string.Empty,
                        CuratorID = (int)reader["CuratorID"]
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

        // AddUser Method
        public bool AddUser(Users user)
        {
            string query = @"INSERT INTO [Users] 
             (Username, Passwords, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
             OUTPUT INSERTED.UserID
             VALUES (@Username, @Passwords, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Username", user.Username ?? string.Empty);
            cmd.Parameters.AddWithValue("@Passwords", user.Passwords ?? string.Empty);
            cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@FirstName", (object?)user.FirstName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", (object?)user.LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DateOfBirth", (object?)user.DateOfBirth ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProfilePicture", (object?)user.ProfilePicture ?? DBNull.Value);

            try
            {
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int userId))
                {
                    user.UserID = userId;
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve UserID.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }

        // UpdateUser Method
        public bool UpdateUser(Users user)
        {
            string query = @"UPDATE [Users] SET 
             Passwords = @Passwords,
             Email = @Email,
             FirstName = @FirstName,
             LastName = @LastName,
             DateOfBirth = @DateOfBirth,
             ProfilePicture = @ProfilePicture
             WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Passwords", user.Passwords ?? string.Empty);
            cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@FirstName", (object?)user.FirstName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LastName", (object?)user.LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DateOfBirth", (object?)user.DateOfBirth ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProfilePicture", (object?)user.ProfilePicture ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserID", user.UserID);

            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false;
            }
        }

        // RemoveUser Method
        public bool RemoveUser(int userId)
        {
            string query = "DELETE FROM [Users] WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing user: {ex.Message}");
                return false;
            }
        }

        public int GetUserIdByUsername(string username)
        {
            string query = "SELECT UserID FROM [Users] WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Username", username);

            try
            {
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int userId))
                {
                    return userId;
                }
                else
                {
                    return -1; // User not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user ID: {ex.Message}");
                return -1;
            }
        }


        // SearchUsers Method
        public List<Users> SearchUsers(string keyword)
        {
            string query = @"SELECT * FROM [Users] 
             WHERE Username LIKE @Keyword 
                OR Email LIKE @Keyword 
                OR FirstName LIKE @Keyword 
                OR LastName LIKE @Keyword";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            List<Users> users = new List<Users>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Users user = new Users
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader["Username"]?.ToString() ?? string.Empty,
                            Passwords = reader["Passwords"]?.ToString() ?? string.Empty,
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            FirstName = reader["FirstName"]?.ToString() ?? string.Empty,
                            LastName = reader["LastName"]?.ToString() ?? string.Empty,
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            ProfilePicture = reader["ProfilePicture"]?.ToString() ?? string.Empty,
                        };
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching users: {ex.Message}");
            }

            return users;
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
            // First, check if the user exists
            string userCheckQuery = "SELECT COUNT(*) FROM [Users] WHERE UserID = @UserID";
            SqlCommand checkCmd = new SqlCommand(userCheckQuery, connection);
            checkCmd.Parameters.AddWithValue("@UserID", userId);
            int userCount = (int)checkCmd.ExecuteScalar();

            if (userCount == 0)
            {
                throw new UserNotFoundException($"User with ID {userId} does not exist.");
            }

            // Now fetch favorite artworks
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
