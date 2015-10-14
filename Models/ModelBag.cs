using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace CS431_Project.Models
{
    public class Movie
    {
        [AutoIncrement]
        [PrimaryKey]
        int MovieId { get; set; }

        string Title;

        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        public enum genre
        {
            Commedy,
            Action,
            Etc
        }

        public genre Genre { get; set; }

        // Awards, etc

        public override string ToString()
        {
            return $"{MovieId}: {Title}, {Duration} minutes, {ReleaseDate} release, {Genre}";
        }
    }

    public class Customer
    {
        [AutoIncrement]
        [PrimaryKey]
        int CustomerId { get; set; }
        string Name;
    }

    public class Showing
    {
        [AutoIncrement]
        [PrimaryKey]
        int ShowingId { get; set; }
        decimal Price;
        
        // This probably gets serialized as a blob
        // Should be changed to a numeric type if we want to enable eg sorting by duration
        DateTime Time;
        int ScreenNumber;
        int TotalSeats;
        int AvailableSeats;
        int MovieId;

        [Reference]
        public Movie Movie { get; set; }
    }

    public class Purchase
    {
        [AutoIncrement]
        [PrimaryKey]
        int PurchaseId { get; set; }
        int MovieId;
        int ShowingId;
        int CustomerId;
        int? PromotionId; // '?' = Nullable => optional

        DateTime PurchaseTime;
        // other purchase info

        [Reference]
        public Movie Movie { get; set; }

        [Reference]
        public Showing Showing { get; set; }

        [Reference]
        public Customer Customer { get; set; }

        [Reference]
        public Promotion Promotion { get; set; }
    }

    public class Promotion
    {
        [AutoIncrement]
        [PrimaryKey]
        int PromotionId { get; set; }
        string PromotionCode;
        DateTime Expiration;

        PromotionType PromoType;

        public enum PromotionType
        {
            FlatRate,
            Percentage,
            Free
        }

        decimal PromoValue; // Percentage discount or flat rate discount
    }

    //public class Concession
}
