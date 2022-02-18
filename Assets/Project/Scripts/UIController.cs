using Project.Classes;
using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class UIController: MonoBehaviour
    {
        [SerializeField]
        private TMP_Text ammoText;

        [SerializeField] 
        private WeaponController weaponController;

        private Weapon weapon => weaponController.Weapon;

        private void Update()
        {
            ammoText.text = $"{weapon.CurrentAmmo} / {weapon.AmountOfBullets - weapon.CurrentAmmo}";
        }
    }
}