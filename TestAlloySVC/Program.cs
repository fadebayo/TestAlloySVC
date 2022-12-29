using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Policy;


namespace TestAlloySVC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static public void dispResult(string result, string Fname)
        {
            string dispMsg, dispTitle;
            if (result == "Approved")
            {
                dispTitle = "Application Approved";
                dispMsg = "Congratulations " + Fname + "! your application has being approved and further details will be sent to you shortly";

            }
            else if (result == "Denied")
            {

                dispTitle = "Application Denied";
                dispMsg = "We are sorry " + Fname + ", we are unable to approve your application. Please try again in 3 months or call a Representative";


            }
            else
            {
                dispTitle = "Application under Review";
                dispMsg = "Hi  " + Fname + ", we need some more time to process your application. We will send you an update via email soon.";


            }

            MessageBox.Show(dispMsg, dispTitle);

        }

    }
}
