using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Tests.Interfaces
{
    // Audio Interfaces
    public interface IAudioManager
    {
        void PlaySound(AudioClip clip);
        void StopSound();
    }

    public interface IAudioSourceWrapper
    {
        void Play();
        void Stop();
        void SetVolume(float volume);
        void SetClip(AudioClip clip);
    }

    public interface IAudioClipFactory
    {
        AudioClip CreateClip(string name);
    }

    // Movement Interfaces
    public interface IRigidbodyWrapper
    {
        void MovePosition(Vector3 position);
        void AddForce(Vector3 force, ForceMode forceMode);
        void SetScale(Vector3 scale);
    }

    // UI Interfaces
    public interface IHealthBar
    {
        void SetValue(float value);
        float GetValue();
    }

    public interface IAmmoText
    {
        void SetText(string text);
        string GetText();
    }

    public interface IScoreText
    {
        void SetText(string text);
        string GetText();
    }

    public interface IAnimator
    {
        void PlayHealthUpdateAnimation();
        void PlayAmmoUpdateAnimation();
        void PlayScoreUpdateAnimation();
    }

    // Network Interfaces
    public interface IPhotonManager
    {
        void StartGame();
        void EndGame();
        void UpdateScore(int score);
    }

    public interface IPhotonNetwork
    {
        void CreateRoom(string roomName);
        void JoinRoom(string roomName);
        void LeaveRoom();
        bool IsConnected { get; }
    }

    public interface IPhotonView
    {
        void RPC(string methodName, RpcTarget target, params object[] parameters);
    }

    // Player Interfaces
    public interface IPlayerManager
    {
        void ResetPlayers();
        void SpawnPlayer(Vector3 position);
    }
} 