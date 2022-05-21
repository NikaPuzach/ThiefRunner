using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DarkTonic.MasterAudio;

public static class UserSettings
{
    private struct Keys
    {
        public const string RightHand           = "InputRightHand";
        public const string EnableMusic         = "EnableMusic";
        public const string VolumeMusic         = "VolumeMusic";
        public const string EnableSound         = "EnableSound";
        public const string VolumeSound         = "VolumeSound";
        public const string GraphicsLevel       = "GraphicsLevel";
        public const string FPSLimit            = "FPSLimit";
        public const string PrivacyAgreement    = "PrivacyAgreement";
        public const string ShowLoopFinished    = "ShowLoopFinished";
    }

    public enum GrapicsLevels
    {
        VeryLow = 0,
        Low,
        Medium,
        High,
        VeryHign,
        Ultra
    }
    const GrapicsLevels DefaultGraphicsLevel    = GrapicsLevels.Medium;

    private static bool privacyAgreement;
    public static bool PrivacyAgreement
    {
        get => privacyAgreement;

        set
        {
            privacyAgreement = value;
            PlayerPrefs.SetInt(Keys.PrivacyAgreement, value ? 1 : 0);
        }
    }

    private static bool rightHanded;
    public static bool RightHanded
    {
        get => rightHanded;

        set
        {
            rightHanded = value;
            PlayerPrefs.SetInt(Keys.RightHand, value ? 1 : 0);
        }
    }

    private static bool enableMusic;
    public static bool EnableMusic
    {
        get => enableMusic;

        set
        {
            enableMusic = value;
            PlayerPrefs.SetInt(Keys.EnableMusic, value ? 1 : 0);

            //if (!value)
            //    MasterAudio.MuteAllPlaylists();
            //else
            //    MasterAudio.UnmuteAllPlaylists();
        }
    }

    private static float volumeMusic;
    public static float VolumeMusic
    {
        get => volumeMusic;

        set
        {
            volumeMusic = value;
            PlayerPrefs.SetFloat(Keys.VolumeMusic, value);

            //MasterAudio.PlaylistMasterVolume = value;
        }
    }

    private static bool enableSound;
    public static bool EnableSound
    {
        get => enableSound;

        set
        {
            enableSound = value;
            PlayerPrefs.SetInt(Keys.EnableSound, value ? 1 : 0);

            //MasterAudio.MixerMuted = !value;
        }
    }

    private static float volumeSound;
    public static float VolumeSound
    {
        get => volumeSound;

        set
        {
            volumeSound = value;
            PlayerPrefs.SetFloat(Keys.VolumeSound, value);

            //MasterAudio.MasterVolumeLevel = value;
        }
    }

    private static GrapicsLevels graphicsLevel;
    public static GrapicsLevels GraphicsLevel
    {
        get => graphicsLevel;

        set 
        { 
            graphicsLevel = value;
            PlayerPrefs.SetInt(Keys.GraphicsLevel, (int) value);

            QualitySettings.SetQualityLevel((int) value, true);
        }
    }

    private static bool fpsLimit;
    public static bool FPSLimit
    {
        get => fpsLimit;

        set
        {
            fpsLimit = value;
            PlayerPrefs.SetInt(Keys.FPSLimit, value ? 1 : 0);

            Application.targetFrameRate = value ? 30 : 60;
        }
    }

    private static bool showLoopFinished;
    public static bool ShowLoopFinished
    {
        get => showLoopFinished;

        set
        {
            showLoopFinished = value;
            PlayerPrefs.SetInt(Keys.ShowLoopFinished, value ? 1 : 0);
        }
    }

    public static void Initialize()
    {
        PrivacyAgreement = PlayerPrefs.GetInt(Keys.PrivacyAgreement, 0) == 1;

        RightHanded = PlayerPrefs.GetInt(Keys.RightHand, 1) == 1;

        EnableMusic = PlayerPrefs.GetInt(Keys.EnableMusic, 1) == 1;
        VolumeMusic = PlayerPrefs.GetFloat(Keys.VolumeMusic, 0.5f);
        EnableSound = PlayerPrefs.GetInt(Keys.EnableSound, 1) == 1;
        VolumeSound = PlayerPrefs.GetFloat(Keys.VolumeSound, 0.5f);

        GraphicsLevel = (GrapicsLevels) PlayerPrefs.GetInt(Keys.GraphicsLevel, (int)DefaultGraphicsLevel);
        FPSLimit = PlayerPrefs.GetInt(Keys.FPSLimit, 1) == 1;

        ShowLoopFinished = PlayerPrefs.GetInt(Keys.ShowLoopFinished, 1) == 1;
    }
}
