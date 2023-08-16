using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Eventos que ser�o acionados para notificar outras partes do c�digo sobre mudan�as no jogo
    public event Action OnMainMenu; // Evento para ativar o menu principal
    public event Action OnItemsMenu; // Evento para ativar o menu de itens
    public event Action OnHeaderMenu; // Evento para ativar o menu de header
    public event Action OnARPosition; // Evento para ativar a posi��o de realidade aumentada (AR)
    
    // Inst�ncia est�tica do GameManager, permitindo acesso global a partir de outras classes
    public static GameManager instance;

    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do GameManager, de forma que s� exista uma em todo o jogo
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start � chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Inicia o jogo com o menu principal
        MainMenu();
    }

    // M�todo para ativar o menu principal e chamar o evento associado
    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Menu Principal Ativado");
    }

    // M�todo para ativar o menu de itens e chamar o evento associado
    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Menu de Itens Ativado");
    }

    // M�todo para ativar a posi��o de realidade aumentada (AR) e chamar o evento associado
    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("Posi��o de AR Ativada");
    }

    // M�todo para ativar o menu de header e chamar o evento associado
    public void HeaderMenu()
    { 
        OnHeaderMenu?.Invoke();
        Debug.Log("Header");
    }

    // M�todo para fechar o aplicativo
    public void CloseAPP()
    {
        Application.Quit();
    }
}