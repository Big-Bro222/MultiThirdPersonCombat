

using System;

public interface ITickProvider
{
   public event Action OnTick;
   public float GetDeltaTime();
}
