using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace Currency_Convertor
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Decimal? rate;
        private Decimal? numTop = 1;
        private Decimal? numBot = 1;
        private Currency cur;
        // Constructor
        public MainPage()
        {
            cur = new Currency();
            InitializeComponent();           

            topTextBox.Text = "1";            
            bottomTextBox.Text = "1";
            
            topListBox.ItemsSource = new Flags();
            bottomListBox.ItemsSource = new Flags();

            topListBox.SelectedIndex = 0;
            bottomListBox.SelectedIndex = 0;
        }

        private void RefreshApplicationBarIconButton_Click(object sender, EventArgs e)
        {
           MessageBox.Show(cur.Refresh());
        }

        private void AboutApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute));
        }

        private void topTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            bottomTextBox.TextChanged -= bottomTextBox_TextChanged;
            if (rate == null)
            {
                if (!bottomTextBox.Text.Equals("") || !topTextBox.Text.Equals(""))
                {
                    numTop = null;
                    numBot = null;
                    bottomTextBox.Text = "";
                    topTextBox.Text = "";
                }
            }
            else
            {
                if (topTextBox.Text.StartsWith(".") || (topTextBox.Text.Equals("") && !bottomTextBox.Text.Equals("")))
                {
                    numTop = null;
                    numBot = null;
                    topTextBox.Text = "";
                    bottomTextBox.Text = "";
                }
                else if ((Regex.Matches(topTextBox.Text, "\\.").Count == 2))
                {
                    topTextBox.Text = topTextBox.Text.Substring(0, topTextBox.Text.Length - 1);
                    topTextBox.SelectionStart = topTextBox.Text.Length;
                }
                else if (topTextBox.Text.EndsWith(".") || (topTextBox.Text.Equals("") && bottomTextBox.Text.Equals("")))
                {
                    //let it be
                }
                else
                {
                    numTop = Decimal.Parse(topTextBox.Text);
                    numBot = rate * numTop;
                    bottomTextBox.Text = String.Format("{0:0.####}", numBot);
                }
            }
            bottomTextBox.TextChanged += bottomTextBox_TextChanged;
        }

        private void bottomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            topTextBox.TextChanged -= topTextBox_TextChanged;
            if (rate == null)
            {
                if (!bottomTextBox.Text.Equals("") || !topTextBox.Text.Equals(""))
                {
                    numTop = null;
                    numBot = null;
                    bottomTextBox.Text = "";
                    topTextBox.Text = "";
                }
            }
            else
            {
                if (bottomTextBox.Text.StartsWith(".") || (bottomTextBox.Text.Equals("") && !topTextBox.Text.Equals("")))
                {
                    numTop = null;
                    numBot = null;
                    topTextBox.Text = "";
                    bottomTextBox.Text = "";
                }
                else if ((Regex.Matches(bottomTextBox.Text, "\\.").Count == 2))
                {
                    bottomTextBox.Text = bottomTextBox.Text.Substring(0, bottomTextBox.Text.Length - 1);
                    bottomTextBox.SelectionStart = bottomTextBox.Text.Length;
                }
                else if (bottomTextBox.Text.EndsWith(".") || (topTextBox.Text.Equals("") && bottomTextBox.Text.Equals("")))
                {
                    //let it be
                }
                else
                {
                    if (rate != 0)
                    {
                        numBot = Decimal.Parse(bottomTextBox.Text);
                        numTop = numBot / rate;                        
                        topTextBox.Text = String.Format("{0:0.####}", numTop);
                    }
                    else
                    {
                        numTop = null;
                        numBot = null;
                        bottomTextBox.Text = "";
                        topTextBox.Text = "";
                    }
                }
            }
            topTextBox.TextChanged += topTextBox_TextChanged;
        }

        private void topListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            bottomTextBox.TextChanged -= bottomTextBox_TextChanged;
            rate = cur.getRate(topListBox.SelectedIndex, bottomListBox.SelectedIndex);
            if (rate != null)
            {
                middleTextBlock.Text = String.Format("{0:0.00##}", rate);
                if (!topTextBox.Text.Equals(""))
                {
                    numBot = rate * numTop;
                    bottomTextBox.Text = String.Format("{0:0.####}", numBot);
                }
            }
            else
            {
                middleTextBlock.Text = "Unknown";
                bottomTextBox.Text = "";
            }
            bottomTextBox.TextChanged += bottomTextBox_TextChanged;
        }

        private void bottomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            bottomTextBox.TextChanged -= bottomTextBox_TextChanged;
            rate = cur.getRate(topListBox.SelectedIndex, bottomListBox.SelectedIndex);
            if (rate != null)
            {
                middleTextBlock.Text = String.Format("{0:0.00##}", rate);
                if (!topTextBox.Text.Equals(""))
                {
                    numBot = rate * numTop;
                    bottomTextBox.Text = String.Format("{0:0.####}", numBot);
                }
            }
            else
            {
                middleTextBlock.Text = "Unknown";
                bottomTextBox.Text = "";
            }
            bottomTextBox.TextChanged += bottomTextBox_TextChanged;
        }
    }
}