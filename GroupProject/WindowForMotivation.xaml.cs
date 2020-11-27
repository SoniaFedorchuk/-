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
using System.Windows.Shapes;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for WindowForMotivation.xaml
    /// </summary>
    public partial class WindowForMotivation : Window
    {
        List<Quotes> quotes = new List<Quotes>();   
        public WindowForMotivation()
        {
            WindowStyle = WindowStyle.None;
            InitializeComponent();


            quotes.Add(new Quotes() { Author = "Ray Bradbury \"451 degrees Fahrenheit\"" , Quote = "There are crimes worse than burning books. For example - do not read them." });
            quotes.Add(new Quotes() { Author = "Alan Alexander Milne \"Winnie the Pooh and all - all - all\"" , Quote = "And what Rabbit thought about this, no one ever found out, because Rabbit was very educated." });
            quotes.Add(new Quotes() { Author = "Oscar Wilde's \"Portrait of Dorian Gray\"", Quote = "We do not tolerate people with the same shortcomings as we do." });
            quotes.Add(new Quotes() { Author = "Mikhail Bulgakov \"The Master and Margarita\"", Quote = "Never ask for anything! Never anything, and especially those who are stronger than you. They will offer and give everything by themselves!" });
            quotes.Add(new Quotes() { Author = "Henri Gidel \"Coco Chanel\" ", Quote = "I don't want to hear what you think of me. I don't think about you at all." });
            quotes.Add(new Quotes() { Author = "Daniel Pennack \"Like a Novel\"", Quote = "The paradox of reading: it distracts us from reality to fill reality with meaning." });
            var quote = quotes[new Random().Next(0, 5)];
            txt_author.Text = quote.Author;
            txt_qoute.Text = quote.Quote;
        }

        public class Quotes
        {
            public string Quote { get; set; }
            public string Author { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
