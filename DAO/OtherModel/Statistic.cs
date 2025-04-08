using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class Statistic
    {
        public int TotalLeader {  get; set; }
        public int TotalStaff {  get; set; }
        public int TotalAnimalType {  get; set; }
        public List<AnimalStatistic> AnimalStatistics {  get; set; }
        public int TotalTeam {  get; set; }
        public List<TeamStatistic> TeamStatistics { get; set; }
        public int TotalZooArea {  get; set; }
        public List<ZooAreaStatistic> ZooAreaStatistics { get; set; }
    }

    public class AnimalStatistic
    {
        public string AnimalTypeName {  get; set; }
        public int TotalQuantity {  get; set; }
    }
    public class TeamStatistic
    {
        public string TeamName {  get; set; }
        public string ZooAreaName { get; set; }
        public string LeaderName {  get; set; }
        public int TotalStaff { get; set; }
    }
    public class ZooAreaStatistic
    {
        public string Name { get; set; }
        public int TotalCage {  get; set; }
    }
}
