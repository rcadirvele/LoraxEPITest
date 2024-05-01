namespace InterviewTestMid.Model
{
    public class Part
    {
        public int PartId { get; set; }
        public string? PartNbr { get; set; }
        public string? PartDesc { get; set; }
        public Meta? Meta { get; set; }
        public PartWeight? PartWeight { get; set; }
        public bool ConversionsApplied { get; set; }
        public List<MaterialEntry>? Materials { get; set; }
    }

    public class Meta
    {
        public PartClassification? PartClassification { get; set; }
        public PartMasterType? PartMasterType { get; set; }
        public PartColour? PartColour { get; set; }
        public PartOpacity? PartOpacity { get; set; }
    }

    public class PartClassification
    {
        public int LookId { get; set; }
        public string? LookNbr { get; set; }
        public string? LookDesc { get; set; }
        public string? LookExtra { get; set; }
    }

    public class PartMasterType
    {
        public int LookId { get; set; }
        public string? LookNbr { get; set; }
        public string? LookDesc { get; set; }
    }

    public class PartColour
    {
        public int LookId { get; set; }
        public string? LookNbr { get; set; }
        public string? LookDesc { get; set; }
    }

    public class PartOpacity
    {
        public int LookId { get; set; }
        public string? LookNbr { get; set; }
        public string? LookDesc { get; set; }
    }

    public class PartWeight
    {
        public int UoM { get; set; }
        public double Value { get; set; }
    }

    public class MaterialEntry
    {
        public Material? Material { get; set; }
        public double Percentage { get; set; }
        public bool? MatrIsBarrier { get; set; }
        public bool? MatrIsDensifier { get; set; }
        public bool? MatrIsOpacifier { get; set; }
    }

    public class Material
    {
        public int LookId { get; set; }
        public string? LookNbr { get; set; }
        public string? LookDesc { get; set; }
    }
}
