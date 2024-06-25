namespace ProductManagementSystem.Frontend.Services
{
    public interface ILoadingService
    {
        public event Action OnChange;
        public void Show();
        public void Hide();
        public bool IsLoading { get;  }
    }

    public class LoadingService : ILoadingService
    {
        public event Action OnChange;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    NotifyStateChanged();
                }
            }
        }

        public void Show()
        {
            IsLoading = true;
        }

        public void Hide()
        {
            IsLoading = false;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
