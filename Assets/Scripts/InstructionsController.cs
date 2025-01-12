using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public GameObject instructionsPanel;

    // Show the instructions
    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    // Hide the instructions
    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}
