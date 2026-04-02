namespace ContratoQR.Entity
{
    public class PersonalEntity
    {
        public int IdPersonal { get; set; }

        public int IdEstablecimiento { get; set; }

        public int IdTipoContrato { get; set; }

        public string? RutPersonal { get; set; }

        public string? NombrePersonal { get; set; }

        public string? ApellidoPersonal { get; set; }

        public string? NombreCargo { get; set; }

        public string? NombreEstablecimiento { get; set; }

        public string? NombreTipoContrato { get; set; }

        public string? FecInicioContrato { get; set; }

        public int NroFicha { get; set; }

        public int NroHora { get; set; }

        public string? Categoria { get; set; }

        public int Nivel { get; set; }

        public string? TipoFuncionario { get; set; }

        public int DiaTrabajado { get; set; }

        public string? UrlContrato { get; set; }

        public int IndEstado { get; set; }

        public string? IdUsuario { get; set; }

        public string? NombreCompleto => $"{NombrePersonal} {ApellidoPersonal}";

        public string? CorreoElectronico { get; set; }

        public int IdTipoFuncionario { get; set; }

        public string? Cometido { get; set; }

        public int IdPersonalHonorario { get; set; }

    }
}
