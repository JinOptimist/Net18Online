using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Configuration;

namespace WebPortalEverthing.E2E.LoadTest
{
    public class PropertiesService
    {

        /// <summary>
        /// Настройки файл App.config в корне проекта.
        /// Ищет ключ в конфигурации.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public static String KeyConfFound( string key )
        {
            /*  Настройки файл App.config в корне проекта.
            раздел <appSettings> файла App.config: 
            key="exampletxt"  */
            //          String outFile = ConfigurationManager.AppSettings["exampletxt"];
            if( ConfigurationManager.AppSettings.AllKeys.Contains(key) )
            {
                Console.WriteLine($"Ключ {key} найден в конфигурации.");
                if( ConfigurationManager.AppSettings[key] != null )
                {
                    Console.WriteLine($"Значение {key} = {ConfigurationManager.AppSettings[key]}");
                    return ConfigurationManager.AppSettings[key];
                } else
                { throw new NullReferenceException($"Значение {key} = null"); }
            } else
            {
                throw new KeyNotFoundException($"Ключ '{key}' не найден в конфигурации.");
            }
        }
    }
}
