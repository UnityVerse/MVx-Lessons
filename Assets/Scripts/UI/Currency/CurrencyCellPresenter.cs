namespace SampleGame
{
    public sealed class CurrencyCellPresenter
    {
        public CurrencyView View => this.view;
        public CurrencyCell Cell => this.cell;
        
        private readonly CurrencyCell cell;
        private readonly CurrencyView view;

        public CurrencyCellPresenter(CurrencyCell cell, CurrencyView view)
        {
            this.cell = cell;
            this.view = view;

            this.view.SetIcon(this.cell.Icon);
            this.view.SetupCurrency(this.cell.Amount.ToString());
            
            this.cell.OnAmountAdded += this.OnAmountAdded;
            this.cell.OnAmountSpent += this.OnAmountSpent;
            this.cell.OnAmountChanged += this.OnAmountChanged;
        }

        public void Dispose()
        {
            this.cell.OnAmountAdded -= this.OnAmountAdded;
            this.cell.OnAmountSpent -= this.OnAmountSpent;
            this.cell.OnAmountChanged -= this.OnAmountChanged;
        }

        private void OnAmountChanged(int currency)
        {
            this.view.ChangeCurrency(currency.ToString());
        }

        private void OnAmountAdded(int range)
        {
            int amount = this.cell.Amount;
            this.view.AddCurrency(amount - range, range);
        }

        private void OnAmountSpent(int _)
        {
            int amount = this.cell.Amount;
            this.view.RemoveCurrency(amount.ToString());
        }
    }
}