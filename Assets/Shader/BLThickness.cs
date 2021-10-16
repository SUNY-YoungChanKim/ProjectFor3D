using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLThickness : MonoBehaviour
{

    private Material material;
    [SerializeField] float Thickness;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update() {
        this.gameObject.GetComponent<MeshRenderer>().materials[1].SetFloat("Thickness",0.2f);
        Debug.Log(this.gameObject.GetComponent<MeshRenderer>().materials[1].GetFloat("Thickness"));
    }
    // Update is called once per frame

}
