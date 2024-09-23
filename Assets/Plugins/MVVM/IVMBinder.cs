namespace SampleGame
{
    public interface IVMBinder<in TView, out TViewModel>
    {
        TViewModel ViewModel { get; }
        
        void Synchronize();
        
        void Show(TView view);
        
        void Hide();
    }
}