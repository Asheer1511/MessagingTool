using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingTool.Models
{
    public class TwitterUsers
    {
        public SortedDictionary<string, List<string>> UsersAndFollowers { get; set; }
    }
}
