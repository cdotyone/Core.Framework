namespace Framework.Core
{
    public class EntityPropertyInfo : IEntityPropertyInfo
    {
        public string Name { get; set; }

        public bool IsNullable { get; set; }

        public bool IsKey { get; set; }

        public bool ForceUpperCase { get; set; }

        public string Type { get; set; }

        public int? MaxLength { get; set; }

        public int MaxQuery { get; set; } = 100;
    }
}
