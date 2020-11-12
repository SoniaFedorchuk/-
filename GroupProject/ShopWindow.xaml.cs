﻿using BLL;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        private IBLLClass _bll = null;
        UserDTO user = null;

        public ShopWindow(IBLLClass _bll, UserDTO user)
        {
            InitializeComponent();

            this._bll = _bll;
            this.user = user;

            dataGrid.ItemsSource = _bll.GetAllBooks(); 
        }

        private void btnHints_Checked(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }
        public class Books
        {
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged();
                }
            }

            private string author;
            public string Author
            {
                get { return author; }
                set
                {
                    author = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _bll.GetAllBooks().Where(b => b.Name.Contains(txt_Search.Text));
            //if (_bll.GetBookByName(txt_Search.Text) != null)
            //{
            //    var bookByName = _bll.GetBookByName(txt_Search.Text);
            //    ObservableCollection<Books> book = new ObservableCollection<Books>();
            //    book.Add(new Books { Author = bookByName.Author, Name = bookByName.Name });
            //    dataGrid.ItemsSource = book;
            //}
            //else if (txt_Search.Text == "")
            //    dataGrid.ItemsSource = books;
        }

        private void btnTakeMotivation_Click(object sender, RoutedEventArgs e)
        {
            WindowForMotivation window = new WindowForMotivation();
            window.Show();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BuyBookWindow buyBookWindow = new BuyBookWindow(dataGrid.SelectedItem as BookDTO,_bll);
            buyBookWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BuyBookWindow buyBookWindow = new BuyBookWindow(dataGrid.SelectedItem as BookDTO,_bll);
            buyBookWindow.Show();
        }

        private void Chat(object sender, RoutedEventArgs e)
        {
            new ChatWindow(_bll, user).Show();
        }
    }
}

