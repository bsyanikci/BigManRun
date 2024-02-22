public enum GameState //Oyun durumlarýný tutmak için enum
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
                    float bonusMultiplier = FindAnyObjectByType<BonusArea>().BonusMultiplier;  //BonusArea 'yý bul ve toplanan Bonuslarýn çarpanýný ata
                    CurrencyManager.Instance.MultiplyLevelCurrency(bonusMultiplier); //Level parabirimini bonus ile katla
                    UIManager.Instance.ShowWinUI(); //Kazanma UI'ýný göster
                }
                else //Lose durumu
                {
                    UIManager.Instance.ShowLostUI(); //Oyunu kaybetme ui'ýný göster
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