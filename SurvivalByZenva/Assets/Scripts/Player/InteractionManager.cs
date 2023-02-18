using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f; //jak czesto strzelac raycast
    private float lastCheckTime; //kiedy ostatni raz strzelalismy raycast
    public float maxCheckDistance; //odleglosc od przedmiotu
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main; //cashujmey kamere jako zmienna, zeby performance byl lepszy
    }

    private void Update()
    {
        //ten warunek pozwala na wykonywanie nie co frame, ale co 0.05seknudny 
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); //znajduje œrodek ekranu, srodkowy pixel
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) //sprawdza, czy trafilismy cos raycastem, o konkretnej layermask
            {
                if (hit.collider.gameObject != curInteractGameObject) // jesli obiekt nie jest obiektem obecnie wybranym, to sprawia ¿e zotaje tym obiektem 
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else//jesli nie trafiliœmy w interactable object
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }

    public void OnInteractInput (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null) // podpiecie klawisza E jako kalwis akcji&&... sprawia zeby gra nie proboweala onterakcji jesli tam nie ma obiektu
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}

public interface IInteractable
{
    string GetInteractPrompt(); //pozwala na pojawienie sie pasuj¹cego txtu po najechaniu na ka¿dy przedmiot
    void OnInteract();
}
