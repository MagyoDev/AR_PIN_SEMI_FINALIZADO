using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Lista de itens que serão mostrados na interface do usuário
    [SerializeField] private List<Item> items = new List<Item>();
    // Referência ao contêiner onde os botões dos itens serão criados
    [SerializeField] private GameObject buttonContainer;
    // Referência ao prefab do script ItemButtonManager, que gerencia os botões dos itens
    [SerializeField] private ItemButtonManager itemButtonManager;

    // Start é chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Inscreve o método CreateButtons no evento OnItemsMenu do GameManager
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    // Método para criar os botões dos itens na interface do usuário
    private void CreateButtons()
    {
        // Para cada item na lista de itens, crie um botão correspondente
        foreach (var item in items)
        {
            // Instancia o prefab do ItemButtonManager dentro do contêiner de botões
            ItemButtonManager itemButton = Instantiate(itemButtonManager, buttonContainer.transform);

            // Configura as informações do item para o botão recém-criado
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;

            // Define o nome do objeto para o nome do item (opcional)
            itemButton.name = item.ItemName;
        }

        // Remove a inscrição do método CreateButtons do evento OnItemsMenu para evitar chamadas repetidas
        GameManager.instance.OnItemsMenu -= CreateButtons;
    }
}
