using FirmaDigitalSqLite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirmaDigitalSqLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleFirmaDigital : ContentPage
    {
        private DatabaseService databaseService;
        public DetalleFirmaDigital()
        {
            databaseService = new DatabaseService();
            InitializeComponent();
            List<FirmaDigital> firmas = databaseService.GetFirmas();
            firmasListView.ItemsSource = firmas;
        }
    }
}