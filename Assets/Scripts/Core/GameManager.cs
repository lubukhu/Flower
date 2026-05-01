using UnityEngine;
using UnityEngine.SceneManagement;

namespace finished3
{
    /// <summary>
    /// Quản lý luồng chính của trò chơi (Menu, Chọn nhân vật, Chơi game).
    /// Lưu trữ dữ liệu nhân vật đã chọn xuyên suốt các Scene.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Dữ liệu nhân vật")]
        public CharacterData selectedCharacterData;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Chuyển sang Scene Chọn nhân vật
        /// </summary>
        public void GoToCharacterSelection()
        {
            SceneManager.LoadScene("Scene_Selection");
        }

        /// <summary>
        /// Chuyển sang Scene Menu chính
        /// </summary>
        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }

        /// <summary>
        /// Bắt đầu game sau khi đã chọn nhân vật
        /// </summary>
        public void StartGame(CharacterData data)
        {
            selectedCharacterData = data;
            SceneManager.LoadScene("Scene_GameWorld");
        }

        /// <summary>
        /// Thoát ứng dụng
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
