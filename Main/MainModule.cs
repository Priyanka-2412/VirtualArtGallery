using System;
using System.Collections.Generic;
using VirtualArtGallery.Dao;
using VirtualArtGallery.Entities;
using VirtualArtGallery.Exceptions;

namespace VirtualArtGallery.Main
{
    public class MainModule
    {
        public static void Main(string[] args)
        {
            IVirtualArtGallery galleryService = new VirtualArtGalleryImpl();

            try
            {
                Console.WriteLine("=== Virtual Art Gallery ===");

                while (true)
                {
                    Console.WriteLine("\nChoose an option:");
                    Console.WriteLine("1. Add Artwork");
                    Console.WriteLine("2. Search Artworks");
                    Console.WriteLine("3. Add Gallery");
                    Console.WriteLine("4. Search Galleries");
                    Console.WriteLine("5. Add Artwork to Favorites");
                    Console.WriteLine("6. View Favorite Artworks");
                    Console.WriteLine("7. Remove Artwork from Favorites");
                    Console.WriteLine("8. Exit");
                    Console.Write("Enter your choice: ");
                    string? choiceInput = Console.ReadLine();

                    if (!int.TryParse(choiceInput, out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            // Add Artwork
                            Console.WriteLine("\nEnter Artwork Details:");
                            Console.Write("Title: ");
                            string? title = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(title))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("Descriptions: ");
                            string? descriptions = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(descriptions))
                            {
                                Console.WriteLine("Descriptions cannot be empty.");
                                break;
                            }

                            Console.Write("Creation Date (yyyy-MM-dd): ");
                            string? creationDateInput = Console.ReadLine();
                            if (!DateTime.TryParse(creationDateInput, out DateTime creationDate))
                            {
                                Console.WriteLine("Invalid date format.");
                                break;
                            }

                            Console.Write("Mediums: ");
                            string? mediums = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(mediums))
                            {
                                Console.WriteLine("Mediums cannot be empty.");
                                break;
                            }

                            Console.Write("Image URL: ");
                            string? imageUrl = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                Console.WriteLine("Image URL cannot be empty.");
                                break;
                            }

                            Console.Write("Artist ID: ");
                            string? artistIdInput = Console.ReadLine();
                            if (!int.TryParse(artistIdInput, out int artistId))
                            {
                                Console.WriteLine("Invalid Artist ID.");
                                break;
                            }

                            Artwork newArtwork = new Artwork
                            {
                                Title = title,
                                Descriptions = descriptions,
                                CreationDate = creationDate,
                                Mediums = mediums,
                                ImageURL = imageUrl,
                                ArtistID = artistId
                            };

                            bool isArtworkAdded = galleryService.AddArtwork(newArtwork);
                            Console.WriteLine("Artwork added: " + isArtworkAdded);
                            break;

                        case 2:
                            // Search Artworks
                            Console.Write("\nEnter keyword to search artworks: ");
                            string? artworkKeyword = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(artworkKeyword))
                            {
                                Console.WriteLine("Keyword cannot be empty.");
                                break;
                            }

                            List<Artwork> artworks = galleryService.SearchArtworks(artworkKeyword);
                            Console.WriteLine("Search results:");
                            foreach (var art in artworks)
                            {
                                Console.WriteLine(art);
                            }
                            break;

                        case 3:
                            // Add Gallery
                            Console.WriteLine("\nEnter Gallery Details:");
                            Console.Write("Gallery Name: ");
                            string? galleryName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(galleryName))
                            {
                                Console.WriteLine("Gallery Name cannot be empty.");
                                break;
                            }

                            Console.Write("Descriptions: ");
                            string? galleryDescriptions = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(galleryDescriptions))
                            {
                                Console.WriteLine("Descriptions cannot be empty.");
                                break;
                            }

                            Console.Write("Locations: ");
                            string? locations = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(locations))
                            {
                                Console.WriteLine("Locations cannot be empty.");
                                break;
                            }

                            Console.Write("Curator ID: ");
                            string? curatorIdInput = Console.ReadLine();
                            if (!int.TryParse(curatorIdInput, out int curatorId))
                            {
                                Console.WriteLine("Invalid Curator ID.");
                                break;
                            }

                            Console.Write("Opening Hours: ");
                            string? openingHours = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(openingHours))
                            {
                                Console.WriteLine("Opening Hours cannot be empty.");
                                break;
                            }

                            Gallery newGallery = new Gallery
                            {
                                GalleryName = galleryName,
                                Descriptions = galleryDescriptions,
                                Locations = locations,
                                CuratorID = curatorId,
                                OpeningHours = openingHours
                            };

                            bool isGalleryAdded = galleryService.AddGallery(newGallery);
                            Console.WriteLine("Gallery added: " + isGalleryAdded);
                            break;

                        case 4:
                            // Search Galleries
                            Console.Write("\nEnter keyword to search galleries: ");
                            string? galleryKeyword = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(galleryKeyword))
                            {
                                Console.WriteLine("Keyword cannot be empty.");
                                break;
                            }

                            List<Gallery> galleries = galleryService.SearchGalleries(galleryKeyword);
                            Console.WriteLine("Gallery search results:");
                            foreach (var gal in galleries)
                            {
                                Console.WriteLine(gal);
                            }
                            break;

                        case 5:
                            // Add Artwork to Favorites
                            Console.Write("\nEnter User ID: ");
                            string? userIdToAddInput = Console.ReadLine();
                            if (!int.TryParse(userIdToAddInput, out int userIdToAdd))
                            {
                                Console.WriteLine("Invalid User ID.");
                                break;
                            }

                            Console.Write("Enter Artwork ID: ");
                            string? artworkIdToAddInput = Console.ReadLine();
                            if (!int.TryParse(artworkIdToAddInput, out int artworkIdToAdd))
                            {
                                Console.WriteLine("Invalid Artwork ID.");
                                break;
                            }

                            bool addedToFavorites = galleryService.AddArtworkToFavorite(userIdToAdd, artworkIdToAdd);
                            Console.WriteLine("Added to favorites: " + addedToFavorites);
                            break;

                        case 6:
                            // View Favorite Artworks
                            Console.Write("\nEnter User ID: ");
                            string? userIdToViewInput = Console.ReadLine();
                            if (!int.TryParse(userIdToViewInput, out int userIdToView))
                            {
                                Console.WriteLine("Invalid User ID.");
                                break;
                            }

                            List<Artwork> favorites = galleryService.GetUserFavoriteArtworks(userIdToView);
                            Console.WriteLine("Favorite artworks:");
                            foreach (var fav in favorites)
                            {
                                Console.WriteLine(fav);
                            }
                            break;

                        case 7:
                            // Remove Artwork from Favorites
                            Console.Write("\nEnter User ID: ");
                            string? userIdToRemoveInput = Console.ReadLine();
                            if (!int.TryParse(userIdToRemoveInput, out int userIdToRemove))
                            {
                                Console.WriteLine("Invalid User ID.");
                                break;
                            }

                            Console.Write("Enter Artwork ID: ");
                            string? artworkIdToRemoveInput = Console.ReadLine();
                            if (!int.TryParse(artworkIdToRemoveInput, out int artworkIdToRemove))
                            {
                                Console.WriteLine("Invalid Artwork ID.");
                                break;
                            }

                            bool removedFromFavorites = galleryService.RemoveArtworkFromFavorite(userIdToRemove, artworkIdToRemove);
                            Console.WriteLine("Removed from favorites: " + removedFromFavorites);
                            break;

                        case 8:
                            // Exit
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (ArtworkNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }
    }
}