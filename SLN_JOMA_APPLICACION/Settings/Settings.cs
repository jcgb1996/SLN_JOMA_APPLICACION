namespace SLN_COM_JOMA_APPLICACION.Settings
{
    public class Settings
    {
        public Settings()
        {
            GSEDOC_BR = new DBSettings();
            GSEDOC_BW = new DBSettings();
        }
        public DBSettings GSEDOC_BR { get; set; }
        public DBSettings GSEDOC_BW { get; set; }
    }

    public class DBSettings
    {
        public string? DataSource { get; set; }
        public string? InitialCatalog { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public long Timeout { get; set; }
        public byte TipoORM { get; set; }
    }
}


//sdcsdfsdf7sdfsdfsdffdfsd
//comentario de orueba