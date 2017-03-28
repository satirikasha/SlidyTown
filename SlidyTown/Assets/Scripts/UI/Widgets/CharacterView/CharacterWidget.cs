using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class CharacterWidget : MonoBehaviour, IDeselectHandler {

    public WorldData Data { get; private set; }

    public Transform Locked;
    public Transform Unlocked;
    public Transform Selected;
    public CharacterPriceWidget Price;
    public Transform Confirm;
    public Transform CommingSoon;

    private RibbonWidget _Ribbon;

    private bool _Unlocked;

    void Awake () {
        _Ribbon = this.GetComponentInChildren<RibbonWidget>();
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        WorldObjectProvider.OnWorldChanged += RefreshSelection;
	}

    void OnEnable() {
        if (Data != null) {
            RibbonType ribbonType;
            if (RibbonManager.Highlighted.TryGetValue(Data.Name, out ribbonType)) {
                _Ribbon.SetupRibbon(ribbonType);
                RibbonManager.OnRibbonObserved(Data.Name);
                return;
            }
        }
        _Ribbon.SetupRibbon(RibbonType.None);
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

        OnEnable();
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
                SetWorld(Data.Name);
            }
            else {
                if (Data.Price <= CurrencyManager.Coins) {
                    if (Confirm.gameObject.activeSelf) {
                        CurrencyManager.SpendCoins(Data.Price);
                        WorldManager.UnlockWorld(Data.Name);
                        SaveManager.Save();
                        RefreshLock();
                        Confirm.gameObject.SetActive(false);
                        SetWorld(Data.Name);
                    }
                    else {
                        Confirm.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private static Coroutine _CurrentSetWorldTask;
    private void SetWorld(string name) {
        if(_CurrentSetWorldTask == null) {
            _CurrentSetWorldTask = StartCoroutine(SetWorldTask(name));
        }
    }
    private IEnumerator SetWorldTask(string name) {
        Selected.gameObject.SetActive(true);
        yield return null;
        WorldObjectProvider.CurrentWorld = Data.Name;
        yield return null;
        WorldObjectProvider.ApplyWorldChanges();
        yield return null;
        //yield return new WaitForSecondsRealtime(0.25f);
        UIManager.SetCurrentPanel("StartPanel");
        _CurrentSetWorldTask = null;
    }

    public void OnDeselect(BaseEventData eventData) {
        Confirm.gameObject.SetActive(false);
    }
}
