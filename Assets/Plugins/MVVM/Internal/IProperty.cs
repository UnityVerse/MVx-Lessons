namespace SampleGame
{
    internal interface IProperty<in TViewModel, in TView>
    {
        void Apply(TViewModel viewModel, TView view);
    }
}