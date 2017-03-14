using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : SingletonBehaviour<CharacterView> {

    public CharacterWidget Prefab;
    public AspectRatioFitter Content;

    private int RowLength;

	void Start () {
        GenerateContent();
	}

    private void GenerateContent() {
        RowLength = (float)Screen.width / (float)Screen.height > 0.6 ? 4 : 3;
        Content.aspectRatio = RowLength / (float)(WorldConfig.Instance.Data.Count / RowLength + 2);
        for (int i = 0; i < WorldConfig.Instance.Data.Count / RowLength + 2; i++) {
            var row = AddRow();
            for (int j = 0; j < RowLength; j++) {
                if(WorldConfig.Instance.Data.Count > i * RowLength + j) {
                    AddWidget(row, WorldConfig.Instance.Data[i * RowLength + j]);
                }
                else {
                    AddWidget(row, null);
                }
            }
        }
    }

    private Transform AddRow() {
        var row = new GameObject("Row", typeof(HorizontalLayoutGroup)).GetComponent<HorizontalLayoutGroup>();
        row.transform.SetParent(Content.transform);
        row.transform.localPosition = Vector3.zero;
        row.transform.localRotation = Quaternion.identity;
        row.transform.localScale = Vector3.one;
        row.childForceExpandHeight = true;
        row.childForceExpandWidth = true;
        row.childControlHeight = true;
        row.childControlWidth = true;
        return row.transform;
    }

    private void AddWidget(Transform row, WorldData data) {
        var widget = Instantiate(Prefab, row);
        widget.transform.localPosition = Vector3.zero;
        widget.transform.localRotation = Quaternion.identity;
        widget.transform.localScale = Vector3.one;
        widget.SetWorldData(data);
    }
}
