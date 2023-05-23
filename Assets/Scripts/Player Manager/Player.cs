
public class Player
{
    private int _playerLevel;

    public int GetPlayerLevel() => _playerLevel;

    public Player(int level)
    {
        _playerLevel = level;
    }

    public void IncreasePlayerLevel(int maxLevelsCount)
    {
        if (_playerLevel >= maxLevelsCount) return;
        _playerLevel += 1;
    }

}
