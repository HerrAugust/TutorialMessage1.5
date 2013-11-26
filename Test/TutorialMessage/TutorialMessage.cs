//TutorialMessage for Windows Phone 7.1; version 1.5.1; OpenSource; Reference to GeheimerSchatz.altervista.org; Images created with Microsoft PowerPoint 2010; released 26 Aug 2013
//Thanks for http://social.msdn.microsoft.com/Forums/it-IT/77e5b48c-27e3-4fc8-8c97-d61cbee91848/come-trovare-la-posizione-assoluta-di-un-control

//Added GetPopup, so that developers can define TutorialMessage's events (for example, close the tapped TutorialMessage)
//Messaggio property is now public, so that you can use it to identify TutorialMessage

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
//using System.Diagnostics;

namespace TutorialMessage
{
    /**
     * Example:
     * [Esempio.cs, Loaded eventhandler]
     * TutorialMessage m = new TutorialMessage("testo esempio", this.Button_ok, 120, this.LayoutRoot, 1, true);
     * m.Show();
     * m.GetPopup().Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(MainPage_Tap);
     **/

    public class TutorialMessage
    {
        /// <summary>
        /// TutorialMessage is basically a Popup
        /// </summary>
        private System.Windows.Controls.Primitives.Popup popup;

        /// <summary>
        /// It is the message that will be shown in the popup image
        /// </summary>
        public TextBlock Messaggio { get; set;}
        /// <summary>
        /// It is the number of the current tutorial, useful if many tutorialmessages are shown to indicate consequential actions from the user
        /// </summary>
        TextBlock Numero { get; set; }

        /// <summary>
        /// represents the control to which the message will point
        /// </summary>
        Control ControlloPuntato { get; set; }
        /// <summary>
        /// It will only used when ControlloPuntato will be non-defined by developer, for example with an AppBarIconButton, that does not contain Absolute Positions (id est the distance from the perimeter of the screen
        /// </summary>
        double PopupVerticalOffset { set; get; }
        /// <summary>
        /// It will only used when ControlloPuntato will be non-defined by developer, for example with an AppBarIconButton, that does not contain Absolute Positions (id est the distance from the perimeter of the screen
        /// </summary>
        double PopupHorizontalOffset { set; get; }

        /// <summary>
        /// It is the Width of the TutorialMessage's  popup
        /// </summary>
        double Larghezza { get; set; }
        /// <summary>
        /// It is needed to show the Popup on the screen. It represents the place where the TutorialMessage will be shown
        /// </summary>
        Grid Layout { get; set; }
        /// <summary>
        /// If showing the number of the TutorialMessage is not necessary, the developer can hide it
        /// </summary>
        bool NumeroVisibile { get; set; }

        //ONLY FOR APPLICATIONBARICONBUTTON
        /// <summary>
        /// The number of ApplicationBarIconButtons contained in the ApplicationBar
        /// </summary>
        int NIcone { get; set; }
        /// <summary>
        /// The index (starting from 0). "ApplicationBar.Buttons[THIS]": "THIS" is requested
        /// </summary>
        int IndexIcona { get; set; }
        /// <summary>
        /// The measure in pixel of the  ApplicationBarIconButton which TutorialMessage (id est the could cartoon) should point. Note: it is supposed that the height and the width of the ApplicationBarIconButton are equal!
        /// </summary>
        double SizeIcona { get; set; }

        /// <summary>
        /// Represents a cloud of cartoon in which a tutorial-message can be positioned. Referred to elements that derive by Control
        /// </summary>
        /// <param name="testo">The tutorial-message in the cloud</param>
        /// <param name="controlloPuntato">The Control to which the Tutorial is referred</param>
        /// <param name="larghezza">The Width of the cloud of cartoon -Height is automatically set</param>
        /// <param name="layout">The layout (Grid) in which the cloud must be positioned on the screen</param>
        /// <param name="numeroTutorial">The number of this tutorial-message</param>
        /// <param name="numeroVisibile">Explains if the number of this tutorial-message is visible</param>
        public TutorialMessage(string testo, Control controlloPuntato, double larghezza, Grid layout, int numeroTutorial, bool numeroVisibile = true)
        {
            this.Messaggio = new TextBlock { Text = testo, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(28,5,5,5), FontSize = 20 };
            this.Numero = new TextBlock { Text = numeroTutorial.ToString(), Margin = new Thickness(7,-2,0,0), FontSize = 18 };
            this.ControlloPuntato = controlloPuntato;
            this.Larghezza = larghezza;
            this.Layout = layout;
            this.NumeroVisibile = numeroVisibile;
        }

