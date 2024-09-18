using UnityEngine;

namespace SampleGame
{
    public interface ICurrencyView
    {
        void SetIcon(Sprite icon);
        void SetupCurrency(string currency);
        void ChangeCurrency(string currency);
        void RemoveCurrency(string currency);
        void AddCurrency(int newvalue, int range);
    }
}