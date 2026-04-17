using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BrewedInk.CRT;

namespace finished3
{

    public class Chapter1Controller : MonoBehaviour
    {
        public static Chapter1Controller Instance { get; private set; }

        [Header("🕒 Bước 5: Chết và Tái sinh")]
        public ItemData shoesReward;
        public GameObject deathOverlay;
        public string nextSceneName = "chapter_2";

        [Header("🕒 Bước 1: Hướng dẫn nhấp chuột")]
        public Vector2Int tutorialTileGridPos = new Vector2Int(2, 2);
        public float fullMapPulseDelay = 3f;
        public ComplexAudioStep introStep; // Âm thanh lúc chuẩn bị/bắt đầu
        public bool loopIntro = false;

        [System.Serializable]
        public struct ComplexAudioStep
        {
            [Tooltip("Hỗ trợ danh sách các hiệu ứng âm thanh (AudioVariant) dùng chung")]
            public AudioVariant[] clips;
            
            public float waitTime;   // Thời gian chờ sau khi đánh hết mảng Clip
            public float pulseSpeed; // Tốc độ nháy Sin khi phát Clip này
            
            [Header("Hiệu ứng Hình ảnh")]
            public bool useGlitch;
            public CRTGlitchDataObject glitchPreset;
        }

        [Header("🕒 Bước 2 & 4: Cấu hình âm thanh")]
        public float globalVolumeMultiplier = 1f; 
        
        [Header("🎵 Nhạc Môi Trường Khi Bắt Đầu Nháy")]
        public AudioClip backgroundMusic1;
        [Range(0, 5)] public float backgroundMusic1Volume = 1f;
        public AudioClip backgroundMusic2;
        [Range(0, 5)] public float backgroundMusicVolume2 = 1f;

        private int activeBGMusic2ID = -1; // ID để quản lý bản nhạc nền thứ 2 (phát dạng Sound loop)

        public ComplexAudioStep[] clickSteps; // Chuỗi click/đập từ từ
        public ComplexAudioStep loopStep;      // Vòng lặp dồn dập
        public ComplexAudioStep screamStep;    // Tiếng hét / Jumpscare
        
        [Header("⚙️ Cấu hình nhịp độ (File 5)")]
        public float initialLoopDelay = 0.6f;
        public float finalLoopDelay = 0.15f;
        public float loopAcceleration = 0.05f;
        public float finalPulseSpeed = 20f;
        
        private float currentPulseSpeed = 5f;
        
        [Header("👻 Cấu hình Jumpscare & Enemy")]
        [Range(0.1f, 5f)] [Tooltip("Hệ số nhân tốc độ dịch chuyển loạn xạ của Enemy (Mặc định: 1)")]
        public float enemyTeleportSpeedMultiplier = 1f; 
        public float jumpscareAppearanceDelay = 0f; // Độ trễ hiện GIF
        
        public Image jumpscareImage;
        public SpriteRenderer jumpscareSpriteRenderer; // Thêm hỗ trợ SpriteRenderer
        public Sprite[] jumpscareFrames;
        public float jumpscareFps = 15f;

        // Glitch Caching
        private CRTCameraBehaviour crtCamera;
        private CRTData originalCrtData;
        private Coroutine currentGlitchRoutine;

        private CharacterInfo enemyInfo; // Bộ đệm lưu Enemy để dịch chuyển

        [System.Serializable]
        private struct TilePulseData
        {
            public SpriteRenderer renderer;
            public OverlayTile tile;
        }

        private List<TilePulseData> pulseDataList = new List<TilePulseData>();
        private OverlayTile tutorialTile;
        private bool isPlayerSpawned = false;
        private bool isDeathSequenceStarted = false;
        private bool isBGMusicStarted = false; // Cờ bảo vệ chống phát chồng nhạc nền
        private bool isFullMapPulseActive = false;

        [Header("🕒 Bước 3: Giới hạn di chuyển")]
        public int maxSteps = 2;
        private int currentSteps = 0;
        private bool isMovementLocked = false;

        [Header("🕒 Bước 4: Áp lực Giai đoạn 2")]
        public float idlePressureDelay = 5f;
        private float idleTimerAfterSpawn = 0f;

        private List<int> activeIntroAudioIDs = new List<int>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(InitStep1Routine());
        }

