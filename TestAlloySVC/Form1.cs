using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Security.Policy;
using System.Net;
using System.IO;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Configuration;


namespace TestAlloySVC
{
    public partial class Form1 : Form
    {

        class Client
        {
            public string name_first { get; set; }
            public string name_last { get; set; }
            public string address_line_1 { get; set; }
            public string address_city { get; set; }

            public string address_state { get; set; }

            public string address_postal_code { get; set; }

            public string address_country_code { get; set; }

            public string document_ssn { get; set; }

            public string birth_date { get; set; }

            public string email_address { get; set; }

            public string address_line_2 { get; set; }



        }

        class summary
        {
            public string outcome { get; set; }
        }

        
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private  async void  button1_Click(object sender, EventArgs e)
        {

            var reqPayload = new Client();
            reqPayload.address_city = txtCity.Text;
            reqPayload.address_country_code = "US";
            reqPayload.address_line_1 = txtAdd1.Text;
            reqPayload.address_line_2 = txtAdd2.Text;
            reqPayload.address_postal_code = txtZip.Text;
            reqPayload.address_state = cmbState.Text;
            DateTime aDate = DateTime.Parse(txtDOB.Text);
            if (aDate.Date != DateTime.Now.Date)
                reqPayload.birth_date = aDate.ToString("yyyy-MM-dd");

            reqPayload.document_ssn = txtSSN.Text;
            reqPayload.name_first = txtFname.Text; 
            reqPayload.name_last = txtLname.Text;
            reqPayload.email_address = txtEmail.Text;


            await SendRequest(reqPayload);
            
        }

        static async Task SendRequest(Client J)
        {
            HttpClient client = new HttpClient();
            string URL = ConfigurationManager.AppSettings["URL"];
            string Token = ConfigurationManager.AppSettings["Token"];
            string Secret = ConfigurationManager.AppSettings["Secret"];
            string AuthKey = Token+ ":" + Secret;

            client.BaseAddress = new Uri(URL);
            var reqPayload = J;

            

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(AuthKey);
            string val = System.Convert.ToBase64String(plainTextBytes);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));


            // List data response.
            HttpResponseMessage response = await client.PostAsJsonAsync("", reqPayload);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                                                                                     //HttpResponseMessage response = client.GetAsync("").Result;
          

           


            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(responseBody);

                JObject obj = JObject.Parse(responseBody);


                string attributes = obj["summary"]["outcome"].ToString();

                Program.dispResult(attributes, J.name_first);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

            }

            
             client.Dispose();


        }

       
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
