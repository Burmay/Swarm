using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] Vector3 _spawnPoint;

    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefab(_playerPrefab, _spawnPoint, Quaternion.identity, null);

        Container.Bind<GameObject>().FromInstance(playerInstance).AsSingle();

        Container.Bind<PlayerMotionController>().FromInstance(playerInstance.GetComponent<PlayerMotionController>()).AsSingle();
    }
}