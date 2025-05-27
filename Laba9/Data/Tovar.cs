using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba9.Data
{
    public class Tovar
    {
        public int TovarId { get; set; } // КодТовара (PK)

        [Display(Name = "Название товара")]
        public string? NazvanieTovara { get; set; }

        [Display(Name = "Описание товара")]
        public string? OpisanieTovara { get; set; }

        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cena { get; set; }

        [Display(Name = "Категория")]
        public int KategoriyaTovaraId { get; set; } // КодКатегории (FK)

        // Навигационные свойства
        public virtual KategoriyaTovara? KategoriyaTovara { get; set; }
        public virtual ICollection<Zakaz>? Zakazy { get; set; }
    }
}