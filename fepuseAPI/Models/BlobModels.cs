using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class BlobUploadModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSizeInBytes { get; set; }
        public long FileSizeInKb { get { return (long)Math.Ceiling((double)FileSizeInBytes / 1024); } }
    }

    public class BlobDownloadModel
    {
        public int Id { get; set; }
        public MemoryStream BlobStream { get; set; }
        public string BlobFileName { get; set; }
        public string BlobContentType { get; set; }
        public long BlobLength { get; set; }
    }

    public class ImagenPersona : BlobUploadModel
    {
        //fpaz: relacion 1 a m con Persona (uno)
        public int PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }

    public class ImagenLiga : BlobUploadModel
    {
        //fpaz: relacion 1 a m con Liga (uno)
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }
    }

    public class ImagenTorneo : BlobUploadModel
    {
        //fpaz: relacion 1 a m con Torneo (uno)
        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }
    }

    public class ImagenEquipo : BlobUploadModel
    {
        //fpaz: relacion 1 a m con Equipo (uno)
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }
    }
}