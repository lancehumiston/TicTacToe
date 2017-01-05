using System.Collections.Generic;

class WinSequence
{
    public List<int> combo { get; private set; }
    public WinSequence (int spot1, int spot2, int spot3)
    {
        this.combo = new List<int>();
        combo.Add(spot1);
        combo.Add(spot2);
        combo.Add(spot3);
    }
}

