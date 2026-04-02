namespace ContratoQR.Entity
{
    public class PersonalHonorarioEntity
    {
        public int IdPersonalHonorario { get; set; }
        public string? RutPersonal { get; set; }
        public string? NombrePersonal { get; set; }
        public DateTime FecNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? NombreComuna { get; set; }
        public string? Sexo { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NombreCargo { get; set; }
        public string? NroDecreto { get; set; }
        public string? Cometido { get; set; }
        public string? NombrePrograma { get; set; }
        public string? NroCuentaPresupuestaria { get; set; }
        public string? Monto { get; set; }
        public string? NombreEstablecimiento { get; set; }
        public DateTime FecIngreso { get; set; }
        public DateTime FecInicioContrato { get; set; }
        public DateTime FecTerminoContrato { get; set; }
        public string? UrlContrato { get; set; }
        public string? IdUsuario { get; set; }
    }
}
