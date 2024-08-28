using COM.JOMA.EMP.CROSSCUTTING.Contants;

namespace COM.JOMA.EMP.CROSSCUTTING.DTOs
{
    public class LogDiskCrossCuttingDto
    {
        public string? FechaHora { set; get; }
        public string? mensaje { set; get; }
        public string? path { set; get; }
        public bool isSubPath { set; get; }
        public string? fileName { set; get; }
        public string? codigoSeguimiento { set; get; }
        public CrossCuttingLogLevel Level { set; get; }
    }
}
