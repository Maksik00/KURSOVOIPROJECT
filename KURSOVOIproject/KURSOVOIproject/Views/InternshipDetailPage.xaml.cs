using System;
using Microsoft.Maui.Controls;
<<<<<<< HEAD
using KURSOVOIproject.Services;
=======
>>>>>>> 359d440 (InternshipADDING!)
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class InternshipDetailPage : ContentPage
    {
<<<<<<< HEAD
        private readonly IApplicationService _applicationService;
        private readonly InternshipDetailViewModel _viewModel;

        public InternshipDetailPage(
            IApplicationService applicationService,
            InternshipDetailViewModel viewModel)
        {
            InitializeComponent();
            _applicationService = applicationService;
=======
        private readonly InternshipDetailViewModel _viewModel;

        public InternshipDetailPage(InternshipDetailViewModel viewModel)
        {
            InitializeComponent();
>>>>>>> 359d440 (InternshipADDING!)
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
<<<<<<< HEAD
            // TODO: вызвать _viewModel.LoadDetailsAsync() (грузим детали стажировки по Id)
=======
            // TODO completed: Р·Р°РіСЂСѓР·РєР° РґРµС‚Р°Р»РµР№
            _viewModel.LoadDetailsCommand.Execute(null);
>>>>>>> 359d440 (InternshipADDING!)
        }

        private async void OnApplyClicked(object sender, EventArgs e)
        {
<<<<<<< HEAD
            // TODO: если пользователь ещё не откликался, вызвать 
            // await _viewModel.ApplyToInternshipAsync(); 
            // иначе вызвать 
            // await _viewModel.RevokeApplicationAsync();
=======
            // TODO completed: РѕС‚РєР»РёРє РёР»Рё РѕС‚Р·С‹РІ
            if (!_viewModel.IsApplied)
                await _viewModel.ApplyCommand.ExecuteAsync(null);
            else
                await _viewModel.RevokeCommand.ExecuteAsync(null);
>>>>>>> 359d440 (InternshipADDING!)
        }
    }
}
