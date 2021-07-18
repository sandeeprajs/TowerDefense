using UnityEngine;

public enum ProType
{
    arrow, rock, fire
}

public class Projectiles : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int attackStrength;

    [SerializeField]
    private ProType projectileType;
    #endregion

    #region Unity Utilites
    public int AttackStrength
    {
        get
        {
            return attackStrength;
        }
    }

    public ProType ProjectileType
    {
        get
        {
            return projectileType;
        }
    }
    #endregion
}
