using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace assembler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // SETTING UP DEFAULTS THEN RUNNING MAIN FUNCTIONS
            display.IsReadOnly = true;
            startingUp();
        }
        Rejestr rejestr = new Rejestr();


        public void startingUp()
        {

            display.Text = rejestr.hello();
            display.AppendText(rejestr.showRegistry());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // this button sets the defaults for registry
            // It checks out if your registry entries are legit (16 bits) if not throws and error for specific input 
            // If everything goes ok the registry is shown
            string[] inputs = { inputAX.Text, inputBX.Text, inputCX.Text, inputDX.Text };
            try
            {
                rejestr.AX = inputs[0];
                rejestr.BX = inputs[1];
                rejestr.CX = inputs[2];
                rejestr.DX = inputs[3];
            }
            catch (Exception a)
            {
                display.AppendText("\n" + a.ToString());

            }
            display.AppendText(rejestr.showRegistry());

        }

        private void Mov_Clicked(object sender, RoutedEventArgs e)
        {
            // get input from that box and split it into an array by thy ' , ' sign
            string[] input = inputMov.Text.Split(',');
            //case if they are empty
            //MessageBox.Show(input[0].Length.ToString()); 
            if(input[0].Length > 0 && input[1].Length > 0)
            {
                Mov(input[0], input[1]);
            }
            else
            {
                display.AppendText("\n Wartość instrukcji nie może być pusta");
            }

          
            display.AppendText(rejestr.showRegistry());
        }
        private void Dec_Clicked(object sender, RoutedEventArgs e)
        {
            string input = inputDec.Text;
            if(input.Length  < 1)
            {
                display.AppendText("\n Proszę podać rejestr do dekrementacji");
                //display.AppendText(input.Length.ToString());
                display.AppendText(rejestr.showRegistry());
            }
         
            else
            {
                Dec(input);
                display.AppendText(rejestr.showRegistry());
            }
        }


        public void Mov( string where, string what)
        {
          

            if (what == "AX"|| what == "BX" || what == "CX" || what == "DX")
            {
                //get dynamic value of second parameter
                string valueWhat = rejestr.GetType().GetProperty(what).GetValue(rejestr, null).ToString();
                //put it into 1st one 
                rejestr.GetType().GetProperty(where).SetValue(rejestr, valueWhat, null);


            }
            else
            {
                rejestr.GetType().GetProperty(where).SetValue(rejestr, what, null);
            }
        }
        public void Dec(string what)
        {
            try
            {
                string value = rejestr.GetType().GetProperty(what).GetValue(rejestr, null).ToString();
                int valueINT = int.Parse(value)-1;
                rejestr.GetType().GetProperty(what).SetValue(rejestr,valueINT.ToString() , null);
           

            }
            catch (FormatException g)
            {
                display.AppendText("\n"+ g.ToString());
               
            }
        }

       
    }

    public class Rejestr
    {
        //creating variables
        public string AX { get; set; }
        public string BX { get; set; }
        public string CX { get; set; }
        public string DX { get; set; }

        public string hello()
        {
            return "Witam! Nazywam się Kuba Janiec i zaprezentuję Państwu tym programem symulację poleceń MOV oraz ADD na przykładzie procesora INTEL 8086... \n ";
        }
        public string showRegistry()
        {
            return ("\nAX: " + this.AX + "  BX: " + this.BX + "  CX: " + this.CX + "  DX: " + this.DX);
        }

       

    }
}

