using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    private ARPointCloudManager aRPointCloudManager;

    // Inicialização é chamada antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Encontra o ARPointCloudManager no cenário
        aRPointCloudManager = FindAnyObjectByType<ARPointCloudManager>();
    }

    // Método público que tira a captura de tela e a compartilha
    public void TakeScreenShot()
    {
        // Ativa/desativa o conteúdo de AR para tirar a captura de tela sem elementos visuais AR
        TurnOnOffARContents();
        // Inicia a rotina para tirar a captura de tela e compartilhá-la
        StartCoroutine(TakeScreenshotAndShare());
    }

    // Método que ativa/desativa o conteúdo de AR e o menu principal
    private void TurnOnOffARContents()
    {
        // Obtém os pontos da nuvem de pontos detectados pelo ARPointCloudManager
        var points = aRPointCloudManager.trackables;
        // Alterna a ativação/desativação de cada ponto da nuvem de pontos
        foreach (var point in points)
        {
            point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
        // Alterna a ativação/desativação do menu principal
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
    }

    // Método que tira a captura de tela e a compartilha
    private IEnumerator TakeScreenshotAndShare()
    {
        // Aguarda o final do quadro (frame) para garantir que tudo esteja renderizado
        yield return new WaitForEndOfFrame();

        // Cria uma nova textura com o tamanho da tela e captura a imagem da tela
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Define o caminho do arquivo temporário para salvar a captura de tela como imagem PNG
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // Para evitar vazamento de memória, a textura é destruída após salvar a imagem
        Destroy(ss);

        // Compartilha a captura de tela usando a biblioteca NativeShare, que permite compartilhar em várias plataformas
        new NativeShare().AddFile(filePath)
            .SetSubject("Assunto vai aqui").SetText("Teste!")
            .SetCallback((result, shareTarget) => Debug.Log("Resultado do compartilhamento: " + result + ", aplicativo selecionado: " + shareTarget))
            .Share();

        // Reativa o conteúdo de AR após compartilhar a captura de tela
        TurnOnOffARContents();
    }
}