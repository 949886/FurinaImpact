using System.Numerics;
using FurinaImpact.Common.Data.Binout;
using FurinaImpact.Gameserver.Controllers;
using FurinaImpact.Gameserver.Game.Avatar;
using FurinaImpact.Gameserver.Game.Entity;
using FurinaImpact.Gameserver.Game.Entity.Factory;
using FurinaImpact.Gameserver.Network.Session;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Scene;
internal class SceneManager
{
    public uint EnterToken { get; private set; }

    private readonly BinDataCollection _binData;

    private readonly NetSession _session;
    private readonly Player _player;
    private readonly EntityManager _entityManager;
    private readonly EntityFactory _entityFactory;

    private readonly List<AvatarEntity> _teamAvatars;

    private uint _enterTokenSeed;
    private uint _sceneId;
    private ulong _beginTime;

    private SceneEnterState _enterState;

    public SceneManager(NetSession session, Player player, EntityManager entityManager, EntityFactory entityFactory, BinDataCollection binData)
    {
        _session = session;
        _player = player;
        _entityManager = entityManager;
        _entityFactory = entityFactory;

        _binData = binData;
        _teamAvatars = new();
    }

    public async ValueTask OnEnterStateChanged(SceneEnterState changedToState)
    {
        if (_enterState is SceneEnterState.None or SceneEnterState.Complete)
            throw new InvalidOperationException($"SceneManager::OnEnterStateChanged called when enter state is {_enterState}!");

        if (_enterState > changedToState)
            throw new ArgumentException($"SceneManager::OnEnterStateChanged - requested state is less than current! (curr={_enterState}, req={changedToState})");

        if (_enterState + 1 != changedToState)
            throw new ArgumentException($"SceneManager::OnEnterStateChanged - trying to skip enter state! (curr={_enterState}, req={changedToState})");

        _enterState = changedToState;
        switch (_enterState)
        {
            case SceneEnterState.ReadyToEnter:
                await OnReadyToEnterScene();
                break;
            case SceneEnterState.InitFinished:
                await OnSceneInitFinished();
                break;
            case SceneEnterState.EnterDone:
                await OnEnterDone();
                break;
            case SceneEnterState.PostEnter:
                await OnPostEnter();
                break;
        }

        if (_enterState == SceneEnterState.PostEnter)
            _enterState = SceneEnterState.Complete;
    }

    public async ValueTask ChangeTeamAvatarsAsync(ulong[] guidList)
    {
        _teamAvatars.Clear();

        foreach (ulong guid in guidList)
        {
            GameAvatar gameAvatar = _player.Avatars.Find(avatar => avatar.Guid == guid)!; // currently only first one

            AvatarEntity avatarEntity = _entityFactory.CreateAvatar(gameAvatar, _player.Uid);
            avatarEntity.SetPosition(2336.789f, 249.98896f, -751.3081f);

            _teamAvatars.Add(avatarEntity);
        }

        await SendSceneTeamUpdate();
        await _entityManager.SpawnEntityAsync(_teamAvatars[0], VisionType.Born);
    }

    private async ValueTask OnEnterDone()
    {
        await _entityManager.SpawnEntityAsync(_teamAvatars[0], VisionType.Born);
    }

    private async ValueTask OnSceneInitFinished()
    {
        GameAvatarTeam avatarTeam = _player.GetCurrentTeam();

        foreach (ulong guid in avatarTeam.AvatarGuidList)
        {
            GameAvatar gameAvatar = _player.Avatars.Find(avatar => avatar.Guid == guid)!;

            AvatarEntity avatarEntity = _entityFactory.CreateAvatar(gameAvatar, _player.Uid);
            avatarEntity.SetPosition(2336.789f, 249.98896f, -751.3081f);

            _teamAvatars.Add(avatarEntity);
        }

        await SendEnterSceneInfo();
        await SendSceneTeamUpdate();
    }

