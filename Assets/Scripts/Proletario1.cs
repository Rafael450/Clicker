using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Proletario : MonoBehaviour
{
    float timeCounter = 0;
    private int quantidade = 0;

     [SerializeField]
    private int multiplicadorBase;

    public int precoBase;
    private int preco;

    [HideInInspector]
    public Money money;

    public void AumentaQuantidade(){
        if (money.currency >= preco){
            quantidade++;
            money.currency -= preco;
            preco += precoBase;
        }
        
    }

    void Start(){
        money = GameObject.Find("MoneyManager").GetComponent<Money>();
        preco = precoBase;
    }

    void Update(){
        if (timeCounter >= 1f){
            timeCounter = 0f;
            money.currency += quantidade * multiplicadorBase;
        }
        timeCounter += Time.deltaTime;
    }
}
