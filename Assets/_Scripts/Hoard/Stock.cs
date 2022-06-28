using System;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public event Action OnStockEmpty;
    public event Action<int> OnStockValueChanged;
    
    private uint stock;

    protected internal void Add(uint amount)
    {
        stock += amount;

        if (amount != 0)
            OnStockValueChanged?.Invoke((int)stock);
    }

    protected internal uint TryTake(uint amount)
    {
        if (stock < amount)
            amount = stock;
         
        stock -= amount;

        if (amount != 0)
            OnStockValueChanged?.Invoke((int)stock);

        if (stock == 0)
            OnStockEmpty?.Invoke();

        return amount;
    }
}
