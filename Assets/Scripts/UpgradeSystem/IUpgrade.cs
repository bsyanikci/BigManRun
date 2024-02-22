public interface IUpgrade
{
    bool Upgrade();
    bool IsUpgradeable(int upgradeCost);
    int CalculateUpgradeCost();
}