using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public event Action<ISpawnable> OnSpawned;
    public event Action<ISpawnable> OnDestroyed;
}
