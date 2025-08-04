//考虑制作接口，使用装饰器模式来实现不同敌人的行为
public interface Enemy
{
    //移动速度
    //public float speed = 2f; 

    //获取血量值
    public int GetHealth();

    //扣除血量值
    public void MinusHealth(int attack);
}
