using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageReco_17
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PO_Image_Bmp imgRec = new PO_Image_Bmp();
                imgRec.imagePoling();
            }
            catch(Exception ex)
            {
                Logger.Error("Main Exception", ex);
            }
        }
    }
}
