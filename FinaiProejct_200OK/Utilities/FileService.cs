using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Utilities
{
    class FileService
    {
        static StreamReader sr;

        public static string ReadFile(string fileName)
        {
            string fileContent = "";

            try
            {
                sr = new StreamReader(fileName);
                fileContent += sr.ReadToEnd();
            }
            catch (Exception e)
            {
                MessageBox.Show("Reading file occurs error: " + e.Message);
            }

            return fileContent;
        }
    }
}
