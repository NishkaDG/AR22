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
    
    public static List<Material> materials;
    public static List<string> posternames;

    // Start is called before the first frame update
    void Start()
    {

        Sprite[] sprites = Resources.LoadAll<Sprite>("Posters");
        materials = new List<Material>(sprites.Length);
        posternames = new List<string>(sprites.Length);
        for (int x = 0; x < sprites.Length; x++) {
            GameObject go = Instantiate(togglePosterPrefab, this.transform);
            go.SetActive(true);
            
            Material mat = new Material(Shader.Find("Standard"));
            mat.SetTexture("_MainTex", sprites[x].texture);
            mat.name = "poster_" + x.ToString();
            materials.Add(mat);
            
            posternames.Add(sprites[x].name);

            Toggle to = go.GetComponent<Toggle>(); 
            to.group = toggleGroup;
            
            int assignedIndex = x;
            to.onValueChanged.AddListener(delegate {
                realObjAdder.ChangeCurrentMaterial(assignedIndex);
                realObjAdder.ChangeCurrentName(assignedIndex);
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
