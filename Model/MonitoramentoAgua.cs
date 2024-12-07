using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Model
{
    public class MonitoramentoAgua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica auto-incremento
        public long? ID_MONITORAMENTO_AGUA { get; set; }
        public DateTime DT_HORA { get; set; } = DateTime.Now;
        public string LC_LOCALIZACAO { get; set; }
        public int QT_PH { get; set; }
        public int QT_OXIGENIO_DISSOLVIDO { get; set; }
        public int QT_TURBIDEZ { get; set; }
        public int QT_COLIFORMES_TOTAIS { get; set; }
        public int QT_FOSFORO_TOTAL { get; set; }

    }
}
