using System.Windows;
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

        public Condition SelectedCondition { get; set; }

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
