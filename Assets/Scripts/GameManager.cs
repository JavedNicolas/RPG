using UnityEngine;
using System.Collections;
using RPG.DataManagement;
using RPG.DataManagement.Player;
using RPG.DataManagement.Team;

public class GameManager
{
    [SerializeField] public CharacterDatabase characterDatabase { get; private set; }
    [SerializeField] public EnemyDatabase enemyDatabase { get; private set; }

    public Player player { get; private set; }
    public Team team { get; private set; }

    #region singleton
    public static GameManager instance = new GameManager();

    private GameManager()
    {
        loadDatabase();
        loadPlayer();
    }
    #endregion

    private void loadPlayer()
    {
        player = new Player();
        team = new Team();
        team.addCharacterToTeam(characterDatabase.getElement(0), true, BattlePosition.Center);
        team.addCharacterToTeam(characterDatabase.getElement(1), false, BattlePosition.Bottom);
        team.addCharacterToTeam(characterDatabase.getElement(2), true, BattlePosition.Bottom);
    }

    private void loadDatabase()
    {
        characterDatabase = Resources.LoadAll<CharacterDatabase>("")[0];
        enemyDatabase = Resources.LoadAll<EnemyDatabase>("")[0];
    }
}
