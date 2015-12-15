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
                if (_selectedCondition.Equals(value))
                {
                    return;
                }
                _selectedCondition = value;
                //this.OnPropertyChanged(c => c.SelectedCondition);
            }
        }

    /*    public List<Condition> Conditions { get; set; }

        private void SetConditions()
        {
            var values = Enum.GetValues((typeof(Condition)));

            Conditions = new List<Condition>();
            foreach (Condition value in values)
            {
                Conditions.Add(value);
            }
        }*/


        public AddGadget (LibraryAdminService service)
        {
            _service = service;
            InitializeComponent();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {

            Gadget newGadget = new Gadget(AddName.Text)
            {
                Name = AddName.Text,
                Manufacturer = Manufacturer.Text,
                Price = Double.Parse(Price.Text),
                Condition = SelectedCondition
            };
            _service.AddGadget(newGadget);
            this.Close();
        }
    }
}
