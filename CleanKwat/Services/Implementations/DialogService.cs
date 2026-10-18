using CleanKwat.Services.Interfaces;

namespace CleanKwat.Services.Implementations;

public class DialogService : IDialogService
{
    private static Page? Page => Application.Current?.Windows.FirstOrDefault()?.Page;
    public Task AlertAsync(string title, string message, string cancel = "OK") => Page?.DisplayAlert(title, message, cancel) ?? Task.CompletedTask;
    public Task<string?> PromptAsync(string title, string message, string accept = "Valider", string cancel = "Annuler", string? placeholder = null)
        => Page?.DisplayPromptAsync(title, message, accept, cancel, placeholder) ?? Task.FromResult<string?>(null);
    public Task<bool> ConfirmAsync(string title, string message, string accept = "Oui", string cancel = "Non")
        => Page?.DisplayAlert(title, message, accept, cancel) ?? Task.FromResult(false);
}
