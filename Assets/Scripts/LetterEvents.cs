using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LetterEvents : MonoBehaviour
{
    int doOnce = 0;
    public Image painel;
    public GameObject carta;
    public Evento[] allEvents;
    private Money money;
    private int consequenceIndex = 3;

    [Header("GUI")]
    public TextMeshProUGUI remetente;
    public TextMeshProUGUI mensagem;

    public GameObject botaoNegar;
    public GameObject textoNegar;
    void Start(){
        money = GameObject.Find("MoneyManager").GetComponent<Money>();
        carta.SetActive(false);
    }
    
    public void Consequencias(){
        if (consequenceIndex == 1){ //80K
            money.currency += 80000;
            StartCoroutine(Desligamento());
        }
        else if (consequenceIndex == 2){
            SceneManager.LoadScene("GameOver");
        }
        else if (consequenceIndex == 6){
            money.currency = 0;
        }
        EndLetter();
        //ADICIONAR MAIS EVENTOS AQUI
    }

    void StartLetter(Evento escolhido){
        if (escolhido.consequenciaIndex % 2 == 0){
            //BOTÃO DE NEGAR NÃO VAI APARECER NOS EVENTOS PARES POR CONVENIÊNCIA
            botaoNegar.SetActive(false);
            textoNegar.SetActive(false);
        }
        else{
            botaoNegar.SetActive(true);
            textoNegar.SetActive(true);
        }
        consequenceIndex = escolhido.consequenciaIndex;
        remetente.text = escolhido.remetente;
        mensagem.text = escolhido.mensagem;
        carta.SetActive(true);
        painel.enabled = true;
        FindObjectOfType<AudioManager>().PlaySound("carta");
        RemoveEvento(escolhido.consequenciaIndex);

    }

    private void RemoveEvento(int index){
        foreach (Evento e in allEvents){
            if (e.consequenciaIndex == index){
                e.consequenciaIndex = 0;
                e.mensagem = "";
                e.remetente = "";
            }
        }
    }

    public void EndLetter(){
        carta.SetActive(false);
        painel.enabled = false;
    }

    void Update(){
        if (money.currency == 0 && allEvents[2].consequenciaIndex != 0){
            StartLetter(allEvents[2]);
        }
        if (money.currency > 1000 && allEvents[0].consequenciaIndex != 0){
            StartLetter(allEvents[0]);
        }
        if (money.currency > 7563 && allEvents[3].consequenciaIndex != 0){
            if (doOnce == 0) {StartCoroutine(Carta3()); doOnce = 1;}
            //StartLetter(allEvents[3]);
        }
    }

    IEnumerator Desligamento(){
        yield return new WaitForSeconds(30);
        StartLetter(allEvents[1]); //Evento de desligamento

    }

    IEnumerator Carta3(){
        yield return new WaitForSeconds(15);
        StartLetter(allEvents[3]);
    }
}
