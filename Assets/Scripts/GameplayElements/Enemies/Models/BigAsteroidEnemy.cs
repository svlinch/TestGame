public class BigAsteroidEnemy : AsteroidEnemy
{
    protected override void HandleHarm(Bullet bullet)
    {
        _eventService.SendMessage(new EnemySpecialEvent(_unit.GetPosition()));
        base.HandleHarm(bullet);
    }
}
