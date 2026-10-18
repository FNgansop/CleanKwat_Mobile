namespace CleanKwat.Services.Interfaces;
public interface IDialogService
{
    Task AlertAsync(string title, string message, string cancel = "OK");
    Task<string?> PromptAsync(string title, string message, string accept = "Valider", string cancel = "Annuler", string? placeholder = null);
    Task<bool> ConfirmAsync(string title, string message, string accept = "Oui", string cancel = "Non");
}
