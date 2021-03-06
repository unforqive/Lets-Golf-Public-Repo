using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DailyRewardSystem;
using System;

public class MenuController : MonoBehaviour
{
	[Header("Menu Game Objects")]
	public GameObject StartScreen;
	public GameObject SettingsMenu;
	public GameObject PlayMenu;
	public GameObject SkinsMenu;
	public GameObject ShopMenu;
	public GameObject SplashScreen;
	public static string menu;
	public string nextMenu;
	public GameObject DailyReward;
	public DailyRewardSystem.DailyRewardSystem rewardSystem;

	[Space]
	[Header("Menu Animators")]
	public Animator SplashScreenAnim;
	public Animator StartScreenAnim;
	public Animator SettingsMenuAnimation;
	public Animator PlayMenuAnimation;
	public Animator SkinsMenuAnimation;
	public Animator ShopMenuAnimation;
	public Animator RewardNoti;

	[Space]
	[Header("Public Variables")]
	public bool returnToMenu;
	public int quality;
	public bool inGame;
	public bool menuClosed;

	[Space]
	[Header("Public Objects")]
	public AudioHandler audioHandler;
	public TMP_Text qualityText;
	public TMP_Dropdown qualityDropDown;
	public GameHandler gameHandler;
	public Slider powerBar;
	public DragPower dragPower;
	public GameObject parContainer;

	[Space]
	private bool startScreenAnimationTimer;
	private int MenuAnimationTimer;

	public bool canPause;

	public Slider timerSlider;
	public TimerHandler timerHandler;
	public GameObject timerObject;

	public MaxStrokesHandler maxStrokesHandler;
	public Slider maxStrokesSlider;

	public LoadName loadName;

	void Awake()
	{
		quality = PlayerPrefs.GetInt("Quality");
		qualityDropDown.value = quality;

		Debug.Log("Player Preferences Loaded.");
	}

	void Start()
	{
		canPause = false;
		menu = "Splash Screen";

		startScreenAnimationTimer = false;
		MenuAnimationTimer = 0;

		returnToMenu = false;

		inGame = false;
		parContainer.SetActive(false);
	}

	void Update()
	{
		maxStrokesHandler.maxStrokes = Convert.ToInt32(maxStrokesSlider.value);
		timerHandler.time = Convert.ToInt32(timerSlider.value);

		if (startScreenAnimationTimer)
		{
			MenuAnimationTimer += 1;
		}

		if (MenuAnimationTimer > 20)
		{
			if (nextMenu == "LootBox Menu")
			{
				menu = "LootBox";
			}

			if (nextMenu == "Start Menu")
			{
				menu = "Start Screen";
			}

			if (nextMenu == "Play Menu")
			{
				menu = "Play Screen";
			}

			if (nextMenu == "Settings Menu")
			{
				menu = "Settings Screen";
			}

			if (nextMenu == "Shop Menu")
			{
				menu = "Shop Screen";
			}

			if (nextMenu == "Skins Menu")
			{
				menu = "Skins Screen";
			}

			startScreenAnimationTimer = false;
			MenuAnimationTimer = 0;
		}

		if (menu == "Splash Screen")
		{
			SplashScreen.SetActive(true);
			StartScreen.SetActive(false);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(false);
		}

		if (menu == "Start Screen")
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(true);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(false);

			DailyReward.SetActive(true);
		}

		if (menu == "Start Screen" && returnToMenu)
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(true);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(false);

			StartScreenAnim.SetBool("Start Appear", true);
			StartScreenAnim.SetBool("Start Disappear", false);
			RewardNoti.SetBool("In", true);
			RewardNoti.SetBool("Out", false);

