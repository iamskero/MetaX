using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MetaX
{
    public static class Parser
    {
        /// <summary>
        /// Parse n exchanges
        /// </summary>
        /// <param name="fileLoc"></param>
        /// <param name="count"></param>
        public static List<object> ParseNExchanges(string fileLoc, int count = 10)
        {
            if (File.Exists(fileLoc))
            {
                File.ReadLines(fileLoc)
                    .Take(count)
                    .ToList()
                    .ForEach(l =>
                    {

                    });


                return null;
            }
            else throw new FileNotFoundException("Wrong file path");

        }
    }
}
