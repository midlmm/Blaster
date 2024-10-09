using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    //[SerializeField] private Transform _spawnPoint;
    //[SerializeField] private Player _playerPrefab;

    [SerializeField] private PlayerHUD _playerHUD;

    public override void InstallBindings()
    {
        BindPlayerHUD();
    }

    private void BindPlayerHUD()
    {
        Container
            .Bind<PlayerHUD>()
            .FromInstance(_playerHUD)
            .AsSingle();
    }
}
