using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stream
{
    public partial class Form1 : Form
    {
        private static string URL = "http://ifirstrowpt.eu/";
        private HashSet<String> links = new HashSet<String>();
        private int currentStream = 0;
        private string oldTeam = "";

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Handles the button click
        private void button1_Click(object sender, EventArgs e)
        {
            startLooking();
        }

        private void startLooking()
        {
            String team = textBox1.Text;

            if (team.Length == 0)
            {
                MessageBox.Show("Team must not be empty..");
                return;
            }

            if (links.Count != 0)
            {
                currentStream = ++currentStream % links.Count;
                button1.Text = getButtonString();
                startStream();
                return;
            }

            team = parseTeamName(team);
            oldTeam = team;

            String html = getHtml(URL);

            getLinks(team, html);

            label2.Visible = true;
            labelResult.Text = links.Count + " Streams found.";

            if (links.Count != 0)
            {
                button1.Text = getButtonString();
            }

            startStream();
        }

        private string getButtonString()
        {
            if(links.Count == 0)
            {
                return "Start!";
            }
            else
            {
                return "Next " + (currentStream+1) + "/" + links.Count;
            }
        }

        private void startStream()
        {
            if (links.Count == 0)
            {
                MessageBox.Show("No streams found..");
                return;
            }

            Process.Start("chrome", @"--app=" + URL + links.ElementAt(currentStream));
        }

        // Parses the team name
        private string parseTeamName(string team)
        {
            return team.ToLower().Replace(" ", "-");
        }

        // Gets the html of a page
        private String getHtml(String url)
        {
            String data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();      
            }

            return data;
        }

        // Gets a list of links from the received html containing the receveid team
        private void getLinks(String team, String html)
        {
            string pat = @"(\/watch\/\d+\/\d\/\watch-.*?" + team + ".*?\\.html)";

            Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(html);
            int matchCount = 0;
            while (m.Success)
            {
                Group g = m.Groups[1];
                links.Add(g.Value);
                m = m.NextMatch();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Text = "Start!";
            labelResult.Text = "";
            label2.Visible = false;
            currentStream = 0;
            links = new HashSet<string>();
        }

      

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                startLooking();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
