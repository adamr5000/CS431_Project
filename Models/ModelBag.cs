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

        // This probably gets serialized as a blob
        // Should be changed to a numeric type if we want to enable eg sorting by duration
        public TimeSpan Duration { get; set; }

        public enum genre
        {
            Commedy,
            Action,
            Etc
        }

        public genre Genre { get; set; }

        // Awards, etc
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
        DateTime Time; // This is also probably serialized as a blob. Maybe.
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
        DateTime PurchaseTime;
        // other purchase info

        [Reference]
        public Movie Movie { get; set; }

        [Reference]
        public Showing Showing { get; set; }

        [Reference]
        public Customer Customer { get; set; }
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
