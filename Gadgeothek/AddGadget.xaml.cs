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
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using Condition = ch.hsr.wpf.gadgeothek.domain.Condition;
using System.Collections.ObjectModel;

namespace Gadgeothek
{
    /// <summary>
    /// Interaction logic for AddGadget.xaml
    /// </summary>
    public partial class AddGadget : Window
    {
        private readonly LibraryAdminService _service;

        private Condition _selectedCondition;

        public Condition SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                if (_selectedCondition == value)
                {
                    return;
                }
                _selectedCondition = value;
            }
        }

        public Gadget EditGadget { get; set; }

            public bool IsNew { get; set; }

        public AddGadget(LibraryAdminService service, bool isNew, Gadget editGadget)
        {

            _service = service;
            EditGadget = editGadget;
            IsNew = isNew;

            InitializeComponent();
            DataContext = EditGadget;

        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                _service.AddGadget(EditGadget);
            }
            else
            {
                _service.UpdateGadget(EditGadget);
            }
            
            this.Close();
        }
    }
}
