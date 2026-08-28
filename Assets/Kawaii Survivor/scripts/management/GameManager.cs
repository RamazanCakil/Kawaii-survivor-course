using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering.Universal;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        setGameState(GameState.MENU);
    }

    public void StartGame()             => setGameState(GameState.GAME);
    public void StartWeaponSelection()  => setGameState(GameState.WEAPONSELECTİON);
    public void StartShop()             => setGameState(GameState.SHOP);

    // Update, oyun çalıştığı sürece her karede (frame) sürekli olarak çalışır
    void Update()
    {
        
    }

    public void setGameState(GameState gameState)
    {
        // FindAnyObjectByType yerine FindObjectsByType kullanıldı.
        // FindObjectsInactive.Exclude parametresi inaktif objelerin aranıp aranmayacağını belirtir (optimizasyon için önemlidir).
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
        {
            // 's' takısı kaldırılarak koleksiyon yerine tekil eleman üzerinden metot çağrıldı.
            gameStateListener.GameStateChangedCallBack(gameState);
        }

        if(gameState == GameState.GAMEOVER)
        {
            ManageGameover();
        }

    }

    public void ManageGameover()
    {
        LeanTween.delayedCall(2,() => SceneManager.LoadScene(0));

    }


    public void WaveComplateCallback()
    {
        // "Eğer oyuncu bu dalgada seviye atladıysa Transition ekranına git"
        if (Player.instance.hasLeveledUp())
        {
            setGameState(GameState.WAVETRANSITION);
        }
        // "Eğer oyuncu seviye atlaMAdıysa doğrudan Shop ekranına git"
        else
        {
            setGameState(GameState.SHOP);
        }
    }
}

public interface IGameStateListener { 

    void GameStateChangedCallBack(GameState gameState);

}