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

namespace Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Costruttore
        public MainPage()
        {
            InitializeComponent();

            //System.Windows.Controls.Primitives.Popup p = new System.Windows.Controls.Primitives.Popup();

            //Image im = new Image();
            //im.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("ApplicationIcon.png", UriKind.Relative));
            //im.Width = 300; im.Height = 300;

            //TextBlock t = new TextBlock();
            //t.Text = "Prova";
            //t.FontSize = 30;
            //Border b = new Border();
            //b.Background = new SolidColorBrush(Colors.Red);
            //b.Width = 200; b.Height = 200;
            //b.Child = t;

            //Grid g = new Grid();
            //g.Children.Add(im);
            //g.Children.Add(b);

            //p.Child = g;
            //p.IsOpen = true;
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            new TutorialMessage.TutorialMessage("Messaggio breve", this.button1, 200, this.LayoutRoot, 2, true).Show();
            new TutorialMessage.TutorialMessage("Messaggio prova così lungo che non c'entra piùùùùùùùùùùùù e quindi lo allungo di nuovo per fare un testo.", 100, 520, 300, this.LayoutRoot, 1, true).Show();
        }
    }
}