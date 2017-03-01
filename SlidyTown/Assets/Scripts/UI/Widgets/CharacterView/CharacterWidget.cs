using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterWidget : MonoBehaviour, IDeselectHandler {

    public WorldData Data { get; private set; }

    public Transform Locked;
    public Transform Unlocked;
    public Transform Selected;
    public CharacterPriceWidget Price;
    public Transform Confirm;
    public Transform CommingSoon;

    private bool _Unlocked;

    void Awake () {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        WorldObjectProvider.OnWorldChanged += RefreshSelection;
	}


	
	void Update () {
		
	}

    public void SetWorldData(WorldData data) {
        Data = data;

        RefreshLock();
        RefreshSelection();

        if (Data != null) {
            Locked.GetComponent<Image>().sprite = Data.SilouetePreview;
            Unlocked.GetComponent<Image>().sprite = Data.ColoredPreview;           
            Price.SetPrice(Data.Price);          
            Confirm.gameObject.SetActive(false);
            CommingSoon.gameObject.SetActive(false);
        }
        else {
            Confirm.gameObject.SetActive(false);
            CommingSoon.gameObject.SetActive(true);
        }
    }

    private void RefreshSelection() {
        Selected.gameObject.SetActive(Data != null && WorldObjectProvider.CurrentWorld == Data.Name);
    }

    private void RefreshLock() {
        _Unlocked = Data != null && SaveManager.Data.UnlockedWorlds.Contains(Data.Name);
        Locked.gameObject.SetActive(!_Unlocked);
        Unlocked.gameObject.SetActive(_Unlocked);
        Price.gameObject.SetActive(!_Unlocked);
    }

    private void OnClick() {
        if(Data != null) {
            if (_Unlocked) {
                Selected.gameObject.SetActive(WorldObjectProvider.CurrentWorld == Data.Name);
                WorldObjectProvider.CurrentWorld = Data.Name;
                UIManager.SetCurrentPanel("StartPanel");
            }
            else {
                if (Data.Price <= SaveManager.Data.Coins) {
                    if (Confirm.gameObject.activeSelf) {
                        SaveManager.Data.Coins -= Data.Price;
                        SaveManager.Data.UnlockedWorlds.Add(Data.Name);
                        SaveManager.Save();
                        RefreshLock();
                        Confirm.gameObject.SetActive(false);
                        Selected.gameObject.SetActive(WorldObjectProvider.CurrentWorld == Data.Name);
                        WorldObjectProvider.CurrentWorld = Data.Name;
                        UIManager.SetCurrentPanel("StartPanel");
                    }
                    else {
                        Confirm.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnDeselect(BaseEventData eventData) {
        Confirm.gameObject.SetActive(false);
    }
}
