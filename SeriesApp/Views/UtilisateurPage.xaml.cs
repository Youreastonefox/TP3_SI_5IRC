using Microsoft.UI.Xaml.Controls;

using SeriesApp.ViewModels;

namespace SeriesApp.Views;

public sealed partial class UtilisateurPage : Page
{
    public UtilisateurViewModel ViewModel
    {
        get;
    }

    public UtilisateurPage()
    {
        ViewModel = App.GetService<UtilisateurViewModel>();
        InitializeComponent();
    }
}