        private void Update()
        {
            // Logic cho Bước 4: Áp lực sau khi đã Spawn
            if (isPlayerSpawned && !isDeathSequenceStarted)
            {
                idleTimerAfterSpawn += Time.deltaTime;

                if (idleTimerAfterSpawn >= idlePressureDelay)
                {
                    GameLogger.Log("Chapter 1: Đã 5s không di chuyển sau khi Spawn. Bắt đầu áp lực âm thanh.");
                    StartDeathSequence();
                }
            }
        }

        private IEnumerator InitStep1Routine()
        {
            yield return null;

            if (MapManager.Instance != null && MapManager.Instance.map != null)
            {
                foreach (var tile in MapManager.Instance.map.Values)
                {
                    var renderer = tile.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                    {
                        pulseDataList.Add(new TilePulseData { renderer = renderer, tile = tile });
                    }

                    if (tile.grid2DLocation == tutorialTileGridPos)
                    {
                        tutorialTile = tile;
                        tutorialTile.isShowing = true;
                    }
                }
            }

            // Phát âm thanh Intro (Bước 1)
            PlayComplexStep(introStep, loopIntro);

            StartCoroutine(PulseTilesRoutine());
            StartCoroutine(TutorialTimelineRoutine());
        }

        private IEnumerator TutorialTimelineRoutine()
        {
            // Chờ 10 giây (Full Map Pulse Delay) đầu game
            yield return new WaitForSeconds(fullMapPulseDelay);

            // Trường hợp 1: Nếu người chơi ĐÃ Click/Spawn
            if (isPlayerSpawned)
            {
                GameLogger.Log("Chapter 1: Đã hết thời gian chờ. Chuyển sang nhạc nền (Phase 2).");
                StopIntroSounds();
                StartBackgroundMusic();
            }
            // Trường hợp 2: Nếu người chơi VẪN CHƯA Spawn
            else
            {
                GameLogger.Log("Chapter 1: Đã hết thời gian chờ. Bắt đầu áp lực kinh dị.");
                StartDeathSequence();
            }
        }
        private void StartDeathSequence()
        {
            if (isDeathSequenceStarted) return;
            isDeathSequenceStarted = true;

            StopIntroSounds();
            StartBackgroundMusic();

            deathRoutine = StartCoroutine(DeathSequenceRoutine());
        }

        private void StopIntroSounds()
        {
            foreach (int id in activeIntroAudioIDs)
            {
                var audioToStop = Hellmade.Sound.EazySoundManager.GetAudio(id);
                if (audioToStop != null) audioToStop.Stop();
            }
            activeIntroAudioIDs.Clear();
        }

        private void StartBackgroundMusic()
        {
            if (isBGMusicStarted) return; // Bảo vệ: Không phát chồng nhạc
            
            isBGMusicStarted = true;
            GameLogger.Log("Chapter 1: Bắt đầu phát Nhạc nền (Layered Background Music).");

            // --- BẢN NHẠC 1 (Cổng Music) ---
            if (backgroundMusic1 != null)
            {
                Hellmade.Sound.EazySoundManager.PlayMusic(backgroundMusic1, backgroundMusic1Volume, true, false);
            }
            else
            {
                EazySoundManagerDemo demo = GameObject.FindFirstObjectByType<EazySoundManagerDemo>();
                if (demo != null && demo.AudioControls != null && demo.AudioControls.Length > 0)
                {
                    AudioClip demoClip = demo.AudioControls[0].audioclip;
                    if (demoClip != null)
                        Hellmade.Sound.EazySoundManager.PlayMusic(demoClip, backgroundMusic1Volume, true, false);
                }
            }

            // --- BẢN NHẠC 2 (Cổng Sound - Chạy song song) ---
            // Dọn dẹp nếu bản cũ đang chạy (vô vọng nhưng để chắc chắn)
            if (activeBGMusic2ID != -1)
            {
                var oldAudio = Hellmade.Sound.EazySoundManager.GetAudio(activeBGMusic2ID);
                if (oldAudio != null) oldAudio.Stop();
            }

            if (backgroundMusic2 != null)
            {
                activeBGMusic2ID = Hellmade.Sound.EazySoundManager.PlaySound(backgroundMusic2, backgroundMusicVolume2, true, null);
            }
            else
            {
                // Fallback lấy Element 1 của Demo nếu có
                EazySoundManagerDemo demo = GameObject.FindFirstObjectByType<EazySoundManagerDemo>();
                if (demo != null && demo.AudioControls != null && demo.AudioControls.Length > 1)
                {
                    AudioClip demoClip = demo.AudioControls[1].audioclip;
                    if (demoClip != null)
                        activeBGMusic2ID = Hellmade.Sound.EazySoundManager.PlaySound(demoClip, backgroundMusicVolume2, true, null);
                }
            }
        }