    private async ValueTask OnReadyToEnterScene()
    {
        await _session.NotifyAsync(CmdType.EnterScenePeerNotify, new EnterScenePeerNotify
        {
            DestSceneId = _sceneId,
            EnterSceneToken = EnterToken,
            HostPeerId = 1, // TODO: Scene peers
            PeerId = 1
        });
    }

    private ValueTask OnPostEnter()
    {
        return ValueTask.CompletedTask;
    }

    public async ValueTask EnterSceneAsync(uint sceneId)
    {
        if (_beginTime != 0) ResetState();

        _beginTime = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
        _sceneId = sceneId;
        EnterToken = ++_enterTokenSeed;

        _enterState = SceneEnterState.EnterRequested;
        await _session.NotifyAsync(CmdType.PlayerEnterSceneNotify, new PlayerEnterSceneNotify
        {
            SceneBeginTime = _beginTime,
            SceneId = _sceneId,
            SceneTransaction = CreateTransaction(_sceneId, _player.Uid, _beginTime),
            Pos = new()
            {
                X = 2191.16357421875f,
                Y = 214.65115356445312f,
                Z = -1120.633056640625f
            },
            TargetUid = _player.Uid,
            UnkUid1020 = _player.Uid,
            EnterSceneToken = EnterToken,
            PrevPos = new(),
            Unk13 = 1,
            Unk3 = 1,
            Unk449 = 1,
            Unk834 = 1
        });
    }

    private async ValueTask SendSceneTeamUpdate()
    {
        SceneTeamUpdateNotify sceneTeamUpdate = new();
        foreach (AvatarEntity avatar in _teamAvatars)
        {
            sceneTeamUpdate.SceneTeamAvatarList.Add(new SceneTeamAvatar
            {
                SceneEntityInfo = avatar.AsInfo(),
                WeaponEntityId = SceneController.WeaponEntityId,
                PlayerUid = _player.Uid,
                WeaponGuid = GameAvatar.WeaponGuid,
                EntityId = avatar.EntityId,
                AvatarGuid = avatar.GameAvatar.Guid,
                AbilityControlBlock = avatar.BuildAbilityControlBlock(_binData),
                SceneId = _sceneId
            });
        }

        await _session.NotifyAsync(CmdType.SceneTeamUpdateNotify, sceneTeamUpdate);
    }

    private async ValueTask SendEnterSceneInfo()
    {
        PlayerEnterSceneInfoNotify enterSceneInfo = new()
        {
            CurAvatarEntityId = _teamAvatars[0].EntityId,
            EnterSceneToken = EnterToken,
            MpLevelEntityInfo = new MPLevelEntityInfo
            {
                EntityId = 184549377,
                AbilityInfo = new AbilitySyncStateInfo(),
                AuthorityPeerId = 1
            },
            TeamEnterInfo = new TeamEnterSceneInfo
            {
                TeamEntityId = 150994946,
                AbilityControlBlock = new AbilityControlBlock(),
                TeamAbilityInfo = new AbilitySyncStateInfo()
            }
        };

        foreach (AvatarEntity avatar in _teamAvatars)
        {
            enterSceneInfo.AvatarEnterInfo.Add(new AvatarEnterSceneInfo
            {
                AvatarGuid = avatar.GameAvatar.Guid,
                AvatarEntityId = avatar.EntityId,
                WeaponEntityId = SceneController.WeaponEntityId,
                WeaponGuid = GameAvatar.WeaponGuid
            });
        }

        await _session.NotifyAsync(CmdType.PlayerEnterSceneInfoNotify, enterSceneInfo);
    }

    private void ResetState()
    {
        _teamAvatars.Clear();
        _entityManager.Reset();
    }

    private static string CreateTransaction(uint sceneId, uint playerUid, ulong beginTime)
        => string.Format("{0}-{1}-{2}-13830", sceneId, playerUid, beginTime);
}
