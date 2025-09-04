using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class CreateInternshipPage : ContentPage
    {
        public CreateInternshipPage(CreateInternshipViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }
}
