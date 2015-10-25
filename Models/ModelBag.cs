using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nancy.ViewEngines.Razor.HtmlHelpers;
using ServiceStack.DataAnnotations;

namespace CS431_Project.Models
{
    public class Movie
    {
        [AutoIncrement]
        [PrimaryKey]
        public int MovieId { get; set; }        

        public string Title;

        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        public enum genre
        {
            [FriendlyName("Commedy")]
            Commedy,
            [FriendlyName("Action")]
            Action,
            [FriendlyName("Other")]
            Etc
        }

        public genre Genre { get; set; }

        // Awards, etc

        public override string ToString()
        {
            return $"{MovieId}: {Title}, {Duration} minutes, {ReleaseDate} release, {Genre}";
        }

        public string GetPrettyTitleLink()
        {
            return Title.ToLower().Replace(' ', '-'); // THIS ISN'T GOOD ENOUGH FOR URL ENCODING BUT WHATEVER. TODO.
        }
    }

    public class Customer
    {
        [AutoIncrement]
        [PrimaryKey]
        public int CustomerId { get; set; }
        public string Name;
    }

    public class Showing
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ShowingId { get; set; }
        public decimal Price;
        
        public DateTime Time;
        public int ScreenNumber;
        public int TotalSeats;
        public int AvailableSeats;

        public int MovieId;
        [Reference]
        public Movie Movie { get; set; }
    }

    public class Purchase
    {
        [AutoIncrement]
        [PrimaryKey]
        public int PurchaseId { get; set; }
        public int MovieId;
        public int ShowingId;
        public int CustomerId;
        public int? PromotionId; // '?' = Nullable => optional

        public DateTime PurchaseTime;
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
        public int PromotionId { get; set; }
        public string PromotionCode;
        public string PromotionName;
        public DateTime Expiration;

        public PromotionType PromoType;

        public enum PromotionType
        {
            [FriendlyName("Flat rate")]
            FlatRate,
            [FriendlyName("Percentage")]
            Percentage,
            [FriendlyName("Free")]
            Free
        }

        public decimal PromoValue; // Percentage discount or flat rate discount
    }

    //public class Concession

    // http://stackoverflow.com/questions/424366/c-sharp-string-enums
    public class FriendlyName : System.Attribute
    {
        private string _value;

        public FriendlyName(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            FriendlyName[] attrs = fi.GetCustomAttributes(typeof(FriendlyName), false) as FriendlyName[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        public static IEnumerable<SelectListItem> GetSelectListItems<T>()
        {
            if (!typeof (T).IsEnum)
                throw new ArgumentException("GetSelectListItems is only implemented for enums");

            var first = true;
            foreach (Enum value in Enum.GetValues(typeof(T)))
            {
                var val = GetStringValue((Enum)value) ?? value.ToString();
                yield return new SelectListItem(val, Convert.ChangeType(value, value.GetTypeCode()).ToString(), first);
                first = false;
            }
        }
    }
}