using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TreasureHuntDesktopApplication.Data;

namespace TreasureHuntDesktopApplication.DataService
{
   
    public class TreasureHuntService : ITreasureHuntService
    {
        public IEnumerable<hunt> GetTreasureHunts()
        {
            using (var context = new TreasureHuntEntities())
            {
                var returnedHunts = context.hunts.ToList();
                returnedHunts.ForEach(e => context.ObjectStateManager.ChangeObjectState(e,System.Data.EntityState.Detached));        
                return returnedHunts;
            }
        }

        public List<long> GetHuntQuestions(hunt hunt)
        { 
            using (var context = new TreasureHuntEntities())
            {
                if (hunt == null) return null;
                var returnedquestionIds = context.huntquestions.Where(c => c.HuntId == hunt.HuntId).Select(s => s.QuestionId).ToList();
                returnedquestionIds.ForEach(e => context.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Detached));
                return returnedquestionIds;
            }
        }

        public question GetQuestion(long questionId)
        {
            using (var context = new TreasureHuntEntities())
            {
                var returnedQuestions = context.questions.Where(c => c.QuestionId == questionId).Single();
                context.ObjectStateManager.ChangeObjectState(returnedQuestions, System.Data.EntityState.Detached);
                return returnedQuestions;
            }
        }

        public long SaveQuestion(question newQuestion)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.questions.AddObject(newQuestion);
                context.SaveChanges();
                context.ObjectStateManager.ChangeObjectState(newQuestion, System.Data.EntityState.Added);
                return newQuestion.QuestionId;
            }
        }
     
        public void SaveNewHuntQuestion(huntquestion huntQuestion)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.huntquestions.AddObject(huntQuestion);
                context.SaveChanges();
                context.ObjectStateManager.ChangeObjectState(huntQuestion, System.Data.EntityState.Added);
            }
        }

        public void SaveNewHunt(hunt newHunt)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.hunts.AddObject(newHunt);
                context.SaveChanges();
                context.ObjectStateManager.ChangeObjectState(newHunt, System.Data.EntityState.Added);
            }
        }

        public void UpdateQuestion(question updatedQuestion)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.questions.AddObject(updatedQuestion);
                context.SaveChanges();
                context.ObjectStateManager.ChangeObjectState(updatedQuestion, System.Data.EntityState.Modified);
            }
        }

        public hunt GetHuntBasedOnName(String name)
        {
            using (var context = new TreasureHuntEntities())
            {
                var returnedHunt = context.hunts.Where(c => c.HuntName == name).Single();
                context.ObjectStateManager.ChangeObjectState(returnedHunt, System.Data.EntityState.Detached);
                return returnedHunt;
            }
        }
    }
}
