namespace Civic.Framework.WebApi
{
    public class ODataV3QueryOptions
    {
        public ODataV3QueryOptions()
        {
            Top = 0;
            Skip = 0;
            InlineCount = false;
        }

        public string Filter
        {
            get;
            internal set;
        }

        public string OrderBy
        {
            get;
            internal set;
        }

        public int Top
        {
            get;
            internal set;
        }

        public int Skip
        {
            get;
            internal set;
        }

        //public string Select
        //{
        //    get;
        //    internal set;
        //}

        public string Expand
        {
            get;
            internal set;
        }

        public bool InlineCount
        {
            get;
            internal set;
        }

        public string Format
        {
            get;
            internal set;
        }

        //public string SkipToken
        //{
        //    get;
        //    internal set;
        //}
    }
}
