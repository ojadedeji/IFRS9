using System.ComponentModel.DataAnnotations;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class MultiSelectDropDown
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string icon { get; set; }

        [Required]
        public string maker { get; set; }

        [Required]
        public bool ticked { get; set; }

    }
}
