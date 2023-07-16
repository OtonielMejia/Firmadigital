using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Xamarin.Forms;
using System.IO;
using System;
using System.Collections.Generic;

using FirmaDigitalSqLite.models;
using SQLite;
using SignaturePad.Forms;

namespace FirmaDigitalSqLite
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService databaseService;

        public MainPage()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
         
        }

        private async void GuardarFirmaClicked(object sender, EventArgs e)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                status = result[Permission.Storage];
            }

            if (status == PermissionStatus.Granted)
            {
                if (string.IsNullOrWhiteSpace(nombreEntry.Text) || string.IsNullOrWhiteSpace(descripcionEntry.Text))
                {
                    await DisplayAlert("Advertencia", "Por favor, completa todos los campos", "OK");
                    return;
                }

                if (signaturePadView.IsBlank)
                {
                    await DisplayAlert("Advertencia", "Por favor, captura una firma", "OK");
                    return;
                }

                // Obtener la imagen de la firma como flujo de bytes
                byte[] firmaBytes;
                using (MemoryStream stream = new MemoryStream())
                {
                    var signatureStream = await signaturePadView.GetImageStreamAsync(SignatureImageFormat.Png);
                    await signatureStream.CopyToAsync(stream);
                    firmaBytes = stream.ToArray();
                }

                FirmaDigital firma = new FirmaDigital
                {
                    Nombre = nombreEntry.Text,
                    Descripcion = descripcionEntry.Text,
                    FirmaImagen = firmaBytes
                };

                databaseService.InsertFirma(firma);
                LimpiarCampos();
            }
        }




        private void LimpiarCampos()
        {
            nombreEntry.Text = string.Empty;
            descripcionEntry.Text = string.Empty;
            signaturePadView.Clear();
        }

        private void VerFirmasClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetalleFirmaDigital());
        }


    }
}
