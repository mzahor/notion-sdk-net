﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Notion.Client
{
    public class DatabasesUpdateParameters : IDatabasesUpdateBodyParameters
    {
        public Dictionary<string, IUpdatePropertySchema> Properties { get; set; }

        public List<RichTextBaseInput> Title { get; set; }

        public IPageIcon Icon { get; set; }

        public FileObject Cover { get; set; }

        public bool Archived { get; set; }

        public bool? IsInline { get; set; }

        public string Description { get; set; }
    }
}
