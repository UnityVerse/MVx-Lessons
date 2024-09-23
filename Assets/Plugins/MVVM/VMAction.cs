namespace SampleGame
{
    public delegate void VMAction<TView, in TViewModel>(
        TView view,
        TViewModel viewModel,
        IVMBinder<TView, TViewModel> binder
    );
}