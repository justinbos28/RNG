using UnityEngine;
using UnityEngine.UI;

public class RNGCalculator : MonoBehaviour
{
    // note this is made by chat gpt to easily calculate the rng value

    public InputField Input; // User input field
    public Button Button;    // Button to trigger calculation
    public Text Text;        // Text to display the result

    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(Calculate); // Attach the Calculate method to the button
    }

    // Method to calculate the desired value
    public void Calculate()
    {
        float inputValue;

        // Parse the input value as a float
        if (float.TryParse(Input.text, out inputValue))
        {
            // Perform the calculation
            float result = (1 / ((inputValue / 1) + 1)) * 100;

            // Display the result in the Text component
            Text.text = "Result: " + result.ToString("F6");
        }
        else
        {
            // Handle invalid input
            Text.text = "Invalid input. Please enter a number.";
        }
    }
}
