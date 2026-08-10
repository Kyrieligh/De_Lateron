using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovable // menghapus Monobehaviour yang mana hal ini akan membuatnya tidak bisa ditampilkan ke komponen Inspector Uity
{
    public void Move(Vector2 direction);
}
