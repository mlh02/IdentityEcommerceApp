using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models.Repositories
{
    public class Like
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }

        [ForeignKey("Reviews")]
        public int ReviewID { get; set; }

    }
}
