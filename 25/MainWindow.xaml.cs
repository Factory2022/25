using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;



namespace _25
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Button[] tasten  = new Button[26];
        public int[] zustand    = new int[26];
        public Label[] mini   = new Label[26];
        public string[,] vorlagen = new string[26,50];   // 50 Verschiedene Vorlagen zum lösen :)
        public int bild = 0, maxBilder = 0;                             // welche Vorlöage wir verwendet.
        public SolidColorBrush rot      = new SolidColorBrush(Colors.Red);
        public SolidColorBrush schwarz  = new SolidColorBrush(Colors.Black);
        public int versuche=0;

        
        public Image[] feldEin = new Image[26];
        public Image[] feldAus = new Image[26];


        public bool imSpiel = false;
        public Random random = new Random();

        // public string name;
        public int gedrueckteTaste, vergleich;

        
        

        public MainWindow()
        {
            InitializeComponent();
            LabelZuweisen();
            for (int i = 1; i < 26; i++)
            {
                feldEin[i] = new Image { Source = new BitmapImage(new Uri("/rotLeuchtend.png", UriKind.Relative)) };
                feldAus[i] = new Image { Source = new BitmapImage(new Uri("/d-rot.png", UriKind.Relative)) };
            }
            ZustandSetzten();
            Laden();
            FelderAnzeigen();
            MiniAnischtZuweisen();
            MiniAnsicht();
            ZufallSetzen();
            versuchNr.Content = Convert.ToString(versuche) + " Spielzüge.";
        }

        public void LabelZuweisen()         // Alle Buttons in ein Label-Array einfügen
        {
            for (int i = 1; i < 26; i++)
            {
                tasten[i] = (Button)FindName($"btn_{Convert.ToString(i)}");
            }
        }

        public void MiniAnischtZuweisen()
        {
            for (int i = 1; i < 26; i++)
            {
                mini[i] = (Label)FindName($"klein{Convert.ToString(i)}");
            }
            
        }

        public void MiniAnsicht()
        {
            // MessageBox.Show(Convert.ToString(bild));
            for (int i = 1; i < 26; i++)
            {
                if (vorlagen[i,bild] =="x")
                {
                    mini[i].Background = rot;
                }   else mini[i].Background = schwarz;
            }

        }

        public void Vergleich()
        {
            vergleich = 0 ; 
            for (int i = 1; i < 26; i++)
            {
                if (vorlagen[i, bild] == "x" && zustand[i] == 1) vergleich++;
                if (vorlagen[i, bild] != "x" && zustand[i] == -1) vergleich++;
            }
            if (vergleich == 25)
            {
                MessageBox.Show("Gewonnen mit "+ Convert.ToString(versuche) + " Spielzügen!");
                ZufallSetzen();
                ZustandSetzten();
                FelderAnzeigen();
            }
        }
        public void ZustandSetzten()
        {
            for (int i = 1; i < 26; i++) zustand[i] = -1;
        }
        public void FelderAnzeigen()                                    // Anzeige wied aktualisiert
        {
            for (int i = 1; i < 26; i++)
            {
                if (zustand[i] == 1) tasten[i].Content = feldEin[i];    //  new Image { Source = new BitmapImage(new Uri("/rotLeuchtend.png", UriKind.Relative)), };
                else tasten[i].Content = feldAus[i];                    // new Image { Source = new BitmapImage(new Uri("/d-rot.png", UriKind.Relative)), };
            }
        }

        public void ZufallSetzen()
        {
            versuche = 0;   // Zähler wieder auf 0!
            versuchNr.Content = Convert.ToString(versuche) + " Spielzüge.";

            bild = random.Next(0, maxBilder+1);
            MiniAnsicht();
            ZustandSetzten();
                       
        }

        private void Click_Leer(object sender, RoutedEventArgs e)  // Feld wieder leeren;
        {
            if (imSpiel == false)
            {
                ZustandSetzten();
                FelderAnzeigen();
            }
        }

        private void Click_Zufall(object sender, RoutedEventArgs e) 
        {
            if (imSpiel == false)
            {
                ZufallSetzen();
                FelderAnzeigen();
            }
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
            for (int i=1; i<26; i++)
            {
                if (sender == tasten[i]) gedrueckteTaste=i;
            }
            //MessageBox.Show("Taste " + Convert.ToString(gedrueckteTaste) + " wurde gedrückt!");
            versuche++;
            versuchNr.Content = Convert.ToString(versuche)+" Spielzüge.";
            Logik();
            Vergleich();
        }
       
        public void Logik()
        {
            switch (gedrueckteTaste)
            {
                case 1:
                    Ecke(1, 2, 6, 7);
                    break;


                case 2:
                    Dreier(1,2, 3);
                    break;
                case 3:
                    Dreier(2, 3, 4);
                    break;
                case 4:
                    Dreier(3, 4, 5);
                    break;

                case 7:
                    Dreier(6, 7, 8);
                    break;
                case 8:
                    Dreier(7, 8, 9);
                    break;
                case 9:
                    Dreier(8, 9, 10);
                    break;

                case 12:
                    Dreier(11, 12, 13);
                    break;
                //case 13:
                  //  Dreier(12, 13, 14);
                  //  break;
                case 14:
                    Dreier(13, 14, 15);
                    break;

                case 17:
                    Dreier(16, 17, 18);
                    break;
                case 18:
                    Dreier(17, 18, 19);
                    break;
                case 19:
                    Dreier(18, 19, 20);
                    break;

                case 22:
                    Dreier(21, 22, 23);
                    break;
                case 23:
                    Dreier(22, 23, 24);
                    break;
                case 24:
                    Dreier(23, 24, 25);
                    break;

                case 6:
                    Dreier(1, 6, 11);
                    break;
                case 11:
                    Dreier(6, 11, 16);
                    break;
                case 16:
                    Dreier(11, 16, 21);
                    break;

                case 10:
                    Dreier(5, 10, 15);
                    break;
                case 15:
                    Dreier(10, 15, 20);
                    break;
                case 20:
                    Dreier(15, 20, 25);
                    break;

                case 5:
                    Ecke(4, 5, 9, 10);
                    break;
                case 21:
                    Ecke(16, 17, 21, 22);
                    break;
                case 25:
                    Ecke(19, 20, 24, 25);
                    break;

                case 13:
                    Fuenf(8, 12, 13, 14, 18);
                    break;

                default:
                    break;

            }
        }

        public void Umschalten(int wert)
        {
            zustand[wert] = zustand[wert] * -1;
            if (zustand[wert] == 1) tasten[wert].Content = feldEin[wert]; // new Image { Source = new BitmapImage(new Uri("/rotLeuchtend.png", UriKind.Relative)), };
            else tasten[wert].Content = feldAus[wert];// new Image { Source = new BitmapImage(new Uri("/d-rot.png"  , UriKind.Relative)), };
        }
        public void Ecke(int taste1, int taste2, int taste3, int taste4)
        {
            Umschalten(taste1);
            Umschalten(taste2);
            Umschalten(taste3);
            Umschalten(taste4);
        }

        public void Dreier(int taste1, int taste2, int taste3)
        {
            Umschalten(taste1);
            Umschalten(taste2);
            Umschalten(taste3);
        }

        

        public void Fuenf(int taste1, int taste2, int taste3, int taste4, int taste5)
        {
            Umschalten(taste1);
            Umschalten(taste2);
            Umschalten(taste3);
            Umschalten(taste4);
            Umschalten(taste5);
        }
      
        
        
        
        public void Laden()
        {
            StreamReader sr = new StreamReader("bilder.txt");

            if (File.Exists("bilder.txt") == true)
            {
                int j = 0;
                while (true)
                {
                    
                    String zeile = sr.ReadLine();

                    if (zeile == "ENDE")
                    {
                        bild--;
                        maxBilder = bild;
                        break;
                    }

                    zeile = sr.ReadLine();
                   // MessageBox.Show(zeile + Convert.ToString(j));
                    for (int i = 1; i < 26; i++)
                    {
                        vorlagen[i, j] = Convert.ToString(zeile[i-1]);
                       // MessageBox.Show(Convert.ToString(zeile[i - 1]));
                    }
                    j++;
                    bild++;
                    //zeile = sr.ReadLine();
                   // if (zeile != "ENDE") j++;
                    //else break;
                }
            }
            sr.Close();
        }

        
    }
}