        /// <summary>
        /// Represents a cloud of cartoon in which a tutorial-message can be positioned. It is referred only to a ApplicationBarIconButton. Note: it is available only for standard icons and AppBarIconButtons
        /// </summary>
        /// <param name="testo">The tutorial-message in the cloud</param>
        /// <param name="nIcone">The number of ApplicationBarIconButtons contained in the ApplicationBar</param>
        /// <param name="indexIcona">The index (starting from 1, that's to say humanly counting). "ApplicationBar.Buttons[THIS]": "THIS" + 1 is requested</param>
        /// <param name="larghezza">The Width of the cloud of cartoon -Height is automatically set</param>
        /// <param name="layout">The layout (Grid) in which the cloud must be positioned on the screen</param>
        /// <param name="numeroTutorial">The number of this tutorial-message</param>
        /// <param name="numeroVisibile">Explains if the number of this tutorial-message is visible</param>
        /// <param name="pathIcona">The path of the image of the ApplicationBarIconButton that the cloud cartoon (id est TutorialMessage) should point. Example: "/Images/icon.png"</param>
        public TutorialMessage(string testo, int nIcone, int indexIcona, Uri pathIcona, double larghezza, Grid layout, int numeroTutorial, bool numeroVisibile = true)
        {
            this.Messaggio = new TextBlock { Text = testo, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(28, 5, 5, 5), FontSize = 20 };
            this.Numero = new TextBlock { Text = numeroTutorial.ToString(), Margin = new Thickness(7, -2, 0, 0), FontSize = 18 };
            this.Larghezza = larghezza;
            this.Layout = layout;
            this.NumeroVisibile = numeroVisibile;
            this.ControlloPuntato = null;
            this.NIcone = nIcone;
            this.IndexIcona = indexIcona;
            this.SizeIcona = new BitmapImage(pathIcona) { CreateOptions = BitmapCreateOptions.None }.PixelWidth;
            System.Diagnostics.Debug.WriteLine("width icona = " + this.SizeIcona.ToString());
        }

        /// <summary>
        /// Represents a cloud of cartoon in which a tutorial-message can be positioned. Referred to a non-Control element
        /// </summary>
        /// <param name="testo">The tutorial-message in the cloud</param>
        /// <param name="popupHorizontalOffset">The horizontal offset (distance) of the message</param>
        /// <param name="popupVerticalOffset">The vertical offset (distance) of the message</param>
        /// <param name="larghezza">The Width of the coud of cartoon -Height is automatically set</param>
        /// <param name="layout">The layout (Grid) in which the cloud must be positioned on the screen</param>
        /// <param name="numeroTutorial">The number of this tutorial-message</param>
        /// <param name="numeroVisibile">Explains if the number of this tutorial-message</param>
        public TutorialMessage(string testo, double popupHorizontalOffset, double popupVerticalOffset, double larghezza, Grid layout, int numeroTutorial, bool numeroVisibile = true)
        {
            this.Messaggio = new TextBlock { Text = testo, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(28, 5, 5, 5), FontSize = 20 };
            this.Numero = new TextBlock { Text = numeroTutorial.ToString(), Margin = new Thickness(7, -2, 0, 0), FontSize = 18 };
            this.PopupHorizontalOffset = popupHorizontalOffset;
            this.PopupVerticalOffset = popupVerticalOffset;
            this.Larghezza = larghezza;
            this.Layout = layout;
            this.NumeroVisibile = numeroVisibile;
            this.ControlloPuntato = null;
        }

