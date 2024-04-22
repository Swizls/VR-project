using System;

public interface IMovable 
{
	public event Action<bool> MovingStateChanged;
}