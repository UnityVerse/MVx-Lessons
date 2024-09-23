using Zenject;

namespace SampleGame
{
    public static class VMExtensions
    {
        public static VMBindingInfo<TView, TViewModel> DeclareViewModel<TView, TViewModel>(this DiContainer container)
        {
            return new VMBindingInfo<TView, TViewModel>(container);
        }
    }
}