        /// <summary>
        /// It creates the graphical TutorialMessage. Called automatically in Show()
        /// </summary>
        private void CreaCorpo()
        {
            popup = new System.Windows.Controls.Primitives.Popup();
            
            //Image of the green clout with message (=tutorial)
            Image img = new Image();
            img.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Immagini/verde_centro.png", UriKind.Relative));
            img.Width = this.Larghezza;

            Border borderTxt = new Border();
            borderTxt.Child = this.Messaggio;
            borderTxt.Background = new SolidColorBrush(Colors.Transparent);

            //number of tutorial on the top-right side
            Image imgNumero = null;
            Border borderNum = null;
            if (this.NumeroVisibile)
            {
                imgNumero = new Image();
                imgNumero.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Immagini/numero.png", UriKind.Relative));
                imgNumero.Width = imgNumero.Height = 23;
                imgNumero.VerticalAlignment = VerticalAlignment.Top;
                imgNumero.HorizontalAlignment = HorizontalAlignment.Left;

                borderNum = new Border();
                borderNum.Child = this.Numero;
                borderNum.Background = new SolidColorBrush(Colors.Transparent);
            }

            //joining images and texts
            Grid grid = new Grid();
            grid.Width = this.Larghezza;
            grid.Children.Add(img);
            grid.Children.Add(borderTxt);
            if (this.NumeroVisibile)
            {
                grid.Children.Add(imgNumero);
                grid.Children.Add(borderNum);
            }
             
            //positioning popup if Control PointedContorl is set
            if (this.ControlloPuntato != null)
            {
                GeneralTransform y = this.ControlloPuntato.TransformToVisual(this.Layout);
                Point absolutePoint = y.Transform(new Point(0, 0));
                popup.HorizontalOffset = absolutePoint.X + this.Larghezza / 2;
                popup.VerticalOffset = absolutePoint.Y - img.ActualHeight;
            }
            else if (NIcone != 0) //positioning popup referred to ApplicationBarIconButton
            {
                System.Diagnostics.Debug.WriteLine("positioning popup");
                double position;
                if (NIcone == 0 || IndexIcona == 0)
                {
                    TutorialMessageException tme = new TutorialMessageException();
                    tme.Info = "TutorialMessage.NIcone and TutorialMessage.IndexIcona can not be 0";
                    throw tme;
                }
                else if (NIcone == 1)
                    position = Application.Current.Host.Content.ActualWidth / 2;
                else //NIcone > 1
                    position = ((double)((Application.Current.Host.Content.ActualWidth - (this.SizeIcona * this.NIcone) - (40 * Application.Current.Host.Content.ActualWidth / 480)) / 2) * Application.Current.Host.Content.ActualWidth / 480 + this.SizeIcona * (this.IndexIcona - 1) + this.SizeIcona / 2 + (((40 * Application.Current.Host.Content.ActualWidth) / 480) * (this.IndexIcona - 1)) - (this.Larghezza / 2));//this formula is only for WP7.x devices(Application.Current.Host.Content.ActualWidth - this.SizeIcona * this.NIcone - 40 * (this.NIcone - 1)) / 2 + this.SizeIcona * (this.IndexIcona - 1) + this.SizeIcona / 2 + 40 * (this.IndexIcona - 1) - this.Larghezza / 2;//Application.Current.Host.Content.ActualWidth / 4.8 * ((4.8 - 0.5 * NIcone - 0.4 * (NIcone - 1)) / 2 + 0.25 + 0.5 * (IndexIcona - 2) + 0.4 * (IndexIcona - 2));
                popup.HorizontalOffset = position;
                popup.VerticalOffset = Application.Current.Host.Content.ActualHeight - 70 * Application.Current.Host.Content.ActualHeight / 800 - ((double)215/398 * this.Larghezza) - 35;//This works but can not be dimostrated Application.Current.Host.Content.ActualHeight - (0.7 / 8 * Application.Current.Host.Content.ActualHeight + 0.7402010050251256 * img.Width);
                System.Diagnostics.Debug.WriteLine("larghezza = " + this.Larghezza.ToString() + " ; altezza = " + ((double)398 / 215 * this.Larghezza).ToString());
                System.Diagnostics.Debug.WriteLine("Offsets popup: " + popup.HorizontalOffset.ToString() + " , " + popup.VerticalOffset.ToString());
            }
            else //positioning popup if explicit Vertical- and HorizontalOffset are set
            {
                popup.VerticalOffset = this.PopupVerticalOffset;
                popup.HorizontalOffset = this.PopupHorizontalOffset;
            }

            //showing popup
            popup.Child = grid;

        }

        /// <summary>
        /// (TODO) Gets the Grid of the Popup, base of TutorialMessage. The developer  can use it in order to custom the behavior of TutorialMessage when it is clicked, tapped, double-tapped, etc.
        /// Note that developers have to firstly define Popup and then custom its events!
        /// </summary>
        public Grid GetPopup()
        {
            return popup.Child as Grid;
        }

        /// <summary>
        /// You can get to know if the TutorialMessage is visible or not (not necessarely it is shown on the screen. See the constructor, Layout)
        /// </summary>
        /// <returns>True if the Popup of the TutorialMessage has been shown</returns>
        public bool TutorialMessage_isVisible()
        {
            if (popup != null) return popup.IsOpen;
            else return false;
        }

        /// <summary>
        /// Hides the TutorialMessage
        /// </summary>
        public void Hide()
        {
            if(popup != null)
                popup.IsOpen = false;
        }

        /// <summary>
        /// Shows the TutorialMessage. Needed after constructor!
        /// </summary>
        public void Show()
        {
            if (popup == null)
                this.CreaCorpo();
            popup.IsOpen = true;
            //This is why the Layout must be set in constructor!
            this.Layout.Children.Add(popup);
        }
    }
}


//this exception is launched when the message will not be totally show on the display
public class TutorialMessageException : Exception
{
    public TutorialMessageException() : base() { }

    public string Info { get; set; }
}

//