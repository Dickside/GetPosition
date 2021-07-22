using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, EventArgs e)
        {
            string line = "";
            using (WebClient wc = new WebClient())
                line = wc.DownloadString($"http://ipwhois.app/json/{textBox1.Text}");
            Match match = Regex.Match(line, "\"ip\":\"(.*?)\",(.*?)\"country\":\"(.*?)\",(.*?)\"city\":\"(.*?)\",(.*?)\"latitude\":(.*?),(.*?)\"longitude\":(.*?),");
            label1.Text = match.Groups[3].Value + "\n" + match.Groups[5].Value;
            label3.Text = match.Groups[1].Value;
            label2.Text = "Если вы не указывали IP"+"\n" + "адрес,то поиск выполнится по"+"\n"+"вашему IP";
            textBox2.Text = match.Groups[7].Value;
            textBox3.Text = match.Groups[9].Value;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, "[^0-9-.]"))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
                MessageBox.Show("Только цифры!");
                
            }
        }

       

        

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.Replace(".", ",");
            textBox3.Text = textBox3.Text.Replace(".", ",");

            gMapControl1.ShowCenter = false;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;

            double lat = Convert.ToDouble(textBox2.Text);
            double lon = Convert.ToDouble(textBox3.Text);
            gMapControl1.Position = new PointLatLng(lat,lon);

            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 20;

            PointLatLng point = new PointLatLng(lat,lon);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.green);

            
            GMapOverlay markers = new GMapOverlay("markers");

            markers.Markers.Add(marker);

            gMapControl1.Overlays.Add(markers);      
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
