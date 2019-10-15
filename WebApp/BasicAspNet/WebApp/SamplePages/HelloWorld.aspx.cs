using System;

namespace WebApp.SamplePages
{
    public partial class HelloWorld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            HelloText.Text = "Boo!";
            HelloText.Font.Size = 100;
            HelloText.ForeColor = System.Drawing.Color.Green;
        }
    }
}