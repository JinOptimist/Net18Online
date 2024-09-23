using System;
using System.Collections.Generic;
using Net18Online.Net18Online.Models;

namespace Net18Online.Net18Online;

/// <summary>
/// GamerService implementation for Net18Online
/// Methods AddGamer and GetGamers
/// </summary>
public class GamerService
{
    private List<Gamer> gamers = new List<Gamer>();

    // �����������
    public GamerService()
    {
        Console.WriteLine("������� ������� ������?");
        try
        {
            var playersCount = int.Parse(Console.ReadLine());
            for( int i = 0 ; i < playersCount ; i++ )
            {
                // ��������� ������ ������
                this.gamers.Add(new Gamer(i));
            }
        } catch( FormatException )
        {
            Console.WriteLine("������: ������� ���������� �����.");
        }
    }

    /// <summary>
    /// ����� ��� ��������� ������ �������
    /// </summary>
    /// <returns></returns>
    public List<Gamer> GetGamers()
    {
        return gamers;
    }

    /// <summary>
    /// ����� ��� ���������� ������
    /// </summary>
    /// <param name="gamer"></param>
    /// <returns></returns>
    public bool AddGamer( Gamer gamer )
    {
        bool result = false;
        gamers.Add(gamer);
        result = true;
        return result;
    }
}