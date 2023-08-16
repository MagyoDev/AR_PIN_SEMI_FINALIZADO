using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    // Variáveis para armazenar as informações do item
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;

    // Referência ao gerenciador de interações de AR
    private ARInteractionsManager interactionsManager;

    // Propriedades para definir as informações do item
    public string ItemName { set { itemName = value; } }
    public string ItemDescription { set { itemDescription = value; } }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }


    // Start é chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Atualiza o texto do botão com o nome, a imagem e a descrição do item
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;

        // Obtém o componente de botão e adiciona dois ouvintes de clique
        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition); // Chama o evento ARPosition do GameManager
        button.onClick.AddListener(Create3DModel); // Chama o método Create3DModel deste script

        // Encontra o ARInteractionsManager no cenário
        interactionsManager = FindAnyObjectByType<ARInteractionsManager>();
    }

    // Método para criar o modelo 3D associado ao item na posição de AR
    private void Create3DModel()
    {
        // Instancia o modelo 3D do item na posição atual da interação de AR
        interactionsManager.Item3DModel = Instantiate(item3DModel);

    }
}
