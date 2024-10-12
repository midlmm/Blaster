using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _prefabPlayer;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        var player = Instantiate(_prefabPlayer, _spawnPoint.position, _spawnPoint.rotation, null);

        player.Initialize();

        Container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();
    }
}
