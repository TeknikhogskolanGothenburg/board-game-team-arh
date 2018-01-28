using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipNet.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Render Dictionary with string-key and object value to JSON string 
        /// </summary>
        /// <param name="toRender">Dictionary to render</param>
        /// <returns>JSON string</returns>
        public static string Render(Dictionary<string, object> toRender)
        {
            string json = "";

            foreach (KeyValuePair<string, object> pair in toRender)
            {
                
                if (json.Length > 0)
                    json += ",\n";

                json += "\"" + pair.Key + "\": ";

                json += RenderValue(pair.Value);
                
            }

            return json;
        }

        /// <summary>
        /// Choose render technic by object type and return value as JSON string
        /// </summary>
        /// <param name="toRender">Value object to render</param>
        /// <returns>JSON string with value</returns>
        public static string RenderValue(object toRender)
        {
            string json = "";

            if (toRender is int || toRender is Int16 || toRender is Int64 || toRender is long || toRender is float || toRender is double || toRender is decimal)
            {
                json += toRender;
            }
            else if (toRender is List<object>)
            {
                json += RenderList<object>((List<object>)toRender);
            }
            else if (toRender is List<Dictionary<string, object>>)
            {
                json += RenderList<Dictionary<string, object>>((List<Dictionary<string, object>>)toRender);
            }
            else if (toRender is List<string>)
            {
                json += RenderList<string>((List<string>)toRender);
            }
            else if (toRender is Dictionary<string, object>)
            {
                json += RenderDictionary((Dictionary<string, object>)toRender);
            }
            else
            {
                json += "\"" + toRender.ToString() + "\"";
            }

            return json;
        }

        /// <summary>
        /// Render List with Generic to JSON string
        /// </summary>
        /// <param name="toRender">List with generic to render</param>
        /// <returns>Return JSON string</returns>
        public static string RenderList<T>(List<T> toRender)
        {
            string json = "[";

            for (int i = 0; i < toRender.Count; i++)
            {
                if (i > 0)
                    json += ",";

                json += RenderValue(toRender.ElementAt(i));
            }

            json += "]\n";

            return json;
        }

        /// <summary>
        /// Render Dictionary with string key and object value to JSON string
        /// </summary>
        /// <param name="toRender">Dictonary to render</param>
        /// <returns>JSON string</returns>
        public static string RenderDictionary(Dictionary<string, object> toRender)
        {
            string json = "{";

            json += Render(toRender);

            json += "}\n";

            return json;
        }
    }
}