using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    // Referências para os Canvas da UI que serão animados
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject itemsMenuCanvas;
    [SerializeField] private GameObject ARPositionCanvas;
    [SerializeField] private GameObject headerMenuCanvas;

    // O método Start é chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Inscreve o gerenciador da UI nos eventos do GameManager para acionar as animações
        GameManager.instance.OnMainMenu += ActivateMainMenu;
        GameManager.instance.OnItemsMenu += ActivateItemsMenu;
        GameManager.instance.OnARPosition += ActivateARPosition;
        GameManager.instance.OnHeaderMenu += ActivateHeaderMenu;
        
    }

    private void ActivateHeaderMenu()
    {
        throw new NotImplementedException();
    }

    // Ativa os elementos da tela de menu principal com animações
    private void ActivateMainMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);


        // Esconde o menu de itens e o anima para fora da tela
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        // Esconde o Canvas da posição de AR
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }

    // Ativa os elementos da tela de itens com animações
    private void ActivateItemsMenu()
    {
        // Esconde o menu principal e o anima para fora da tela
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    

        // Mostra o menu de itens com animações
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);
    }

    // Ativa os elementos da tela de posição de AR com animações
    private void ActivateARPosition()
    {
        // Esconde o menu principal e o anima para fora da tela
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


        // Esconde o menu de itens e o anima para fora da tela
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        // Mostra o Canvas da posição de AR com animações
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
}
