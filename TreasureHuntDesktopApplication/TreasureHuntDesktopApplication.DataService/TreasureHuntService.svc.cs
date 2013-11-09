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
                //do i need to detach here?
                context.ObjectStateManager.ChangeObjectState(returnedHunts, System.Data.EntityState.Detached);
                return returnedHunts;
            }
        }

        //Need to refactor - not sure the best way to do this
        //returns a list of hunt questions ids
        public List<long> GetHuntQuestions(hunt hunt)
        { 
            using (var context = new TreasureHuntEntities())
            {
                //Selects all of the question ids
                var returnedquestionIds = context.huntquestions.Where(c => c.HuntId == hunt.HuntId).Select(s => s.QuestionId).ToList();
                context.ObjectStateManager.ChangeObjectState(returnedquestionIds, System.Data.EntityState.Detached);
                return returnedquestionIds;
            }
        }

        //Returns a list of questions for a given question Id
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
                return newQuestion.QuestionId;
            }
        }
     
        public void SaveNewHuntQuestion(huntquestion huntQuestion)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.huntquestions.AddObject(huntQuestion);
                context.SaveChanges();
            }
        }

        public void SaveNewHunt(hunt newHunt)
        {
            using (var context = new TreasureHuntEntities())
            {
                context.hunts.AddObject(newHunt);
                context.SaveChanges();
            }
        }
    }
}
