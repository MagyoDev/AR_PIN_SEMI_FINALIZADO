using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    // Vari�veis para armazenar as informa��es do item
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;

    // Refer�ncia ao gerenciador de intera��es de AR
    private ARInteractionsManager interactionsManager;

    // Propriedades para definir as informa��es do item
    public string ItemName { set { itemName = value; } }
    public string ItemDescription { set { itemDescription = value; } }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }


    // Start � chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Atualiza o texto do bot�o com o nome, a imagem e a descri��o do item
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;

        // Obt�m o componente de bot�o e adiciona dois ouvintes de clique
        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition); // Chama o evento ARPosition do GameManager
        button.onClick.AddListener(Create3DModel); // Chama o m�todo Create3DModel deste script

        // Encontra o ARInteractionsManager no cen�rio
        interactionsManager = FindAnyObjectByType<ARInteractionsManager>();
    }

    // M�todo para criar o modelo 3D associado ao item na posi��o de AR
    private void Create3DModel()
    {
        // Instancia o modelo 3D do item na posi��o atual da intera��o de AR
        interactionsManager.Item3DModel = Instantiate(item3DModel);

    }
}
