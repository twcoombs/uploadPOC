using System.IO;
using System.Collections.Generic;

namespace uploadPOC.Classes
{
    class CSVRead {
        public List<string> readCSV(string path)
        {
            List<string> listA = new List<string>();
            string[] lines = new string[] { };

            using (var reader = new StreamReader(path))
            {
                if (File.Exists(path))
                {
                    lines = File.ReadAllLines(path);

                }

            }

            var line = lines[0];
            var values = line.Split(',');

            foreach (string v in values)
            {
                listA.Add(v);
            }



            return listA;
        }
    }

}