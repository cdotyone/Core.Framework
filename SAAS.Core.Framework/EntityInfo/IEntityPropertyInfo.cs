namespace SAAS.Core.Framework
{
    public interface IEntityPropertyInfo
    {
        string Name { get; set; }

        bool IsNullable { get; set; }

        bool IsKey { get; set; }

        bool ForceUpperCase { get; set; }

        string Type { get; set; }

        int? MaxLength { get; set; }

        int MaxQuery { get; set; } 
    }
}