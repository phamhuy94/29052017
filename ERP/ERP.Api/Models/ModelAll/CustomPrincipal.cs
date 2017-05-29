using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ERP.Web.Security
{
    public class CustomPrincipal : IPrincipal
    {

        public CustomPrincipal() {
        }
        public IIdentity Identity
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}