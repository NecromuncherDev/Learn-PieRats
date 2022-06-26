using System;
using UnityEngine;

public class Hoarder : MonoBehaviour
{
    public event Action OnHoardEmpty;
    public event Action<int> OnHoardValueChanged;


    private int hoard;

    public int Hoard
    {
        get => hoard;
        set
        {
            if (hoard != value)
            {
                if (value <= 0)
                {
                    if (hoard > 0)
                    {
                        OnHoardEmpty?.Invoke();
                    }

                    hoard = 0;
                }
                else
                {
                    hoard = value;
                }

                OnHoardValueChanged?.Invoke(hoard);
            }
        }
    }

    public int TryTake(int amount)
    {
        if (Hoard < amount)
            amount = Hoard;
         
        Hoard -= amount;
        return amount;
    }
}
