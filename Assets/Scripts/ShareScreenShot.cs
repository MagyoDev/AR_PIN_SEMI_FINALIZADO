using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    private ARPointCloudManager aRPointCloudManager;

    // Inicializa��o � chamada antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Encontra o ARPointCloudManager no cen�rio
        aRPointCloudManager = FindAnyObjectByType<ARPointCloudManager>();
    }

    // M�todo p�blico que tira a captura de tela e a compartilha
    public void TakeScreenShot()
    {
        // Ativa/desativa o conte�do de AR para tirar a captura de tela sem elementos visuais AR
        TurnOnOffARContents();
        // Inicia a rotina para tirar a captura de tela e compartilh�-la
        StartCoroutine(TakeScreenshotAndShare());
    }

    // M�todo que ativa/desativa o conte�do de AR e o menu principal
    private void TurnOnOffARContents()
    {
        // Obt�m os pontos da nuvem de pontos detectados pelo ARPointCloudManager
        var points = aRPointCloudManager.trackables;
        // Alterna a ativa��o/desativa��o de cada ponto da nuvem de pontos
        foreach (var point in points)
        {
            point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
        // Alterna a ativa��o/desativa��o do menu principal
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
    }

    // M�todo que tira a captura de tela e a compartilha
    private IEnumerator TakeScreenshotAndShare()
    {
        // Aguarda o final do quadro (frame) para garantir que tudo esteja renderizado
        yield return new WaitForEndOfFrame();

        // Cria uma nova textura com o tamanho da tela e captura a imagem da tela
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Define o caminho do arquivo tempor�rio para salvar a captura de tela como imagem PNG
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // Para evitar vazamento de mem�ria, a textura � destru�da ap�s salvar a imagem
        Destroy(ss);

        // Compartilha a captura de tela usando a biblioteca NativeShare, que permite compartilhar em v�rias plataformas
        new NativeShare().AddFile(filePath)
            .SetSubject("Assunto vai aqui").SetText("Teste!")
            .SetCallback((result, shareTarget) => Debug.Log("Resultado do compartilhamento: " + result + ", aplicativo selecionado: " + shareTarget))
            .Share();

        // Reativa o conte�do de AR ap�s compartilhar a captura de tela
        TurnOnOffARContents();
    }
}