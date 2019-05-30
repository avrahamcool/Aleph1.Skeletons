using System.ComponentModel;

namespace Aleph1.Skeletons.Proxy.Models
{
    /// <summary>נתוני אדם</summary>
    public class Person
    {
        /// <summary>מזהה</summary>
        [DisplayName(@"ת""ז")]
        public int ID { get; set; }

        /// <summary>שם פרטי</summary>
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }

        /// <summary>שם משפחה</summary>
        [DisplayName("שם משפחה")]
        public string LastName { get; set; }
    }
}
