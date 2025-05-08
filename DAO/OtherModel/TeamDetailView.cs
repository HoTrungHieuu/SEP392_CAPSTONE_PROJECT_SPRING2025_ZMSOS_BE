using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TeamDetailView
    {
        public TeamView? Team {  get; set; }
        public int TotalCage {  get; set; }
        public int TotalAnimalType {  get; set; }
        public int TotalAnimal {  get; set; }
        public LeaderAssignView? Leader {  get; set; }
        public List<MemberAssignView>? Members { get; set; }
    }
}
