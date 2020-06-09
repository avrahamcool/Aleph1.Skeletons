namespace Aleph1.Skeletons.WebAPI.Models
{
    /// <summary>נתוני אדם</summary>
    public class Person
    {
        /// <summary>מזהה</summary>
        public int ID { get; set; }

        /// <summary>שם פרטי</summary>
        public string FirstName { get; set; }

        /// <summary>שם משפחה</summary>
        public string LastName { get; set; }
    }
}
