using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class TowerManager : Singleton<TowerManager>
{
    #region Variables
	public TowerButton towerButtonPressed;

    private SpriteRenderer spriteRenderer;
    #endregion

    #region Main Methods
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if(hit.collider.tag == "BuildSite")
            {
                hit.collider.tag = "BuildSiteFull";
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }

    }
    #endregion

    #region Unity Utility
    public void PlaceTower (RaycastHit2D hit)
    {
        if(!EventSystem.current.IsPointerOverGameObject() && towerButtonPressed != null)
        {
            GameObject newTower = Instantiate(towerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            disableDragSprite();
        }
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
        enableDragSprite(towerButtonPressed.DragSprite);
    }

    public void FollowMouse ()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x - 0.175f, transform.position.y);
    }

    public void enableDragSprite (Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void disableDragSprite ()
    {
        spriteRenderer.enabled = false;
        towerButtonPressed = null;
    }
    #endregion
}
