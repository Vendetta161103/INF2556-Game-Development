using UnityEngine;

public class WeatherFSM : MonoBehaviour
{
    // Alle States
    public enum WeatherState
    {
        Sunny,
        Cloudy,
        Rainy,
        Stormy
    }

    // Aktueller State
    public WeatherState currentState;

    // Timer für automatische Wechsel
    private float timer;
    public float stateDuration = 5f;

    void Start()
    {
        currentState = WeatherState.Sunny;
        ApplyState();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stateDuration)
        {
            ChangeState();
            timer = 0f;
        }
    }

    void ChangeState()
    {
        switch (currentState)
        {
            case WeatherState.Sunny:
                currentState = WeatherState.Cloudy;
                break;

            case WeatherState.Cloudy:
                currentState = WeatherState.Rainy;
                break;

            case WeatherState.Rainy:
                currentState = WeatherState.Stormy;
                break;

            case WeatherState.Stormy:
                currentState = WeatherState.Sunny;
                break;
        }

        ApplyState();
    }

    void ApplyState()
    {
        switch (currentState)
        {
            case WeatherState.Sunny:
                Debug.Log("Sunny State");
                Camera.main.backgroundColor = Color.cyan;
                break;

            case WeatherState.Cloudy:
                Debug.Log("Cloudy State");
                Camera.main.backgroundColor = Color.gray;
                break;

            case WeatherState.Rainy:
                Debug.Log("Rainy State");
                Camera.main.backgroundColor = Color.blue;
                break;

            case WeatherState.Stormy:
                Debug.Log("Stormy State");
                Camera.main.backgroundColor = Color.black;
                break;
        }
    }
}