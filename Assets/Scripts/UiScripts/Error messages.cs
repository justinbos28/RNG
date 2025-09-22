using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Errormessages : MonoBehaviour
{
    [Header("Error Message main")]
    public Text ErrorMessageMain;
    public float Timer;
    public bool timerActive = false;
    public bool AnimationActive = false;
    public GameObject ErrorMessageMainObject;

    [Header("Error Message Machines")]
    public Text ErrorMessageMachines;
    public float TimerMachines;
    public bool timerActiveMachines = false;
    public GameObject ErrorMessageMachinesObject;

    [Header("Animation")]
    [SerializeField] private float Duration;
    [SerializeField] private RectTransform ErrorMessageMainRectTransform;
    [SerializeField] private RectTransform ErrorMessageMachinesRectTransform;
    [SerializeField] private float StartPositionY, EndPositionY;

    public void SetErrorMessageMain(string message)
    {
        if (ErrorMessageMain != null)
        {
            timerActive = true;
            ErrorMessageMain.text = message;
            ErrorMessageMainObject.SetActive(true);
            MainErrorMessageStart();
            AnimationActive = true;
        }
        else
        {
            Debug.LogError("ErrorMessageMain is not assigned in the inspector.");
        }
    }

    public void SetErrorMessageMachines(string message)
    {
        if (ErrorMessageMachines != null)
        {
            timerActiveMachines = true;
            ErrorMessageMachines.text = message;
            ErrorMessageMachinesObject.SetActive(true);
            MachinesErrorMessageStart();
        }
        else
        {
            Debug.LogError("ErrorMessageMachines is not assigned in the inspector.");
        }
    }
    private void MachinesErrorMessageStart()
    {
        ErrorMessageMachinesRectTransform.DOAnchorPosY(EndPositionY, Duration);
    }
    private async Task MachinesErrorMessageEnd()
    {
        await ErrorMessageMachinesRectTransform.DOAnchorPosY(StartPositionY, Duration).AsyncWaitForCompletion();
    }

    private void MainErrorMessageStart()
    {
        if (AnimationActive)
        {
            ErrorMessageMainRectTransform.position = new Vector2(ErrorMessageMainRectTransform.position.x, StartPositionY);
            Timer = 0;
            ErrorMessageMainRectTransform.DOAnchorPosY(EndPositionY, Duration);
        }
        else
        {
            ErrorMessageMainRectTransform.DOAnchorPosY(EndPositionY, Duration);
        }
    }

    private async Task MainErrorMessageEnd()
    {

        await ErrorMessageMainRectTransform.DOAnchorPosY(StartPositionY, Duration).AsyncWaitForCompletion();
    }

    private async void Update()
    {
        // timers
        if (timerActive)
        {
            Timer += Time.deltaTime;
        }
        if (timerActiveMachines)
        {
            TimerMachines += Time.deltaTime;
        }

        // check if timers are active and reset them after 5 seconds
        if (TimerMachines > 5)
        {
            timerActiveMachines = false;
            TimerMachines = 0;
            await MachinesErrorMessageEnd();
            ErrorMessageMachinesObject.SetActive(false);
            AnimationActive = false;
        }
        if (Timer > 5)
        {
            timerActive = false;
            Timer = 0;
            await MainErrorMessageEnd();
            ErrorMessageMainObject.SetActive(false);
            AnimationActive = false;
        }
    }
}