        private void PlayComplexStep(ComplexAudioStep step, bool loop = false)
        {
            if (step.clips != null)
            {
                foreach (var evt in step.clips)
                {
                    // Sử dụng hàm Play() chuẩn hóa từ AudioData (Ultimate Unification)
                    int id = evt.Play(globalVolumeMultiplier, loop);
                    
                    if (loop && id != -1)
                    {
                        activeIntroAudioIDs.Add(id);
                    }
                }
            }

            if (step.useGlitch && step.glitchPreset != null)
            {
                if (currentGlitchRoutine != null) StopCoroutine(currentGlitchRoutine);
                currentGlitchRoutine = StartCoroutine(PlayGlitch(step.glitchPreset));
            }
        }

        private IEnumerator DeathSequenceRoutine()
        {
            isDeathSequenceStarted = true;
            isFullMapPulseActive = true; 

            // Thu thập Camera Glitch nếu cần
            crtCamera = GameObject.FindFirstObjectByType<CRTCameraBehaviour>();
            if (crtCamera != null && crtCamera.data != null)
            {
                originalCrtData = crtCamera.data.Clone();
            }

            // Bắt đầu luồng dịch chuyển dồn dập của thây ma
            StartCoroutine(EnemyTeleportRoutine());

            // 1. Phát chuỗi Click Here (Mảng đa luồng + Glitch)
            if (clickSteps != null)
            {
                for (int i = 0; i < clickSteps.Length; i++)
                {
                    currentPulseSpeed = clickSteps[i].pulseSpeed;
                    PlayComplexStep(clickSteps[i]);

                    // Đợi hết clip dài nhất + thời gian chờ cấu hình
                    float maxLen = 0f;
                    if (clickSteps[i].clips != null)
                    {
                        foreach(var c in clickSteps[i].clips) 
                            if (c.clip != null && c.clip.length > maxLen) maxLen = c.clip.length;
                    }
                    
                    yield return new WaitForSeconds(maxLen + clickSteps[i].waitTime);
                }
            }

            // 2. Lặp file dồn dập (Nhanh dần)
            if (loopStep.clips != null && loopStep.clips.Length > 0)
            {
                float currentDelay = initialLoopDelay;
                int safetyCounter = 0;

                while (currentDelay > finalLoopDelay || safetyCounter < 5)
                {
                    float t = 1f - ((currentDelay - finalLoopDelay) / (initialLoopDelay - finalLoopDelay));
                    currentPulseSpeed = Mathf.Lerp(loopStep.pulseSpeed, finalPulseSpeed, t);

                    // Mỗi nhịp lặp lại, ta phát mảng âm thanh + Glitch mới
                    PlayComplexStep(loopStep);

                    yield return new WaitForSeconds(currentDelay);
                    
                    currentDelay = Mathf.Max(finalLoopDelay, currentDelay - loopAcceleration);
                    safetyCounter++;
                    if (safetyCounter > 50) break;
                }
            }

            // 3. Tiếng hét kết thúc + JUMPSCARE
            currentPulseSpeed = finalPulseSpeed * 1.5f; 
            PlayComplexStep(screamStep);
            
            // Tính toán thời gian tiếng hét dài nhất
            float sLen = 0f;
            if (screamStep.clips != null)
            {
                foreach(var c in screamStep.clips) 
                    if (c.clip != null && c.clip.length > sLen) sLen = c.clip.length;
            }
            if (sLen <= 0f) sLen = 2f; // Dự phòng nếu không có clip

            // Chờ một khoảng trễ trước khi hiện GIF (nếu có cấu hình)
            if (jumpscareAppearanceDelay > 0)
            {
                yield return new WaitForSeconds(jumpscareAppearanceDelay);
                sLen = Mathf.Max(0.1f, sLen - jumpscareAppearanceDelay); // Trừ bớt thời gian còn lại cho GIF
            }

            // Hiện Jumpscare Ảnh GIF - Lặp lại cho đến khi hết tiếng hét
            bool hasImage = jumpscareImage != null && jumpscareFrames != null && jumpscareFrames.Length > 0;
            bool hasSprite = jumpscareSpriteRenderer != null && jumpscareFrames != null && jumpscareFrames.Length > 0;

            if (hasImage || hasSprite)
            {
                if (hasImage) jumpscareImage.gameObject.SetActive(true);
                if (hasSprite) jumpscareSpriteRenderer.gameObject.SetActive(true);

                float delayPerFrame = 1f / jumpscareFps;
                float timer = 0f;
                int frameIndex = 0;

                while (timer < sLen)
                {
                    if (hasImage) jumpscareImage.sprite = jumpscareFrames[frameIndex];
                    if (hasSprite) jumpscareSpriteRenderer.sprite = jumpscareFrames[frameIndex];
                    
                    yield return new WaitForSeconds(delayPerFrame);
                    timer += delayPerFrame;
                    
                    frameIndex = (frameIndex + 1) % jumpscareFrames.Length;
                }
            }
            else
            {
                yield return new WaitForSeconds(sLen);
            }

            // Khôi phục Camera nếu bị nhiễu
            if (crtCamera != null && originalCrtData != null)
            {
                crtCamera.data = originalCrtData;
            }

            // ✨ [BƯỚC 5] CÁI CHẾT VÀ TÁI SINH
            if (deathOverlay != null) deathOverlay.SetActive(true);
            
            // Ẩn Jumpscare
            if (hasImage) jumpscareImage.gameObject.SetActive(false);
            if (hasSprite) jumpscareSpriteRenderer.gameObject.SetActive(false);

            if (shoesReward != null && InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(shoesReward, 1);
            }

            yield return new WaitForSeconds(2f); // Chờ 2 giây để người chơi cảm nhận cái chết

            SceneManager.LoadScene(nextSceneName);
        }

