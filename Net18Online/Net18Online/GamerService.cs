using System.Collections.Generic;

public class GamerService
{
    private List<Gamer> gamers = new List<Gamer>();
    public void AddGamer( Gamer gamer ) { gamers.Add(gamer); }

}