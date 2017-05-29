using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ERP.Api.ModelAll
{
    public static class SessionPersister
    {
        public static HttpSessionState Session = HttpContext.Current.Session;
        public static string idSession = "ID";

        public static string userSession = "Username";

        public static string nameSession = "FullName";
        public static string birthdaySession = "Birthday";
        public static string addressSession = "Address";
        public static string statusSession = "Status";
        public static string roleSession = "Role";
        public static string serverSession = "Server";
        public static string serverUserNameSession = "ServerUserName";
        public static string serverPasswordSession = "ServerPassword";
        public static string idNhanvien = "IDNhanVien";
        public static string quyenhanSession = "QuyenHan";
        public static string congtySession = "CongTy";
        public static int ID
        {
            get
            {
                var sessionVar = HttpContext.Current.Session[idSession];
                if (sessionVar != null)
                {
                    return Convert.ToInt16(sessionVar);
                }
                return -1;
            }
            set
            {
                HttpContext.Current.Session[idSession] = value;
            }
        }
        public static string UserName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[userSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[userSession] = value;
            }
        }
        public static string CongTy
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[congtySession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[congtySession] = value;
            }
        }
        public static DateTime? Birthday
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return DateTime.Now;
                }
                var sessionVar = HttpContext.Current.Session[birthdaySession];
                if (sessionVar != null)
                {
                    return sessionVar as DateTime?;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[birthdaySession] = value;
            }
        }
        public static string Address
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[addressSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[addressSession] = value;
            }
        }
        public static string Name
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[nameSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[nameSession] = value;
            }
        }
        public static int? Status
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return -1;
                }
                var sessionVar = HttpContext.Current.Session[statusSession];
                if (sessionVar != null)
                {
                    return Convert.ToInt16(sessionVar);
                }
                return -1;
            }
            set
            {
                HttpContext.Current.Session[statusSession] = value;
            }
        }
        public static bool Role
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return false;
                }
                var sessionVar = HttpContext.Current.Session[roleSession];
                if (sessionVar != null)
                {
                    return Convert.ToBoolean(sessionVar);
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session[roleSession] = value;
            }
        }
        public static string Server
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionVar = HttpContext.Current.Session[serverSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[serverSession] = value;
            }
        }
        public static string ServerUserName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionVar = HttpContext.Current.Session[serverUserNameSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[serverUserNameSession] = value;
            }
        }
        public static string ServerPassword
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionVar = HttpContext.Current.Session[serverPasswordSession];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[serverPasswordSession] = value;
            }
        }

        public static int IDNhanVien
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                var sessionVar = HttpContext.Current.Session[serverPasswordSession];
                if (sessionVar != null)
                {
                    return Convert.ToInt32(sessionVar);
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[serverPasswordSession] = value;
            }
        }
    }
}