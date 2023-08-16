using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que representa um item no jogo.
// É um ScriptableObject, o que permite que seja criado e editado no Unity Editor.
[CreateAssetMenu]
public class Item : ScriptableObject
{
    // Nome do item.
    public string ItemName;

    // Imagem do item.
    public Sprite ItemImage;

    // Descrição do item.
    public string ItemDescription;

    // Modelo 3D do item, que será utilizado no jogo.
    public GameObject Item3DModel;
}
