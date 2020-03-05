using UnityEngine;
using System.Collections;

public class GameManager
{
    [SerializeField] public CharacterDatabase characterDatabase { get; private set; }
    [SerializeField] public EnemyDatabase enemyDatabase { get; private set; }

    public Player player { get; private set; }

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
        player.addCharacterToTeam(characterDatabase.getElement(0), true, BattlePosition.Center);
        player.addCharacterToTeam(characterDatabase.getElement(1), false, BattlePosition.Bottom);
        player.addCharacterToTeam(characterDatabase.getElement(2), true, BattlePosition.Bottom);
    }

    private void loadDatabase()
    {
        characterDatabase = Resources.LoadAll<CharacterDatabase>("")[0];
        enemyDatabase = Resources.LoadAll<EnemyDatabase>("")[0];
    }
}
