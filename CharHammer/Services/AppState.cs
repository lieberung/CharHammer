namespace CharHammer.Services;

public class AppState
{
    public string MainTopRowTitle { get; private set; } = "Charente Hammer";
    public string PageHeadTitle { get; private set; } = "Charente Hammer";
    public bool JeSuisDieu { get; private set; }
    public event Action? OnChange;
    
    private void NotifyStateChanged() => OnChange?.Invoke();

    public void SetPageHeadTitle(string title)
    {
        PageHeadTitle = title;
        NotifyStateChanged();
    }

    public void SetMainTopRowTitle(string title)
    {
        MainTopRowTitle = title;
        NotifyStateChanged();
    }

    public void UnCombattantEnEngageUnAutre()
    {
        NotifyStateChanged();
    }

    public void PretendreEtreUnDieu(string password)
    {
        JeSuisDieu = GenericService.DieuEstDAccord(password);
        NotifyStateChanged();
    }
}