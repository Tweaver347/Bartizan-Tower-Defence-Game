// Define a tower interface
using UnityEngine;
/// <summary>
/// This interface defines a contract for a tower and the basic functions all towers should have
/// </summary>
public interface ITower
{
    bool Active { get; set; }
    int[] Location { get; set; }
    int Damage { get; set; }
    float AttackRange { get; set; }
    float AttackCooldown { get; set; }
    GameObject Target { get; set; }
    Transform FirePoint { get; set; }
    /// <summary>
    /// Attacks whatever is the closest enemy to the tower
    /// </summary>
    void Attack();
    /// <summary>
    /// Subtracts the amount of gold that the player has. If the player does not have enough gold the tower is not upgraded
    /// Increases the following attributes: Damage, AttackRange
    /// Decreases the following arrtibutes: AttackCooldown
    /// </summary>
    void Upgrade();
    /// <summary>
    /// Sells the Tower selected by the player
    /// Refunds gold to the player
    /// Destroys game object
    /// </summary>
    /// <returns> Returns the location of the tower sold</returns>
    int[] Sell();

}