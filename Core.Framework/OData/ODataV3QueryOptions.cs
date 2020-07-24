using System.Collections.Generic;

namespace Core.Framework.OData
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

        public string ProcessOrderByOptions()
        {
            string orderBy = null;
            if (!string.IsNullOrEmpty(OrderBy))
            {
                orderBy = OrderBy.ToUpper() + ",";
                orderBy = orderBy.Replace(" ,", ",");
                orderBy = orderBy.Replace(" ASC,", "_ASC,");
                orderBy = orderBy.Replace(" DESC,", "_DESC,");
                orderBy = orderBy.Trim(new[] {' ', ','});

                var parts = orderBy.Split(',');
                var list = new List<string>();
                foreach (var part in parts)
                {
                    if (!(part.EndsWith("_ASC") || part.EndsWith("_DESC")))
                        list.Add(part + "_ASC");
                    else list.Add(part);
                }
                orderBy = string.Join(",", list.ToArray());
            }

            return orderBy;
        }
    }
}
