using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceFiap.Model
{
    public class MonitoramentoAr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? ID_MONITORAMENTO_AR { get; set; }
        public DateTime DT_HORA { get; set; } = DateTime.Now;
        public string LC_LOCALIZACAO { get; set; }

        // Ajustar tipo para decimal, pois conforme o DDL temos valores em casas decimais
        public decimal? QT_MONOXIDO_CARBONO { get; set; }
        public decimal? QT_OZONIO { get; set; }
        public decimal? QT_DIOXIDO_NITROGENIO { get; set; }
        public decimal? QT_DIOXIDO_ENXOFRE { get; set; }
    }
}
