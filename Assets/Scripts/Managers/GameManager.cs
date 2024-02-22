public enum GameState //Oyun durumlar�n� tutmak i�in enum
{
    Playing,
    Won,
    End
}

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState = GameState.Playing;
    private bool isGameStarted = false;
    private bool gameWon;
    public bool IsGameStarted => isGameStarted;
    
    public void SetState(GameState state) //Oyun durumunu ayarla
    {
        CurrentState = state;

        switch (CurrentState)
        {
            case GameState.Playing:
                gameWon = false;
                break;
            case GameState.Won: 
                gameWon = true; 
                break;
            case GameState.End:                     
                if(gameWon) //Win durumu
                {
                    float bonusMultiplier = FindAnyObjectByType<BonusArea>().BonusMultiplier;  //BonusArea 'y� bul ve toplanan Bonuslar�n �arpan�n� ata
                    CurrencyManager.Instance.MultiplyLevelCurrency(bonusMultiplier); //Level parabirimini bonus ile katla
                    UIManager.Instance.ShowWinUI(); //Kazanma UI'�n� g�ster
                }
                else //Lose durumu
                {
                    UIManager.Instance.ShowLostUI(); //Oyunu kaybetme ui'�n� g�ster
                }
                break;
        }
    }
    public void StartGame()
    {
        isGameStarted = true;
        UIManager.Instance.SetGameStartUI();
    }
}