using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulSistemiFinal
{
    class Veritabani
    {
        string strcon = "Server=MD2PQD4C\\SQLEXPRESS;Initial Catalog=aleynaVize;Integrated Security=True;";
        public SqlConnection conAc()
        {
            SqlConnection bag = new SqlConnection(strcon);
            bag.Open();
            return bag;
        }
        public void conKapa()
        {
            SqlConnection bag = new SqlConnection(strcon);
            bag.Close();
        }
    }
}
