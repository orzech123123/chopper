using UnityEngine;

public class Layers {
    public static LayerMask PlayerAmmunition = LayerMask.NameToLayer(nameof(PlayerAmmunition));
    public static LayerMask EnemyAmmunition = LayerMask.NameToLayer(nameof(EnemyAmmunition));
    public static LayerMask Enemy = LayerMask.NameToLayer(nameof(Enemy));
    public static LayerMask Player = LayerMask.NameToLayer(nameof(Player));
}
