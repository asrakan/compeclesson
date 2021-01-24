using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Stat
{
    public interface IPlayerStatHolder
    {
        PlayerStat PlayerStat { get; }
        void SetStat(PlayerStat stat);
    }
}