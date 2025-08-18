using System.Collections.Generic;

public static class GlobalData
{
    //敌人列表
    public static List<Enemy> globalEnemies = new List<Enemy>();

    //在后续开发中，敌人的接受攻击函数，传入此攻击属性作为参数，达到抗性效果
    enum AttackAttribute
    {
        JIN, MU, SHUI, HUO, TU
    }

}
