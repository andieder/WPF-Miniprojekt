using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

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
                if (_selectedGadget != null && _selectedGadget.Equals(value))
                {
                   return;
                }
                _selectedGadget = value;
            }
        }

        private Loan _selectedLoan;

        public Loan SelectedLoan
        {
            get { return _selectedLoan; }
            set
            {
                if (_selectedLoan != null && _selectedLoan.Equals(value))
                {
                    return;
                }
                _selectedLoan = value;
            }
        }

        private readonly LibraryAdminService _service = new LibraryAdminService("http://mge8.dev.ifs.hsr.ch");

        public ObservableCollection<Gadget> Gadgets { get; set; }

        public ObservableCollection<Loan> Loans { get; set; }

        public MainWindow()
        {
            Gadgets = new ObservableCollection<Gadget>();
            Loans =  new ObservableCollection<Loan>();

            DataContext = this;
            InitializeComponent();

            RefreshGadgetView();
            RefreshView();

            WindowLoader();
        }

        private void WindowLoader()
        {
            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Tick += new EventHandler(dispatcherTimer);
            dispatcher.Interval = new TimeSpan(0, 0, 3);
            dispatcher.Start();
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            Window addWindow = new AddGadget(_service, true, new Gadget(""));
            addWindow.Title = "Add new Gadget";
            addWindow.ShowDialog();
            RefreshGadgetView();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedGadget != null)
            {
                Window editWindow = new AddGadget(_service, false, SelectedGadget);
                editWindow.Title = "Edit Gadget";
                editWindow.ShowDialog();
            }

            RefreshGadgetView();
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {

            if (
                MessageBox.Show("Sind Sie sicher, dass Sie " + SelectedGadget.Name + " löschen wollen?", "Sind Sie sicher?" , MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                _service.DeleteGadget(SelectedGadget);
                RefreshGadgetView();
            }

        }

        public void RefreshGadgetView()
        {
            Gadgets.Clear();
            _service.GetAllGadgets().ForEach((g) => Gadgets.Add(g));
        }

        private void dispatcherTimer(object sender, EventArgs e)
        {
            RefreshView();
        }

        public void RefreshView()
        {   
            Loans.Clear();
            _service.GetAllLoans().ForEach((l) => Loans.Add(l));
        }

    }

}
