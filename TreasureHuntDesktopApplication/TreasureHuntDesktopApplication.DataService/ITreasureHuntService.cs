using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TreasureHuntDesktopApplication.Data;

namespace TreasureHuntDesktopApplication.DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITreasureHuntService" in both code and config file together.
    [ServiceContract]
    public interface ITreasureHuntService
    {
        [OperationContract]
        long SaveQuestion(question newQuestion);

        [OperationContract]
        void SaveNewHuntQuestion(huntquestion huntQuestion);

        [OperationContract]
        void SaveNewHunt(hunt newHunt);

        [OperationContract]
        List<long> GetHuntQuestions(hunt hunt);

        [OperationContract]
        IEnumerable<hunt> GetTreasureHunts();

        [OperationContract]
        question GetQuestion(long questionId);

        [OperationContract]
        void UpdateQuestion(question updatedQuestion);

        [OperationContract]
        hunt GetHuntBasedOnName(String name);
    }
}
