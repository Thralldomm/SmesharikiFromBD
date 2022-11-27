namespace Roman_To_Int
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BallInfoes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Zodiac_Sign { get; set; }

        [StringLength(50)]
        public string Image { get; set; }
    }
}
