using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoCountUI : MonoBehaviour
{
    public Image UIBulletPrefab;
    public Sprite fullBulletSprite;
    public Sprite emptyBulletSprite;
    private List<Image> bullets = new List<Image>();

    // Method to set the max number of bullets in the UI
    public void SetMaxBullets(int maxBullets)
    {
        foreach(Image bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

        bullets.Clear();

        for(int i = 0; i < maxBullets; i++)
        {
            Image newBullet = Instantiate(UIBulletPrefab, transform);
            newBullet.sprite = fullBulletSprite;
            bullets.Add(newBullet);
        }
    }

    // Method to update the displayed bullets based on current count
    public void UpdateBullets(int currentBullet)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if(i < currentBullet)
            {
                bullets[i].sprite = fullBulletSprite;
            }
            else
            {
                bullets[i].sprite = emptyBulletSprite;
            }
        }
    }
}