        private void TeleportEnemyRandomly()
        {
            if (enemyInfo == null)
            {
                // Tìm Enemy hiện tại trên map (bỏ qua Player nếu Có)
                foreach(var tile in MapManager.Instance.map.Values)
                {
                    if (tile.unitOnTile != null && (PlayerController.Instance == null || tile.unitOnTile != PlayerController.Instance.character))
                    {
                        enemyInfo = tile.unitOnTile;
                        break;
                    }
                }
            }

            if (enemyInfo != null)
            {
                List<OverlayTile> emptyTiles = new List<OverlayTile>();
                foreach(var tile in MapManager.Instance.map.Values)
                {
                    if (tile.unitOnTile == null) emptyTiles.Add(tile);
                }

                if (emptyTiles.Count > 0)
                {
                    OverlayTile randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                    
                    // Xóa Enemy khỏi ô cũ
                    if (enemyInfo.standingOnTile != null)
                    {
                        enemyInfo.standingOnTile.unitOnTile = null;
                    }

                    // Đưa Enemy vào ô mới
                    enemyInfo.standingOnTile = randomTile;
                    randomTile.unitOnTile = enemyInfo;

                    // Dịch chuyển Transform
                    enemyInfo.transform.position = randomTile.transform.position;
                }
            }
        }

        private IEnumerator EnemyTeleportRoutine()
        {
            while (!isPlayerSpawned && isDeathSequenceStarted)
            {
                // Tốc độ dịch chuyển phụ thuộc vào currentPulseSpeed
                // Ở tốc độ bình thường (Pulse = 5), delay sẽ là 0.4 giây. Ở tốc độ cao, delay có thể < 0.05 giây.
                float waitTime = 2f / (currentPulseSpeed * enemyTeleportSpeedMultiplier);
                
                yield return new WaitForSeconds(Mathf.Max(0.02f, waitTime)); 
                
                TeleportEnemyRandomly();
            }
        }

        private IEnumerator PlayGlitch(CRTGlitchDataObject preset)
        {
            if (crtCamera == null || crtCamera.data == null) yield break;

            float timer = 0f;
            float duration = Random.Range(0.15f, 0.4f); 

            while (timer < duration)
            {
                timer += Time.deltaTime;
                crtCamera.data.dithering4 = preset.dithering4;
                crtCamera.data.dithering8 = preset.dithering8;
                crtCamera.data.pixelationAmount = preset.pixelation;
                crtCamera.data.colorScans.redBlueChannelMultiplier = Random.Range(-preset.redBlueShift, preset.redBlueShift);
                crtCamera.data.colorScans.sizeMultiplier = preset.scanSize;
                crtCamera.data.vignette = preset.vignette;
                crtCamera.data.maxColorChannels.greyScale = preset.greyscale;
                crtCamera.data.tint = preset.tint;

                if (preset.flicker && Random.value > 0.7f)
                    crtCamera.data.pixelationAmount = Random.Range(10, 80);

                yield return null;
            }

            if (crtCamera != null && originalCrtData != null)
                crtCamera.data = originalCrtData;
        }

