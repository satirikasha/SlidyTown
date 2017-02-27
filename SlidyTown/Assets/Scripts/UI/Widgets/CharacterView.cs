using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour {

    public CharacterWidget Prefab;
    public AspectRatioFitter Content;

	void Start () {
        GenerateContent();
	}

    private void GenerateContent() {
        Content.aspectRatio /= WorldConfig.Instance.Data.Count / 3 + 1;
        for (int i = 0; i < WorldConfig.Instance.Data.Count / 3 + 1; i++) {
            var row = AddRow();
            for (int j = 0; j < 3; j++) {
                if(WorldConfig.Instance.Data.Count > i * 3 + j) {
                    AddWidget(row, WorldConfig.Instance.Data[i * 3 + j]);
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
        row.transform.localScale = Vector3.one;
        row.childForceExpandHeight = true;
        row.childForceExpandWidth = true;
        row.childControlHeight = true;
        row.childControlWidth = true;
        return row.transform;
    }

    private void AddWidget(Transform row, WorldData data) {
        var widget = Instantiate(Prefab, row);
        widget.transform.localScale = Vector3.one;
        widget.Data = data;
    }
}
