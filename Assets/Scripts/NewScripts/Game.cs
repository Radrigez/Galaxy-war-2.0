using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
  [SerializeField] private string _sceneName;

   public void Awake()
   {
      SceneManager.LoadScene(_sceneName);
      DontDestroyOnLoad(this.gameObject);
   }
}
