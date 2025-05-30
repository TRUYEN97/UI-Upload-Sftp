using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upload.Service;

namespace Upload.Model
{
    internal class AccessUserListModel
    {
        public HashSet<UserModel> UserModels { get; set; } = new HashSet<UserModel>();
    }
}
