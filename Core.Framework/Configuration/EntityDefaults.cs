using System.Runtime.Serialization;

namespace Core.Framework.Configuration
{    
    [DataContract(Name = "entityDefaults")]
    public class EntityDefaults
    {
        [DataMember(Name = "forceUpperCase")]
        public bool ForceUpperCase { get; set; } = false;

        [DataMember(Name = "max")]
        public int Max { get; set; } = 100;

        [DataMember(Name = "canView")]
        public bool CanView { get; set; } = true;

        [DataMember(Name = "canAdd")]
        public bool CanAdd { get; set; } = false;

        [DataMember(Name = "canModify")]
        public bool CanModify { get; set; } = false;

        [DataMember(Name = "canRemove")]
        public bool CanRemove { get; set; } = false;

        [DataMember(Name = "useLocalTime")]
        public bool UseLocalTime { get; set; } = false;
    }
}
