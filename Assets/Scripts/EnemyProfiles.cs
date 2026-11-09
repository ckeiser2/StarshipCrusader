
public static class EnemyProfiles
{
    public static EnemyData CreateScout()
    {
        return new EnemyData(enemyName: "Scout", maximumHealth: 2, scoreValue: 100);
        
    }

    public static EnemyData CreateHeavyFighter()
    {
        return new EnemyData(enemyName: "Heavy Fighter", maximumHealth: 4, scoreValue: 200);
    }
    
    public static EnemyData CreateTurret()
    {
        return new EnemyData(enemyName: "Turret Sentry", maximumHealth: 2, scoreValue: 300);
    }
    
}
