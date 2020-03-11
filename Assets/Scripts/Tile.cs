using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public Material[] Texture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "TurretGround" && hit.collider.gameObject == this.gameObject)
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = Texture[1];
                if (Input.GetKeyDown(KeyCode.Mouse0) && ResourceManager.Currency >= 100)
                {
                    GameObject Tower = Instantiate(GameObject.FindGameObjectWithTag("Tower0"), this.transform.position, Quaternion.identity, this.transform);
                    Tower.tag = "Tower";
                    this.tag = "TowerGround";
                    ResourceManager.Currency = ResourceManager.Currency - 100;
                    GameObject ScriptFind = GameObject.FindGameObjectWithTag("Manager");
                    ScriptFind.GetComponent<ResourceManager>().TextUpdate();
                }
            }

            else if (hit.collider.tag == "TowerGround" && hit.collider.gameObject == this.gameObject)
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = Texture[1];
                if (Input.GetKeyDown(KeyCode.Mouse0) && ResourceManager.Currency >= 50)
                {
                    GameObject TowerObj = this.transform.GetChild(0).gameObject;
                    TowerObj.GetComponent<Tower>().Upgrade();
                    this.tag = "UpgradedTower";
                    GameObject ScriptFind = GameObject.FindGameObjectWithTag("Manager");
                    ResourceManager.Currency -= 50;
                    ScriptFind.GetComponent<ResourceManager>().TextUpdate();
                }
            }
            else
                gameObject.GetComponent<MeshRenderer>().material = Texture[0];
        }
        else
            this.gameObject.GetComponent<MeshRenderer>().material = Texture[0];
    }
}
