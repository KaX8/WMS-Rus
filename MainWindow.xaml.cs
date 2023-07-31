using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Path = System.IO.Path;

namespace DikayaMagiya
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, string> list = new Dictionary<string, string>();
        public Dictionary<string, string> randomLong = new Dictionary<string, string>();
        public Dictionary<string, string> randomShort = new Dictionary<string, string>();
        public List<string> randomEff = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            ParseSurges();

        }

        public void ParseSurges()
        {
            string rE = Properties.Resources.randomEff;
            string rM = Properties.Resources.randommagic;

            string[] rMagic = rM.Split("\r\n");
            string[] rEff = rE.Split("\r\n");

            foreach (string s in rMagic) 
            {
                Regex regex = new Regex(@"^([0-9]{1,2}-[0-9]{1,3})|^([1-9][0-9]?[0-9]?)");
                string ids = "000";

                MatchCollection matches = regex.Matches(s);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        ids = match.Value;
                }

                if (s == " ")
                    break;


                list.Add(ids, s.Remove(0, ids.Length));
            }

            foreach (var item in list)
            {
                if (item.Key.Contains('-'))
                {
                    randomLong.Add(item.Key, item.Value.Remove(0, 1));
                }
                else
                {
                    randomShort.Add(item.Key, item.Value.Remove(0, 1));
                }
            }

            foreach (string s in rEff)
            {
                randomEff.Add(s);
            }
        }

        private void btnDice_Click(object sender, RoutedEventArgs e)
        {
            string d = dice.Text;
            magicText1.Text = "";
            magicText2.Text = "";

            foreach (var line in randomLong)
            {
                if (line.Key.Contains(d))
                {
                    magicText1.Text += line.Value + "\n";
                    break;
                }
            }

            foreach (var line in randomShort)
            {
                if (line.Key.Contains(d))
                {
                    magicText2.Text += line.Value + "\n";
                    break;
                }
            }
        }

        private void btn600_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int id1 = r.Next(0,randomEff.Count);
            int id2 = r.Next(0,randomEff.Count);

            magicText1.Text = randomEff[id1];
            magicText2.Text = randomEff[id2];
        }

        private void dice_Click(object sender, RoutedEventArgs e)
        {
            dice.Text = "";
        }
    }
}
