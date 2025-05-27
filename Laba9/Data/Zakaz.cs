using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba9.Data
{
    public class Zakaz
    {
        public int ZakazId { get; set; } // КодЗаказа (PK)

        [Display(Name = "Количество")]
        public int Kolichestvo { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime DataZakaza { get; set; }

        [Display(Name = "Общая стоимость")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ObshayaStoimost => CenaTovara * Kolichestvo; // Вычисляемое поле

        // Внешний ключ и навигационное свойство
        public int TovarId { get; set; }
        public virtual Tovar? Tovar { get; set; }

        [NotMapped] // Не сохраняется в БД, так как вычисляется
        public decimal CenaTovara => Tovar?.Cena ?? 0;
    }
}
