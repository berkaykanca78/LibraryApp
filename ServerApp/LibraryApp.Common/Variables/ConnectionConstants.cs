using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Common.Variables
{
    public static class ConnectionConstants
    {
        public static string DbConnectionString { get; } = "Data Source=DESKTOP-IQ4D12E\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Trust Server Certificate=true;MultipleActiveResultSets=true;";
        public static string JwtKey { get; } = "a90244f19c883e32acb2c721099c9a7f2763273f1660c39e826cbc6943d4c26e9fb947655e7f529dadbf0eaa2159173e3f742edf07cd8654b96c40a5c07827f8";
    }
}
