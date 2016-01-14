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
using System.Windows.Forms;

namespace Stream
{
    public partial class StreamView : Form
    {

        private class Stream
        {
            public string title;
            public string link;

            public Stream(string title, string link)
            {
                this.title = title;
                this.link = link;
            }
        }

        private static string URL = "http://ifirstrowpt.eu/";
        private LinkedList<Stream> links = new LinkedList<Stream>();
        private int currentStream = 0;
        private string oldTeam = "";

        public StreamView()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void team_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(Keys.Enter)))
            {
                links = new LinkedList<Stream>();
                link_list.Items.Clear();
                startLooking();
            }
        }

        private void startLooking()
        {
            String teamString = team.Text;

            if (teamString.Length == 0)
            {
                MessageBox.Show("Team must not be empty..");
                return;
            }

            teamString = parseTeamName(teamString);
            oldTeam = teamString;

            String html = getHtml(URL);

            getLinks(teamString, html);
            if (links.Count == 0)
            {
                MessageBox.Show("No streams found..");
            }
            else
            {
                fillListView();
            }
        }

        private void fillListView()
        {
            int i=1;
            foreach (var streamLink in links)
            {
                string[] row = { streamLink.title, streamLink.link };
                var listViewItem = new ListViewItem(row);
                link_list.Items.Add(listViewItem);
                i++;
            }
        }

        private void startStream(int idx)
        {
            Process.Start("chrome", @"--app=" + URL + links.ElementAt(idx).link);
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
            while (m.Success)
            {
                Group g = m.Groups[1];
                int startIndex = g.Value.LastIndexOf("watch-") + "watch-".Length;
                int length = g.Value.IndexOf(".", startIndex) - startIndex;
                string title = length < 0 ? "Unknown" : g.Value.Substring(startIndex, length);
                links.AddLast(new Stream(title, g.Value));
                m = m.NextMatch();
            }
        }

        private void link_list_DoubleClick(object sender, EventArgs e)
        {
            int idx = link_list.SelectedItems[0].Index;
            startStream(idx);
        }
    }
}
