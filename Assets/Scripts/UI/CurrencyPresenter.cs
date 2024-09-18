namespace SampleGame
{
    public sealed class CurrencyPresenter
    {
        public ICurrencyView View => this.view;
        public CurrencyCell Cell => this.cell;
        
        private readonly CurrencyCell cell;
        private readonly ICurrencyView view;

        private int prevAmount;
        
        public CurrencyPresenter(CurrencyCell cell, ICurrencyView view)
        {
            this.cell = cell;
            this.view = view;

            this.prevAmount = this.cell.Amount;
            
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
            this.prevAmount = currency;
        }

        private void OnAmountAdded(int range)
        {
            int amount = this.cell.Amount;
            this.view.AddCurrency(amount, range);
            this.prevAmount = amount;
        }

        private void OnAmountSpent(int _)
        {
            int amount = this.cell.Amount;
            this.view.RemoveCurrency(amount.ToString());
            this.prevAmount = amount;
        }
    }
}