namespace CleanKwat.Controls;

public partial class PageHeader : ContentView
{
    public static readonly BindableProperty EyebrowProperty = BindableProperty.Create(nameof(Eyebrow), typeof(string), typeof(PageHeader), string.Empty);
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(PageHeader), string.Empty);
    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(PageHeader), string.Empty, propertyChanged:(b,o,n)=>((PageHeader)b).OnPropertyChanged(nameof(HasSubtitle)));
    public static readonly BindableProperty ShowBackProperty = BindableProperty.Create(nameof(ShowBack), typeof(bool), typeof(PageHeader), true);
    public string Eyebrow { get=> (string)GetValue(EyebrowProperty); set=>SetValue(EyebrowProperty,value); }
    public string Title { get=> (string)GetValue(TitleProperty); set=>SetValue(TitleProperty,value); }
    public string Subtitle { get=> (string)GetValue(SubtitleProperty); set=>SetValue(SubtitleProperty,value); }
    public bool ShowBack { get=> (bool)GetValue(ShowBackProperty); set=>SetValue(ShowBackProperty,value); }
    public bool HasSubtitle => !string.IsNullOrWhiteSpace(Subtitle);
    public PageHeader() => InitializeComponent();
    private async void OnBackClicked(object? sender, EventArgs e) => await Shell.Current.GoToAsync("..");
}
