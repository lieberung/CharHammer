namespace CharHammer.Services;

public class AppState
{
  public bool JeSuisDieu { get; private set; }
  public event Action? OnChange;

  private void NotifyStateChanged() => OnChange?.Invoke();

  public void UnCombattantEnEngageUnAutre()
  {
    NotifyStateChanged();
  }

  public void PretendreEtreUnDieu(string password)
  {
    JeSuisDieu = GenericService.DieuEstDAccord(password);
    NotifyStateChanged();
  }

  public void RenoncerAuStatutDivin()
  {
    JeSuisDieu = false;
    NotifyStateChanged();
  }
}