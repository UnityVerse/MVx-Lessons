using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class VMBindingInfo<TView, TViewModel>
    {
        private readonly DiContainer diContainer;
        private readonly List<IProperty<TViewModel, TView>> properties = new();

        private VMAction<TView, TViewModel> onShow;
        private VMAction<TView, TViewModel> onHide;
        private object[] args = Array.Empty<object>();

        public VMBindingInfo(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }
        
        public VMBindingInfo<TView, TViewModel> WithArguments(params object[] args)
        {
            this.args = args;
            return this;
        }

        public VMBindingInfo<TView, TViewModel> OnShow(VMAction<TView, TViewModel> onShow)
        {
            this.onShow = onShow;
            return this;
        }

        public VMBindingInfo<TView, TViewModel> OnHide(VMAction<TView, TViewModel> onHide)
        {
            this.onHide = onHide;
            return this;
        }

        public VMBindingInfo<TView, TViewModel> BindProperty<T>(Func<TViewModel, T> getter, Action<TView, T> setter)
        {
            this.properties.Add(new Property<T, TViewModel, TView>(getter, setter));
            return this;
        }

        public void AsSingle()
        {
            this.diContainer
                .Bind<IVMBinder<TView, TViewModel>>()
                .To<VMBinder<TView, TViewModel>>()
                .FromMethod(() => new VMBinder<TView, TViewModel>(
                    this.diContainer.Instantiate<TViewModel>(this.args),
                    this.properties,
                    this.onShow,
                    this.onHide
                ))
                .AsSingle()
                .NonLazy();
        }
    }
}