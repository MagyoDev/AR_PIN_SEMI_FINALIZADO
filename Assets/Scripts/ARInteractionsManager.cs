using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionsManager : MonoBehaviour
{
    // Refer�ncias para a c�mera AR e o ARRaycastManager
    [SerializeField] private Camera aRCamera;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Refer�ncias para o objeto de apontamento, modelo 3D associado ao item e item selecionado
    private GameObject aRPointer;
    private GameObject item3DModel;
    private GameObject itemSelected;

    // Vari�veis para controlar estados e intera��es
    private bool isInitialPosition;
    private bool isOverUI;
    private bool isOver3DModel;
    private Vector2 initialTouchPos;

    // Propriedade para definir o modelo 3D associado ao item
    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = aRPointer.transform.position;
            item3DModel.transform.parent = aRPointer.transform;
            isInitialPosition = true;
        }
    }

    // Start � chamado antes do primeiro quadro (frame) ser atualizado
    void Start()
    {
        // Obt�m a refer�ncia ao objeto de apontamento, o ARRaycastManager e se inscreve no evento OnMainMenu do GameManager
        aRPointer = transform.GetChild(0).gameObject;
        aRRaycastManager = FindAnyObjectByType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

    // Update � chamado uma vez por quadro (frame)
    void Update()
    {
        // Se o modelo 3D ainda n�o foi posicionado na cena AR
        if (isInitialPosition)
        {
            // Obt�m o ponto central da tela e faz um raycast para detectar planos
            Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            aRRaycastManager.Raycast(middlePointScreen, hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                // Move este objeto para a posi��o do primeiro ponto de acerto e define sua rota��o
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                aRPointer.SetActive(true);
                isInitialPosition = false;
            }
        }

        // Se houver toques na tela
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);

            if (touchOne.phase == TouchPhase.Began)
            {
                var touchPosition = touchOne.position;
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
            }

            if (touchOne.phase == TouchPhase.Moved)
            {
                // Faz um raycast para detectar planos e move o modelo 3D se n�o estiver sobre a interface de usu�rio
                if (aRRaycastManager.Raycast(touchOne.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    if (!isOverUI && isOver3DModel)
                    {
                        transform.position = hitPose.position;
                    }
                }
            }

            // Se houver dois toques na tela
            if (Input.touchCount == 2)
            {
                Touch touchTwo = Input.GetTouch(1);

                // Armazena a posi��o inicial dos dois toques
                if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
                {
                    initialTouchPos = touchTwo.position - touchOne.position;
                }

                // Rotaciona o modelo 3D se os dois toques estiverem em movimento
                if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = touchTwo.position - touchOne.position;
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
                    item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.eulerAngles.y - angle, 0);
                    initialTouchPos = currentTouchPos;
                }
            }

            // Se tocar sobre um modelo 3D, selecione-o e coloque-o na posi��o de apontamento
            if (isOver3DModel && item3DModel == null && !isOverUI)
            {
                GameManager.instance.ARPosition();
                item3DModel = itemSelected;
                itemSelected = null;
                aRPointer.SetActive(true);
                transform.position = item3DModel.transform.position;
                item3DModel.transform.parent = aRPointer.transform;
            }
        }
    }

    // M�todo para verificar se o toque foi realizado sobre um modelo 3D
    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            if (hit3DModel.collider.CompareTag("Item"))
            {
                itemSelected = hit3DModel.transform.gameObject;
                return true;
            }
        }

        return false;
    }

    // M�todo para verificar se o toque foi realizado sobre a interface de usu�rio
    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);

        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        return result.Count > 0;
    }

    // M�todo para posicionar o modelo 3D associado ao item na posi��o inicial
    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRPointer.SetActive(false);
            item3DModel = null;
        }
    }

    // M�todo para deletar o modelo 3D atual
    public void DeleteItem()
    {
        Destroy(item3DModel);
        aRPointer.SetActive(false);
        GameManager.instance.MainMenu();
    }
}