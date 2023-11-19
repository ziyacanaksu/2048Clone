using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Tile  : MonoBehaviour
{
   
    public Vector3 position;
    private int Number;
    [SerializeField] private TextMeshPro NumberText;
    



    private void Start(){
        UpdateNumberText();


}
    private void Awake() {
   
        
    }


    private void UpdateNumberText(){
         NumberText.text = Number.ToString();

    }
    public void SetNumber(int num){
        Number = num;
    }
    public int GetNumber(){
        return Number;

    }
    public void SetPosition(int x , int y){
        position.x = x;
        position.y = 0;
        position.z = y;
    }


    public void DestroyTile()
{

    Destroy(gameObject);

}

}
