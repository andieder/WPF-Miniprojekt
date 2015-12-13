using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Gadgeothek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Gadget> Gadgets { get; set; }
        public ObservableCollection<Customer> Customers { get; set; } 
        public ObservableCollection<Reservation> Reservations { get; set; } 
        public ObservableCollection<Loan> Loans { get; set; } 
        public MainWindow()
        {
            Gadgets = new ObservableCollection<Gadget>();
            Customers = new ObservableCollection<Customer>();
            Reservations = new ObservableCollection<Reservation>();
            Loans =  new ObservableCollection<Loan>();

            DataContext = this;
            InitializeComponent();

            var service = new LibraryAdminService("http://mge1.dev.ifs.hsr.ch");

            service.GetAllGadgets().ForEach((g) => Gadgets.Add(g));
            service.GetAllCustomers().ForEach((c) => Customers.Add(c));
            service.GetAllReservations().ForEach((r) => Reservations.Add(r));
            service.GetAllLoans().ForEach((l) => Loans.Add(l));
        }
    }
}
