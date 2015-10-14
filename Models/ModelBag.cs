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
        int MovieId;

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
        int CustomerId;
        string Name;
    }

    public class Showing
    {
        int ShowingId;
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
        int PurchaseId;
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
        int PromotionId;
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
