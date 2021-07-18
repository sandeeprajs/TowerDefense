using UnityEngine;

public class TowerButton : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject towerObject;

    [SerializeField]
    private Sprite dragSprite;
    #endregion

    #region Unity Untilities
    public GameObject TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }
    #endregion
}
