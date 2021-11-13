using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestParser
{
    public partial class MainForm : Form
    {
        private bool flag;


        private List<Game> _games;
        private List<Game> Games
        {
            get => _games;
            set
            {
                _games = value;
                if (_games.Count > 0)
                    Invoke(new MyDelegate(() => { InsertInDb(value); }));
                    

            }
        }

        delegate void MyDelegate();
        public void InsertInDb(List<Game> games)
        {

            LogTBox.Text = DateTime.Now + " Данные добавлены в БД\n"+LogTBox.Text;
            Application.DoEvents();
        }





        public MainForm()
        {
            InitializeComponent();





        }

        

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Task task=new Task(() =>
            {
                string url = "https://www.footballrussia.pro/";
                WebClient client = new WebClient {Encoding = Encoding.UTF8};
                flag = true;
                while (flag) 
                {
                    try
                    {
                        string html = client.DownloadString(url);
                        Regex reg = new Regex("<div class=\"day-matches_team-host\">([^<]*)</div>[^<]*<div class=\"day-matches_score-time\" style=\"font-size: 1.0em; background:[^;]*; color: [^;]*;\">([^:<]*)</div>[^<]*<div class=\"day-matches_team-visitor\">([^<]*)</div>");
                        Games = reg.Matches(html).Cast<Match>().Where(t => t.Groups.Count == 4).Select(t => new Game(t.Groups[1].Value, t.Groups[2].Value, t.Groups[3].Value)).ToList();


                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        Invoke(new MyDelegate(Close));
                    }
                    
                }

            });
            task.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            flag = false;
        }
    }
}
