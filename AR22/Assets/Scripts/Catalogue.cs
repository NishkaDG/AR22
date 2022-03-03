using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catalogue : MonoBehaviour
{
    
    
    [SerializeField]
    private GameObject togglePosterPrefab;
    
    [SerializeField]
    private ToggleGroup toggleGroup;

    [SerializeField]
    private RealObjectAdder realObjAdder;

    // Start is called before the first frame update
    void Start()
    {

        Sprite[] sprites = Resources.LoadAll<Sprite>("Posters");
        for (int x = 0; x < sprites.Length; x++) {
            GameObject go = Instantiate(togglePosterPrefab, this.transform);
            go.SetActive(true);
            
            Material mat = new Material(Shader.Find("Standard"));
            mat.SetTexture("_MainTex", sprites[x].texture);

            Toggle to = go.GetComponent<Toggle>(); 
            to.group = toggleGroup;
            
            int assignedIndex = x;
            to.onValueChanged.AddListener(delegate {
                realObjAdder.ChangeCurrentMaterial(assignedIndex);
                realObjAdder.ChangeCurrentName(sprites[assignedIndex].name);
            });

            Image img = go.GetComponentInChildren<Image>();
            img.sprite = sprites[x];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
