using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using Project.Scripts.DataBase.Data;

namespace Project.Scripts.DataBase
{
    [Serializable]
    public class SpreadsheetContent
    {
        [SpreadsheetPage("Enemies")] public List<EnemyData> Enemies;
        [SpreadsheetPage("PlayerClasses")] public List<PlayerClassesData> PlayerClasses;
        [SpreadsheetPage("Weapons")] public List<WeaponData> Weapons;
    }
}