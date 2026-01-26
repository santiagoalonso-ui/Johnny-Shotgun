using UnityEngine;
using UnityEngine.UI;

public class ShellHUD : MonoBehaviour
{
    [SerializeField] Image[] bullets;   
    [SerializeField] int maxAmmo = 8;

    int currentAmmo;

    void Awake()
    {
        currentAmmo = maxAmmo;
        UpdateHUD();
    }

    public void ConsumeBullet()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        UpdateHUD();
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].enabled = i < currentAmmo;
        }
    }
}
