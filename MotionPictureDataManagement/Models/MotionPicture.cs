using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MotionPictureDataManagement.API.Models
{
    public class MotionPicture
    {
        // PK / NN
        public int? Id { get; set; }
        // NVARCHAR(50)/NN
        [Required]
        public string Name { get; set; }
        // NVARCHAR(500)/N
        public string Description { get; set; }
        // INT /NN
        [Required]
        public int ReleaseYear { get; set; }
    }
}
