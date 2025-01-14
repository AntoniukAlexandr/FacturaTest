public class EnemyIdleTransition : Transition
{ 
    public override void StartTransition()
    {
        NeedTransit = true;
    }
    
    void Update()
    {
        
    }
}
