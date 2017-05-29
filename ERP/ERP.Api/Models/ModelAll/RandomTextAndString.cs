using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ERP.Web.Models.BusinessModel
{
    public class RandomTextAndString
    {
        public string RandomString(int size)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            int d;
            Random rand = new Random();
            for (int i = 0; i < size / 2; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                d = rand.Next(0, 9);

                sb.Append(c);
            }
            for (int i = 0; i < size / 2; i++)
            {

                d = rand.Next(0, 9);

                sb.Append(d);
            }
            return sb.ToString();

        }
    }
}