using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POTC
{
    public static class Extensions
    {
        public static string CleanSQL(this String str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str.Replace("'", "''");
            }
            else
            {
                return str;
            }
        }

        public static string Translate(string english, string spanish, string language)
        {
            if (language == "sp")
            {
                return spanish;
            }
            else
            {
                return english;
            }
        }

        public static string Translate(string english, string french, string spanish, string language)
        {
            if (language == "fr")
            {
                return french;
            }
            else if (language == "sp")
            {
                return spanish;
            }
            else
            {
                return english;
            }
        }
    }    
}