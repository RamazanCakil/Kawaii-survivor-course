
public enum GameState { 
    
    MENU,
    WEAPONSELECTION,
    GAME,
    GAMEOVER,
    STAGECOMPLATE,
    WAVETRANSITION,
    SHOP
    
}


public enum stat
{ 
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalPercent,
    MoveSpeed,
    MaxHealth,
    Range,
    HealthRecoverySpeed,
    Armor,
    Luck,
    Dodge,
    LifeSteal

}

public static class enums
{
    public static string FormatStatName(stat stat) {

        string formated = "";
        string unformatedString = stat.ToString();

        if (unformatedString.Length <= 0)
            return "unvalid stat name";

        formated += unformatedString[0];


        for(int i=1; i < unformatedString.Length; i++)
        {
            if (char.IsUpper(unformatedString[i]))
                formated += '_';

            formated += unformatedString[i];

        }
        return formated;

    }
}