			dragPower.strokesContainer.SetActive(false);
			parContainer.SetActive(false);
			returnToMenu = false;
		}

		if (menu == "Play Screen")
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(false);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(true);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(false);
		}

		if (menu == "Settings Screen")
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(false);
			SettingsMenu.SetActive(true);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(false);
		}

		if (menu == "Shop Screen")
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(false);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(false);
			ShopMenu.SetActive(true);
		}

		if (menu == "Skins Screen")
		{
			SplashScreen.SetActive(false);
			StartScreen.SetActive(false);
			SettingsMenu.SetActive(false);
			PlayMenu.SetActive(false);
			SkinsMenu.SetActive(true);
			ShopMenu.SetActive(false);
		}	
	}

	#region Menu Buttons

	public void BeginningScreen()
	{
		startScreenAnimationTimer = true;

		nextMenu = "Start Menu";

		SplashScreenAnim.SetBool("CameraDown", true);
	}

	public void Play()
	{
		startScreenAnimationTimer = true;

		nextMenu = "Play Menu";

		StartScreenAnim.SetBool("Start Appear", false);
		StartScreenAnim.SetBool("Start Disappear", true);
		RewardNoti.SetBool("In", false);
		RewardNoti.SetBool("Out", true);

		audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
	}

	public void Settings()
	{
		startScreenAnimationTimer = true;

		nextMenu = "Settings Menu";

		StartScreenAnim.SetBool("Start Appear", false);
		StartScreenAnim.SetBool("Start Disappear", true);
		RewardNoti.SetBool("In", false);
		RewardNoti.SetBool("Out", true);

		audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
	}

	public void Shop()
	{
		startScreenAnimationTimer = true;

		nextMenu = "Shop Menu";

		StartScreenAnim.SetBool("Start Appear", false);
		StartScreenAnim.SetBool("Start Disappear", true);
		RewardNoti.SetBool("In", false);
		RewardNoti.SetBool("Out", true);

		audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
	}

	public void Skins()
	{
		startScreenAnimationTimer = true;

		nextMenu = "Skins Menu";

		StartScreenAnim.SetBool("Start Appear", false);
		StartScreenAnim.SetBool("Start Disappear", true);
		RewardNoti.SetBool("In", false);
		RewardNoti.SetBool("Out", true);

		audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
	}

	#endregion

	#region Close Menu Buttons

	public void CloseMenu()
	{
		if (menu == "Play Screen")
		{
			PlayMenuAnimation.SetBool("Play Appear", false);
			PlayMenuAnimation.SetBool("Play Disappear", true);
			returnToMenu = true;

			audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
		}

		if (menu == "Settings Screen")
		{
			SettingsMenuAnimation.SetBool("Settings Appear", false);
			SettingsMenuAnimation.SetBool("Settings Disappear", true);
			returnToMenu = true;

			audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
		}

		if (menu == "Shop Screen")
		{
			ShopMenuAnimation.SetBool("Shop Appear", false);
			ShopMenuAnimation.SetBool("Shop Disappear", true);
			returnToMenu = true;

			audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
		}

		if (menu == "Skins Screen")
		{
			SkinsMenuAnimation.SetBool("Skins Appear", false);
			SkinsMenuAnimation.SetBool("Skins Disappear", true);
			returnToMenu = true;

			audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
		}

		startScreenAnimationTimer = true;
		nextMenu = "Start Menu";
		menuClosed = true;
	}

	#endregion

	public void StartMenu()
	{
		menu = "Start Screen";

		StartScreenAnim.SetBool("Start Appear", true);
		StartScreenAnim.SetBool("Start Disappear", false);
		RewardNoti.SetBool("In", true);
		RewardNoti.SetBool("Out", false);

		audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
		quality = qualityIndex;
	}

	public void SetQualityPref()
	{
		PlayerPrefs.SetInt("Quality", quality);

		Debug.Log("Player Preferences Saved.");
	}

	public void LaunchGame()
	{
		loadName.displayName.SetActive(false);

		timerObject.SetActive(true);

		CloseMenu();

		audioHandler.soundtrack.Pause();

		canPause = true;

		gameHandler.EnablePlayerCamera();
		inGame = true;
		powerBar.gameObject.transform.parent.gameObject.SetActive(true);
		dragPower.strokesContainer.SetActive(true);
		parContainer.SetActive(true);

		timerHandler.StartTime();
	}

	public void QuitGame()
	{
		inGame = false;

		canPause = false;

		powerBar.gameObject.transform.parent.gameObject.SetActive(false);
		dragPower.strokesContainer.SetActive(false);
		parContainer.SetActive(false);
	}
}
