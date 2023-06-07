using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataModel
{
    public class Category: BaseDataModel<int>
    {
        public string CategoryName { get; set; }
    }
}
