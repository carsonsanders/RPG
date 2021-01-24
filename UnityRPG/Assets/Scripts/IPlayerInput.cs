using System;

public interface IPlayerInput
{
    event Action<int> HotkeyPressed;
    float Vertical { get; }
    float Horizontal { get; }
    
    float MouseX { get; }
    
    void Tick();
}