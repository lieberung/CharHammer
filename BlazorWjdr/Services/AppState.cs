using System;

namespace BlazorWjdr.Services
{
    public class AppState
    {
        public bool JeSuisDieu { get; private set; }

        public event Action? OnChange;

        public void UnCombattantEnEngageUnAutre()
        {
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void PretendreEtreUnDieu(string password)
        {
            JeSuisDieu = GenericService.DieuEstDAccord(password);
        }
    }
}