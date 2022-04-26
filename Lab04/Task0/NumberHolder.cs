using System;

public class NumberHolder: Singleton<NumberHolder>
{
    private int number;
    private Random randomGen;

    public int Number { get { return number; } }

    public event Action<int> Changed;

    protected NumberHolder()
    {
        this.randomGen = new Random();
        this.Randomize();
    }

    // Whoops! You can create a public constructor with 
    // a different parameter signature!
    // public NumberHolder(int b)
    // {

    // }

    public void Randomize()
    {
        this.number = this.randomGen.Next(100000) + 2000;
        if (this.Changed != null) 
            this.Changed.Invoke(this.number);
    }
}
