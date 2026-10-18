using CleanKwat.Helpers;
using CleanKwat.ViewModels;
namespace CleanKwat.Views.Auth;
public partial class OnboardingPage : ContentPage { public OnboardingPage(){InitializeComponent();BindingContext=ServiceHelper.Get<OnboardingViewModel>();} }
