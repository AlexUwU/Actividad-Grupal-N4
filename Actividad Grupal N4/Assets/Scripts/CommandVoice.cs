using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

public class CommandVoice : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer; // creo mi reconocedor de comandode voz

    Dictionary<string, Action> actions = new Dictionary<string, Action>(); // creo las comando

    void Start()
    {
        //creo las acciones segun el comandod e voz
        actions.Add("jugar",Jugar);
        actions.Add("salir",Salir);
        actions.Add("reiniciar",Reiniciar);
        actions.Add("menu", Menu);
        //


        //seteo el ronocedor de voz y lo inicio
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }

    private void Menu()
    {
        Debug.Log("Menu Del Juego");
        SceneManager.LoadScene(0);
    }

    private void Reiniciar()
    {
        Debug.Log("Se Reinicio el Level");
        SceneManager.LoadScene(1);
    }

    private void Salir()
    {
        Debug.Log("Juego Finalizado");
        Application.Quit();
    }

    private void Jugar()
    {
        Debug.Log("Iniciar Juego");
        SceneManager.LoadScene(1);
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs command)
    {
        actions[command.text].Invoke();
    }

}
