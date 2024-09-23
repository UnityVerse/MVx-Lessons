using System;

namespace SampleGame
{
    internal sealed class Property<TValue, TViewModel, TView> : IProperty<TViewModel, TView>
    {
        private readonly Func<TViewModel, TValue> getter;
        private readonly Action<TView, TValue> setter;

        private object _currentState;

        public Property(Func<TViewModel, TValue> getter, Action<TView, TValue> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public void Apply(TViewModel viewModel, TView view)
        {
            TValue newState = this.getter.Invoke(viewModel);
            if (newState.Equals(_currentState))
            {
                return;
            }

            _currentState = newState;
            this.setter.Invoke(view, newState);
        }
    }
}