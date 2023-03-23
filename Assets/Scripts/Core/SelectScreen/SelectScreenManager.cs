using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField]
    List<WeaponInfo> selectableWeapon = new List<WeaponInfo>();
    [SerializeField]
    Image weaponThumb;
    [SerializeField]
    TextMeshProUGUI weaponDesc;
    int weaponIndex = 0;

    [SerializeField]
    List<MapInfo> selectableMap = new List<MapInfo>();
    [SerializeField]
    RawImage mapThumb;
    int mapIndex = 0;
	private void Start()
	{
		SelectWeapon(0);
		SelectMap(0);
	}

	public void NextWeapon()
    {
        if (selectableWeapon.Count < 0)
            return;
        weaponIndex++;
        if (weaponIndex < 0 || weaponIndex >= selectableWeapon.Count)
        {
            weaponIndex = 0;
        }
        SelectWeapon(weaponIndex);
	}
    void SelectWeapon(int index)
    {
		weaponThumb.sprite = selectableWeapon[index].Image;
		weaponDesc.SetText(selectableWeapon[index].WeaponDesc);
	}

	public void NextMap()
	{
		if (selectableMap.Count < 0)
			return;
		mapIndex++;
		if (mapIndex >= selectableMap.Count)
		{
			mapIndex = 0;
		}
        SelectMap(mapIndex);
	}

    void SelectMap(int index)
    {
		mapThumb.texture = selectableMap[index].texture;
		mapThumb.uvRect = new Rect(0, 0, 8 / 1.2f * selectableMap[index].scale.x, 5.28f / 1.2f * selectableMap[index].scale.y);
	}

	public void PreviousMap()
	{
		if (selectableMap.Count < 0)
			return;
		mapIndex--;
		if (mapIndex < 0)
		{
			mapIndex = selectableMap.Count - 1;
		}
		SelectMap(mapIndex);
	}

    public void StartPlay()
    {
        SceneDataKeeper.Singleton.WeaponChoice = selectableWeapon[weaponIndex].ChoiceValue;
        SceneDataKeeper.Singleton.mapChoice = selectableMap[weaponIndex].texture;
        SceneDataKeeper.Singleton.mapScale = selectableMap[weaponIndex].scale;
		SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
		Resources.UnloadUnusedAssets();
	}

	[Serializable]
    class WeaponInfo
    {
        public Sprite Image;
        public string WeaponDesc;
        public int ChoiceValue;
    }

	[Serializable]
	class MapInfo
	{
		public Texture2D texture;
        public Vector2 scale;
	}
}
