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

namespace Gadgeothek
{
    /// <summary>
    /// Interaction logic for EditGadget.xaml
    /// </summary>
    public partial class EditGadget : Window
    {
        private readonly LibraryAdminService _service;
        private Gadget _changeGadget;

        public EditGadget(LibraryAdminService service, Gadget changeGadget)
        {
            _service = service;
            _changeGadget = changeGadget;
            InitializeComponent();

        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Gadget editGadget = new Gadget();
            _service.UpdateGadget(editGadget);
            throw new NotImplementedException();
        }
    }
}
