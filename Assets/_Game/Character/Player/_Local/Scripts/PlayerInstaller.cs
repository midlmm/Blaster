using System;
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
        Container
            .Bind<ICharacterMovementsInput>()
            .To<PlayerMovementsInput>()
            .AsSingle();

        Container
            .Bind<ICameraRotateInput>()
            .To<CameraRotateInput>()
            .AsSingle();

        Container
            .Bind<IToolitemInput>()
            .To<PlayerToolitemInput>()
            .AsSingle();

        var player = Container
            .InstantiatePrefabForComponent<Player>(_prefabPlayer, _spawnPoint.position, _spawnPoint.rotation, null);

        Container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();
    }
}
