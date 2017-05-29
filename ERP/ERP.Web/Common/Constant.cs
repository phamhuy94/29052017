using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Common
{
    public class Constant
    {
        public static int FAIL = -1;
        public static string SYSTEM_ERROR = "Lỗi hệ thống. Vui lòng thử lại hoặc liên hệ với admin";
        public static string LOGIN_FAIL = "Tài khoản hoặc mật khẩu không đúng. Vui lòng thử lại";
        public static int INPUT_ERROR = -2;
        public static int SUCCESS = 0;
    }

    public class ChungTuConstant {
        public static string CHI = "CHI_TM";
        public static string CHUYENKHO = "CHUYENKHO";
        public static string HDBH = "HDBH";
        public static string HDMH = "HDMH";
        public static string NHAP = "NHAPKHO";
        public static string NTTK = "NTTK";
        public static string THU = "THU";
        public static string UNC = "UNC";
        public static string XUAT = "XUATKHO";
    }
    public class DMCHUNGTU
    {
        public static string CHUYENKHO = "Chứng từ chuyển kho";
        public static string NHAPKHO = "Hóa đơn nhập hàng";
        public static string XUATKHO = "Hóa đơn xuất hàng";
    }
}