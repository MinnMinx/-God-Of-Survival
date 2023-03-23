using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private Core.PlayerController player;
	[SerializeField]
    private VuKhi.Base gun, spellbook, sword;
	private VuKhi.Base _activeWeapon;
	[SerializeField]
	private Image weaponLevelProgressImg;
	[SerializeField]
	private TextMeshProUGUI weaponLevelTxt;

	[SerializeField]
	private GameObject levelUpItemPrefab;

	private void Start()
	{
		Monster.Monster.SpecialDropAction = SpawnLevelUpItem;
		ChooseWeapon(2);
		_activeWeapon.Init();
		UpdateWeaponUI();
	}

	void ChooseWeapon(int choice)
	{
		switch(choice)
		{
			default:
				sword.gameObject.SetActive(true);
				spellbook.gameObject.SetActive(false);
				gun.gameObject.SetActive(false);
				_activeWeapon = sword;
				break;
			case 1:
				sword.gameObject.SetActive(false);
				spellbook.gameObject.SetActive(true);
				gun.gameObject.SetActive(false);
				_activeWeapon = spellbook;
				break;
			case 2:
				sword.gameObject.SetActive(false);
				spellbook.gameObject.SetActive(false);
				gun.gameObject.SetActive(true);
				_activeWeapon = gun;
				break;
		}
	}

	void SpawnLevelUpItem(Vector3 position)
	{
		var gameObj = Instantiate(levelUpItemPrefab, position, Quaternion.identity);
		if (gameObj.GetComponent<MainWeaponItem>() != null)
		{
			gameObj.GetComponent<MainWeaponItem>().SetPickUp(LevelUpWeapon);
		}
	}

	void LevelUpWeapon()
	{
		if (!_activeWeapon.IsMaxLevel())
		{
			_activeWeapon.GetExp();
			UpdateWeaponUI();
		}
		else
		{
			player.Heal(50);
			player.HealShield(30);
		}
	}

	void UpdateWeaponUI()
	{
		weaponLevelProgressImg.fillAmount = _activeWeapon.CurrentExp;
		weaponLevelTxt.text = string.Format("Lv. {0}", _activeWeapon.Level);
	}
}
