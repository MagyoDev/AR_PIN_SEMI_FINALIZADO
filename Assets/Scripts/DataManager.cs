using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Lista de itens que ser�o mostrados na interface do usu�rio
    [SerializeField] private List<Item> items = new List<Item>();
    // Refer�ncia ao cont�iner onde os bot�es dos itens ser�o criados
    [SerializeField] private GameObject buttonContainer;
    // Refer�ncia ao prefab do script ItemButtonManager, que gerencia os bot�es dos itens
    [SerializeField] private ItemButtonManager itemButtonManager;

    // Start � chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Inscreve o m�todo CreateButtons no evento OnItemsMenu do GameManager
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    // M�todo para criar os bot�es dos itens na interface do usu�rio
    private void CreateButtons()
    {
        // Para cada item na lista de itens, crie um bot�o correspondente
        foreach (var item in items)
        {
            // Instancia o prefab do ItemButtonManager dentro do cont�iner de bot�es
            ItemButtonManager itemButton = Instantiate(itemButtonManager, buttonContainer.transform);

            // Configura as informa��es do item para o bot�o rec�m-criado
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;

            // Define o nome do objeto para o nome do item (opcional)
            itemButton.name = item.ItemName;
        }

        // Remove a inscri��o do m�todo CreateButtons do evento OnItemsMenu para evitar chamadas repetidas
        GameManager.instance.OnItemsMenu -= CreateButtons;
    }
}
