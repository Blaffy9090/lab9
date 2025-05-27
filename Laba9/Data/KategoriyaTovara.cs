using System.ComponentModel.DataAnnotations;

namespace Laba9.Data
{
    public class KategoriyaTovara
    {
        public int KategoriyaTovaraId { get; set; } // КодКатегории (PK)

        [Display(Name = "Название категории")]
        public string? NazvanieKategorii { get; set; }

        // Навигационное свойство для связи с товарами
        public virtual ICollection<Tovar>? Tovary { get; set; }
    }
}
