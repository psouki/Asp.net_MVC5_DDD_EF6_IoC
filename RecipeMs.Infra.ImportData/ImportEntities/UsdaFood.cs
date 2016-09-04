namespace RecipeMs.Infra.ImportData.ImportEntities
{
    public class UsdaFood
    {
        public int UsdaRefId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Subgroup { get; set; }
        public string InitialStage { get; set; }
        public string FinalStage { get; set; }
        public string Complement { get; set; }
        public string RefuseDesc { get; set; }
        public decimal Refuse { get; set; }
        public decimal ProteinFactor { get; set; }
        public decimal FatFactor { get; set; }
        public decimal CarbFactor { get; set; }
    }
}
