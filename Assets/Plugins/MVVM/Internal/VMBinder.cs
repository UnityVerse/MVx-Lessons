using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SampleGame
{
    internal sealed class VMBinder<TView, TViewModel> : IVMBinder<TView, TViewModel>
    {
        [ShowInInspector]
        public TViewModel ViewModel => this.value;

        private readonly TViewModel value;
        private readonly List<IProperty<TViewModel, TView>> properties;
        private readonly VMAction<TView, TViewModel> onShow;
        private readonly VMAction<TView, TViewModel> onHide;

        private TView _view;

        public VMBinder(
            TViewModel value,
            List<IProperty<TViewModel, TView>> properties,
            VMAction<TView, TViewModel> onShow,
            VMAction<TView, TViewModel> onHide
        )
        {
            this.value = value;
            this.properties = properties;
            this.onShow = onShow;
            this.onHide = onHide;
        }

        public void Synchronize()
        {
            if (_view == null)
            {
                return;
            }

            foreach (var property in this.properties)
            {
                property.Apply(this.value, _view);
            }
        }

        public void Show(TView view)
        {
            _view = view;
            this.onShow?.Invoke(view, this.value, this);

            foreach (var property in this.properties)
            {
                property.Apply(this.value, _view);
            }
        }

        public void Hide()
        {
            this.onHide?.Invoke(_view, this.value, this);
            _view = default;
        }
    }
}