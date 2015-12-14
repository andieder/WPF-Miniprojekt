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

        private Gadget _selectedGadget;

        public Gadget SelectedGadget
        {
            get { return _selectedGadget; }
            set
            {
                if (_selectedGadget == value)
                {
                    return;
                }
                _selectedGadget = value;
                //this.OnPropertyChanged(p => p.SelectedGadget);
            }
        }

        private readonly LibraryAdminService _service = new LibraryAdminService("http://mge1.dev.ifs.hsr.ch");

        public ObservableCollection<Gadget> Gadgets { get; set; }
        public ObservableCollection<Customer> Customers { get; set; } 
        public ObservableCollection<Reservation> Reservations { get; set; } 
        public ObservableCollection<Loan> Loans { get; set; }
        public ObservableCollection<Loan> GadgetsLoans { get; set; }
        public MainWindow()
        {
            Gadgets = new ObservableCollection<Gadget>();
            Customers = new ObservableCollection<Customer>();
            Reservations = new ObservableCollection<Reservation>();
            Loans =  new ObservableCollection<Loan>();
            GadgetsLoans = new ObservableCollection<Loan>();

            DataContext = this;
            InitializeComponent();

            

            _service.GetAllGadgets().ForEach((g) => Gadgets.Add(g));
            _service.GetAllCustomers().ForEach((c) => Customers.Add(c));
            _service.GetAllReservations().ForEach((r) => Reservations.Add(r));
            _service.GetAllLoans().ForEach((l) => Loans.Add(l));
/*            _service.GetAllLoans().ForEach((gl) =>
            {
                if (gl.Gadget != null)
                {
                    GadgetsLoans.Add(gl);
                }
                
            });*/

/*            private void Action(Loan gl)
            {
                var query = from gl in Loans
                            where gl.GadgetId != null
                            select gl;
                foreach (var gls in query)
                {
                    GadgetsLoans.Add(gls);
                }
            }*/



            /*            var freeFromQuery = from loan in Loans
                                            join gadget in Gadgets on loan.GadgetId equals gadget.InventoryNumber
                                            select loan.ReturnDate;

                        foreach (var free in freeFromQuery)
                        {
                            FreeFrom.Add(free);
                        }

                        var loanFromQuery = from loan in Loans
                                            join customer in Customers on loan.CustomerId equals customer.Studentnumber
                                            select customer.Name;*/
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            Window addWindow = new AddGadget(_service);
            addWindow.Show();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Window editWindow = new EditGadget();
            editWindow.Show();
            Gadgets.Clear();
            _service.GetAllGadgets().ForEach((g) => Gadgets.Add(g));
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            _service.DeleteGadget(SelectedGadget);

            Gadgets.Clear();
            _service.GetAllGadgets().ForEach((g) => Gadgets.Add(g));
        }
    }
}
