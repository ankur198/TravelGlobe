using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGlobe.Model
{
    public class Ikigai
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public virtual List<Position> Positions { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Positions
    {
        goodAt, profession, paidFor, vocation, worldNeeds, mission, youLove, passion, purpose
    }

    public class Position
    {
        public int ID { get; set; }

        public Positions position { get; set; }

        public int Ikigai_ID { get; set; }

        public virtual List<Activity> Activities { get; set; }

    }

    public class Activity
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