        public bool CanTapTile(OverlayTile tile)
        {
            // Giao đoạn CHƯA Spawn
            if (!isPlayerSpawned)
            {
                // Tuyệt đối không được Tap vào ô có Enemy
                if (tile.unitOnTile != null) return false;

                // Cho phép Tap ô tutorial hoặc toàn Map nếu đang nháy
                if (isFullMapPulseActive) return true;
                return tile == tutorialTile;
            }

            // Giai đoạn ĐÃ Spawn
            // Cho phép nhấp vào ô trống HOẶC ô có chính nhân vật mình đứng (để hiện Range)
            if (tile.unitOnTile != null)
            {
                // Cho phép Tap nếu Unit trên ô chính là Nhân vật của người chơi
                return tile.unitOnTile == PlayerController.Instance.character; 
            }

            return true;
        }

        private Coroutine deathRoutine;

        public void SetPlayerSpawned(bool state)
        {
            isPlayerSpawned = state;

            if (isPlayerSpawned)
            {
                // ✨ Tắt trạng thái nháy toàn map để di chuyển bình thường
                isFullMapPulseActive = false;
                
                // ✨ Dọn dẹp sạch sẽ trạng thái Tutorial để trả lại quyền điều khiển cho PlayerController
                HideAllTiles();

                if (deathRoutine != null) StopCoroutine(deathRoutine);
                
                // Loại bỏ StopAllMusic và Stop activeBGMusic2ID để giữ nhạc nền chạy xuyên suốt khi đã Spawn
                isDeathSequenceStarted = false;

                // ✨ [BƯỚC 3] Giới hạn tầm di chuyển của nhân vật về 1
                CharacterInfo player = GameObject.FindFirstObjectByType<CharacterInfo>();
                if (player != null)
                {
                    var stats = player.GetComponent<CharacterStats>();
                    if (stats != null && stats.characterData != null)
                    {
                        stats.characterData.moveRange = 1;
                        GameLogger.Log("Chapter 1: Đã giới hạn MoveRange về 1.");
                    }
                }
            }
        }

        private IEnumerator PulseTilesRoutine()
        {
            // Nháy liên tục cho đến khi người chơi Spawn nhân vật HOẶC khi đang trong chuỗi chết
            while (!isPlayerSpawned || isDeathSequenceStarted)
            {
                float alpha = (Mathf.Sin(Time.time * currentPulseSpeed) + 1f) / 2f;

                foreach (var data in pulseDataList)
                {
                    if (data.renderer == null || data.tile == null) continue;

                    // 1. Ô có Enemy: Tuyệt đối không nháy (Luôn ẩn)
                    if (data.tile.unitOnTile != null)
                    {
                        data.renderer.color = new Color(1, 1, 1, 0);
                        continue;
                    }

                    // 2. Chế độ Nháy Toàn Map (Áp lực Step 2 & 4)
                    if (isFullMapPulseActive)
                    {
                        Color c = data.renderer.color;
                        c.a = alpha;
                        data.renderer.color = c;
                        continue;
                    }

                    // 3. Chế độ Nháy 1 ô (Hướng dẫn Step 1) hoặc Vùng di chuyển (Step 3)
                    if (data.tile.isShowing)
                    {
                        Color c = data.renderer.color;
                        c.a = alpha;
                        data.renderer.color = c;
                    }
                    else
                    {
                        // Ẩn các ô còn lại
                        data.renderer.color = new Color(1, 1, 1, 0);
                    }
                }
                yield return null;
            }
        }

        private void HideAllTiles()
        {
            foreach (var data in pulseDataList)
            {
                if (data.tile != null) data.tile.HideTile();
            }
        }

        public bool IsMovementLocked() => isMovementLocked;

        public void OnPlayerMove()
        {
            if (isDeathSequenceStarted) return;

            // [BƯỚC 4] Reset bộ đếm thời gian đứng im khi người chơi di chuyển
            idleTimerAfterSpawn = 0f;

            currentSteps++;
            GameLogger.Log($"Chapter 1: Bước chân thứ {currentSteps}/{maxSteps}.");

            if (currentSteps >= maxSteps)
            {
                isMovementLocked = true;
                GameLogger.Log("Chapter 1: Đã hết lượt di chuyển. Vòng lặp sắp kết thúc...");
            }
        }
    }
}
